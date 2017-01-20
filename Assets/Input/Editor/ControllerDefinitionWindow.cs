using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class ControllerDefinitionWindow : EditorWindow
{
    TextAsset m_controllerDefData;
    string m_assetName = "Input/ControllerDefinitions/new.txt";

    int m_joystickCount = 3;
    int m_triggerCount = 2;
    int m_buttonCount = 16;
    Vector2 m_scrollPos;

    ControllerDefinition.ControllerData m_controllerData;

    [MenuItem("Input/Manage ControllerDefinitions")]
    static void Init()
    {
        ControllerDefinitionWindow window = (ControllerDefinitionWindow)EditorWindow.GetWindow(typeof(ControllerDefinitionWindow));
        window.Show();
    }

    void OnGUI()
    {
        m_scrollPos = GUILayout.BeginScrollView(m_scrollPos);

        GUILayout.Label("Manage ControllerDefinitions", EditorStyles.boldLabel);

        m_controllerDefData = (TextAsset)EditorGUILayout.ObjectField(m_controllerDefData, typeof(TextAsset));

        if (GUILayout.Button("Load"))
        {
            Load();
        }
        if (GUILayout.Button("Save"))
        {
            Save();
        }

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Create"))
        {
            Create();
        }
        m_assetName = GUILayout.TextField(m_assetName);
        GUILayout.EndHorizontal();

        EditorGUILayout.Separator();


        if (m_controllerData == null)
            m_controllerData = new ControllerDefinition.ControllerData();

        GUILayout.Label("Mapping", EditorStyles.boldLabel);

        m_controllerData.Name = LabelTextField("Name", m_controllerData.Name);
        m_controllerData.ControllerNameRegex = LabelTextField("Controller Name Regex", m_controllerData.ControllerNameRegex);
        m_joystickCount = LabelIntField("Joysticks", m_joystickCount);
        m_triggerCount = LabelIntField("Triggers", m_triggerCount);
        m_buttonCount = LabelIntField("Buttons", m_buttonCount);

        if (m_controllerData.Joysticks == null || m_controllerData.Joysticks.Length != m_joystickCount)
        {
            m_controllerData.Joysticks = new ControllerDefinition.Joystick[m_joystickCount];
            for (int i = 0; i < m_controllerData.Joysticks.Length; i++)
                m_controllerData.Joysticks[i] = new ControllerDefinition.Joystick();
        }

        if (m_controllerData.Triggers == null || m_controllerData.Triggers.Length != m_triggerCount)
        {
            m_controllerData.Triggers = new ControllerDefinition.Trigger[m_triggerCount];
            for (int i = 0; i < m_controllerData.Triggers.Length; i++)
                m_controllerData.Triggers[i] = new ControllerDefinition.Trigger();
        }

        if (m_controllerData.Buttons == null || m_controllerData.Buttons.Length != m_buttonCount)
        {
            m_controllerData.Buttons = new int[m_buttonCount];
            for (int i = 0; i < m_controllerData.Buttons.Length; i++)
                m_controllerData.Buttons[i] = new int();
        }


        EditorGUILayout.Separator();

        GUILayout.Label("Joysticks", EditorStyles.boldLabel);
        for (int i = 0; i < m_joystickCount; i++)
        {
            EditorGUILayout.Separator();
            GUILayout.Label("Joystick " + i);

            m_controllerData.Joysticks[i].HorizontalAxis = LabelIntField("Horizontal axis", m_controllerData.Joysticks[i].HorizontalAxis);
            m_controllerData.Joysticks[i].VerticalAxis = LabelIntField("Vertical axis", m_controllerData.Joysticks[i].VerticalAxis);
            m_controllerData.Joysticks[i].Deadzone = LabelFloatField("Deadzone", (float)m_controllerData.Joysticks[i].Deadzone);
        }

        GUILayout.Label("Triggers", EditorStyles.boldLabel);
        for (int i = 0; i < m_triggerCount; i++)
        {
            EditorGUILayout.Separator();
            GUILayout.Label("Trigger " + i);

            m_controllerData.Triggers[i].Axis = LabelIntField("Axis", m_controllerData.Triggers[i].Axis);
            m_controllerData.Triggers[i].Deadzone = LabelFloatField("Deadzone", (float)m_controllerData.Triggers[i].Deadzone);
        }

        GUILayout.Label("Buttons", EditorStyles.boldLabel);
        for (int i = 0; i < m_buttonCount; i++)
        {
            m_controllerData.Buttons[i] = LabelIntField("Button " + i, m_controllerData.Buttons[i]);
        }
         
        GUILayout.EndScrollView();
    }

    void Save()
    {
        if (m_controllerDefData != null)
        {
            string jsonText = LitJson.JsonMapper.ToJson(m_controllerData);
            string path = AssetDatabase.GetAssetPath(m_controllerDefData.GetInstanceID());

            File.WriteAllText(path, jsonText);
            AssetDatabase.Refresh();

            Debug.Log("Saved to: " + path);
        }
        else
        {
            Debug.LogWarning("Failed to save: No asset referenced. ");
        }
    }

    void Load()
    {
        if (m_controllerDefData != null)
        {
            m_controllerData = LitJson.JsonMapper.ToObject<ControllerDefinition.ControllerData>(m_controllerDefData.text);

            m_joystickCount = m_controllerData.Joysticks.Length;
            m_triggerCount = m_controllerData.Triggers.Length;
            m_buttonCount = m_controllerData.Buttons.Length;
        }
        else
        {
            Debug.LogWarning("Failed to load: No asset referenced. ");
        }
    }

    void Create()
    {
        string jsonText = LitJson.JsonMapper.ToJson(m_controllerData);

        File.WriteAllText(Application.dataPath + "/" + m_assetName, jsonText);
        AssetDatabase.Refresh();

        Debug.Log("Created: " + Application.dataPath + "/" + m_assetName);
    }

    int LabelIntField(string label, int value)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label(label);
        value = EditorGUILayout.IntField(value);
        GUILayout.EndHorizontal();

        return value;
    }
    float LabelFloatField(string label, float value)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label(label);
        value = EditorGUILayout.FloatField(value);
        GUILayout.EndHorizontal();

        return value;
    }

    string LabelTextField(string label, string value)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label(label);
        value = EditorGUILayout.TextField(value);
        GUILayout.EndHorizontal();

        return value;
    }
}