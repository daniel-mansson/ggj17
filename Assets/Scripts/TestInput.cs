using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInput : MonoBehaviour
{
	InputManager m_input;

	void Start ()
	{
		m_input = Systems.Instance.Input;
	}
	
	void Update ()
	{
		
	}

	private void OnGUI()
	{
		GUILayout.Label("sdfg: " + m_input.GetControllerCount());
		var c = m_input.GetController(0);
		GUILayout.Label("X: " + c.GetJoystick(Xbox360ControllerJoystickId.Left).x);
		GUILayout.Label("Y: " + c.GetJoystick(Xbox360ControllerJoystickId.Left).y);
	}
}
