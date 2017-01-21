using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public interface Controller
{
	Vector2 GetJoystick(int id);
	float GetTrigger(int id);
	bool GetButton(int id);
	bool GetButtonDown(int id);
	bool GetButtonUp(int id);
}

public class ControllerImpl : Controller
{
	ControllerDefinition m_definition;
	int m_internalId = 0;

	List<Vector2> m_joysticks = new List<Vector2>();
	List<float> m_triggers = new List<float>();
	List<bool> m_prevButtonStates = new List<bool>();
	List<bool> m_buttonStates = new List<bool>();

	public ControllerImpl(ControllerDefinition definition, int internalId)
	{
		m_definition = definition;
		m_internalId = internalId;

		for (int i = 0; i < m_definition.Data.Joysticks.Length; i++)
			m_joysticks.Add(Vector2.zero);

		for (int i = 0; i < m_definition.Data.Triggers.Length; i++)
			m_triggers.Add(0.0f);

		for (int i = 0; i < m_definition.Data.Buttons.Length; i++)
		{
			m_buttonStates.Add(false);
			m_prevButtonStates.Add(false);
		}
	}

	public void Update()
	{
		for (int i = 0; i < m_joysticks.Count; i++)
		{
			var joystick = m_definition.Data.Joysticks[i];

			Vector2 raw;
			raw.x = Input.GetAxisRaw("axis_" + m_internalId + "_" + joystick.HorizontalAxis);
			raw.y = -Input.GetAxisRaw("axis_" + m_internalId + "_" + joystick.VerticalAxis);

			float len = raw.magnitude;
			Vector2 adjusted = raw.normalized * Mathf.Clamp01((len - (float)joystick.Deadzone) / (1.0f - (float)joystick.Deadzone));

			m_joysticks[i] = adjusted;
		}

		for (int i = 0; i < m_triggers.Count; i++)
		{
			var trigger = m_definition.Data.Triggers[i];

			float raw = Input.GetAxisRaw("axis_" + m_internalId + "_" + trigger.Axis);
			float adjusted = raw * Mathf.Clamp01((Mathf.Abs(raw) - (float)trigger.Deadzone) / (1.0f - (float)trigger.Deadzone));

			m_triggers[i] = adjusted;
		}

		for (int i = 0; i < m_buttonStates.Count; i++)
		{
			m_prevButtonStates[i] = m_buttonStates[i];
			m_buttonStates[i] = Input.GetButton("button_" + m_internalId + "_" + m_definition.Data.Buttons[i]);
		}

		HackyKeyboardInput();
	}

	void HackyKeyboardInput()
	{
		if (m_internalId == 3)
		{
			Vector2 joyMod = Vector2.zero;
			if (Input.GetKey(KeyCode.D))
				joyMod += Vector2.right;
			if (Input.GetKey(KeyCode.A))
				joyMod += Vector2.left;
			if (Input.GetKey(KeyCode.W))
				joyMod += Vector2.up;
			if (Input.GetKey(KeyCode.S))
				joyMod += Vector2.down;

			m_joysticks[Xbox360ControllerJoystickId.Left] += joyMod;
			if (joyMod.sqrMagnitude > 1f)
			{
				m_joysticks[Xbox360ControllerJoystickId.Left].Normalize();
			}

			if (Input.GetKeyDown(KeyCode.LeftShift))
				m_buttonStates[Xbox360ControllerButtonId.A] = true;
			if (Input.GetKeyDown(KeyCode.E))
				m_buttonStates[Xbox360ControllerButtonId.Start] = true;
			if (Input.GetKeyDown(KeyCode.Q))
				m_buttonStates[Xbox360ControllerButtonId.B] = true;
		}
		else if(m_internalId == 4)
		{
			Vector2 joyMod = Vector2.zero;
			if (Input.GetKey(KeyCode.RightArrow))
				joyMod += Vector2.right;
			if (Input.GetKey(KeyCode.LeftArrow))
				joyMod += Vector2.left;
			if (Input.GetKey(KeyCode.UpArrow))
				joyMod += Vector2.up;
			if (Input.GetKey(KeyCode.DownArrow))
				joyMod += Vector2.down;

			m_joysticks[Xbox360ControllerJoystickId.Left] += joyMod;
			if (joyMod.sqrMagnitude > 1f)
			{
				m_joysticks[Xbox360ControllerJoystickId.Left].Normalize();
			}

			if (Input.GetKeyDown(KeyCode.RightShift))
				m_buttonStates[Xbox360ControllerButtonId.A] = true;
			if (Input.GetKeyDown(KeyCode.Return))
				m_buttonStates[Xbox360ControllerButtonId.Start] = true;
			if (Input.GetKeyDown(KeyCode.Backspace))
				m_buttonStates[Xbox360ControllerButtonId.B] = true;
		}
	}

	public Vector2 GetJoystick(int id)
	{
		if (id < 0 || id >= m_joysticks.Count)
			return Vector2.zero;

		return m_joysticks[id];
	}

	public float GetTrigger(int id)
	{
		if (id < 0 || id >= m_triggers.Count)
			return 0.0f;

		return m_triggers[id];
	}

	public bool GetButton(int id)
	{
		if (id < 0 || id >= m_buttonStates.Count)
			return false;

		return m_buttonStates[id];
	}

	public bool GetButtonDown(int id)
	{
		if (id < 0 || id >= m_buttonStates.Count)
			return false;

		return m_buttonStates[id] && !m_prevButtonStates[id];
	}

	public bool GetButtonUp(int id)
	{
		if (id < 0 || id >= m_buttonStates.Count)
			return false;

		return !m_buttonStates[id] && m_prevButtonStates[id];
	}


}
