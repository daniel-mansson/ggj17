using UnityEngine;
using System.Collections;
using System;

public class RewiredController : Controller
{
	Rewired.Player m_player;

	public RewiredController(int id)
	{
		m_player = Rewired.ReInput.players.GetPlayer(id);
	}

	

	public bool GetButton(int id)
	{
		return m_player.GetButton(id);
	}

	public bool GetButtonDown(int id)
	{
		return m_player.GetButtonDown(id);
	}

	public bool GetButtonUp(int id)
	{
		return m_player.GetButtonUp(id);
	}

	public Vector2 GetJoystick(int id)
	{
		return m_player.GetAxis2D(id, id + 1);
	}

	public float GetTrigger(int id)
	{
		return m_player.GetAxis(id);
	}
}
