using UnityEngine;
using UnityEditor;
using System.Collections;

public class SetupInputManagerWindow : EditorWindow
{
    int m_maxControllers = 11;
    int m_maxAxes = 20;

    [MenuItem("Input/Setup InputManager")]
    static void Init()
    {
        SetupInputManagerWindow window = (SetupInputManagerWindow)EditorWindow.GetWindow(typeof(SetupInputManagerWindow));
        window.Show();
    }

    private static SerializedProperty GetChildProperty(SerializedProperty parent, string name)
    {
        SerializedProperty child = parent.Copy();
        child.Next(true);
        do
        {
            if (child.name == name) return child;
        }
        while (child.Next(false));
        return null;
    }
    enum AxisType
    {
        KeyOrMouseButton = 0,
        MouseMovement = 1,
        JoystickAxis = 2
    };

    void OnGUI()
    {
        GUILayout.Label("Setup InputManager.", EditorStyles.boldLabel);
        GUILayout.Label("This will overwrite your previous input values in ProjectSettings/InputManager.asset", EditorStyles.wordWrappedLabel);

        if (GUILayout.Button("Setup"))
        {
            Setup();
        }
    }

    void Setup()
    {
        SerializedObject serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
        SerializedProperty axesProperty = serializedObject.FindProperty("m_Axes");
        axesProperty.ClearArray();

        for (int i = 0; i < m_maxControllers; i++)
        {
            for (int j = 0; j < m_maxAxes; j++)
            {
                AddAxis(axesProperty, string.Format("button_{0}_{1}", i, j), AxisType.KeyOrMouseButton, i, j);
                AddAxis(axesProperty, string.Format("axis_{0}_{1}", i, j), AxisType.JoystickAxis, i, j);
            }
        }

        serializedObject.ApplyModifiedProperties();
        Debug.Log("Done");
    }

    void AddAxis(SerializedProperty axesProperty, string name, AxisType type, int joyNum, int axis)
    {
        axesProperty.arraySize++;
        SerializedProperty axisProperty = axesProperty.GetArrayElementAtIndex(axesProperty.arraySize - 1);

        GetChildProperty(axisProperty, "m_Name").stringValue = name;
        GetChildProperty(axisProperty, "descriptiveName").stringValue = "";
        GetChildProperty(axisProperty, "descriptiveNegativeName").stringValue = "";
        GetChildProperty(axisProperty, "negativeButton").stringValue = "";
        GetChildProperty(axisProperty, "positiveButton").stringValue = type == AxisType.KeyOrMouseButton ? "joystick " + joyNum + " button " + axis : "";
        GetChildProperty(axisProperty, "altNegativeButton").stringValue = "";
        GetChildProperty(axisProperty, "altPositiveButton").stringValue = "";
        GetChildProperty(axisProperty, "gravity").floatValue = 1000;
        GetChildProperty(axisProperty, "dead").floatValue = 0;
        GetChildProperty(axisProperty, "sensitivity").floatValue = 1;
        GetChildProperty(axisProperty, "snap").boolValue = false;
        GetChildProperty(axisProperty, "invert").boolValue = false;
        GetChildProperty(axisProperty, "type").intValue = (int)type;
        GetChildProperty(axisProperty, "axis").intValue = type == AxisType.KeyOrMouseButton ? 0 : axis;
        GetChildProperty(axisProperty, "joyNum").intValue = joyNum;
    }
}