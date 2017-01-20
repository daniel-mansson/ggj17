using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class InputManager : MonoBehaviour 
{
    [SerializeField]
    TextAsset m_defaultControllerDefinitionData;
    
    [SerializeField]
    List<TextAsset> m_controllerDefinitionData = new List<TextAsset>();

    List<ControllerDefinition> m_controllerDefinitions = new List<ControllerDefinition>();
    List<ControllerImpl> m_controllers = new List<ControllerImpl>();

    ControllerDefinition m_defaultControllerDefinition;

	void Awake () 
    {
        foreach (var data in m_controllerDefinitionData)
            m_controllerDefinitions.Add(new ControllerDefinition(data.text));

        m_defaultControllerDefinition = new ControllerDefinition(m_defaultControllerDefinitionData.text);

        for (int i = 0; i < 8; i++)
        {
            //Don't match by name. Use defualt always
           /* string controllerName = Input.GetJoystickNames()[i];

            if(controllerName.Length == 0)
                continue;

            Debug.Log("[Input] Detected controller: " + controllerName);
            ControllerDefinition match = null;

            foreach (var def in m_controllerDefinitions)
            {
                if (Regex.Match(controllerName.ToLower(), def.Data.ControllerNameRegex).Success)
                {
                    match = def;
                    break;
                }
            }

            if (match != null)
            {
                Debug.Log("[Input] Match found: " + match.Data.Name);

                m_controllers.Add(new ControllerImpl(match, i + 1));
            }
            else
            {
                Debug.Log("[Input] No match found. Using default definition");

                m_controllers.Add(new ControllerImpl(m_defaultControllerDefinition, i + 1));
            }*/

            m_controllers.Add(new ControllerImpl(m_defaultControllerDefinition, i + 1));
        }
	}
	
	void Update () 
    {
        foreach (var controller in m_controllers)
        {
            controller.Update();
        }
	}

    public int GetControllerCount()
    {
        return m_controllers.Count;
    }

    public Controller GetController(int id)
    {
        return m_controllers[id];
    }
}
