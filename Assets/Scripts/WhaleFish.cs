using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleFish : MonoBehaviour
{
	public float m_force;
	public int m_playerId = 0;
	public float m_rotScale = 0f;

	Rigidbody2D m_body;
	Controller m_controller;

	void Start ()
	{
		m_controller = Systems.Instance.Input.GetController(m_playerId);
		m_body = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate ()
	{
		var joy = m_controller.GetJoystick(Xbox360ControllerJoystickId.Left);
		m_body.AddForce(m_force * joy);

		m_body.rotation = m_body.velocity.y * m_rotScale;
	}
}
