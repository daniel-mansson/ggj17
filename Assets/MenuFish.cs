using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFish : MonoBehaviour
{
	public bool m_isInMenu = false;
	WhaleFish m_fish;
	MenuButtons m_buttons;
	PlayerInfo m_playerInfo;
	Controller m_controller;

	// Use this for initialization
	void Start () {
		m_fish = GetComponent<WhaleFish>();
		m_playerInfo = Systems.Instance.Players;
		m_controller = Systems.Instance.Input.GetController(m_fish.m_playerId);

		if (m_isInMenu)
		{
			m_fish.enabled = false;
			m_buttons = GameObject.FindObjectOfType<MenuButtons>();
			SetShadow(!m_playerInfo.IsJoined(m_fish.m_playerId));
		}
		else
		{
		}
	}

	void SetShadow(bool useShadow)
	{
		var allSprites = m_fish.GetComponentsInChildren<SpriteRenderer>();
		foreach (var s in allSprites)
		{
			s.material.SetColor("_Color", useShadow ? Color.black : Color.white);
		}
	}
	
	void Update ()
	{
		if (m_controller.GetButtonDown(Xbox360ControllerButtonId.A))
		{
			if (m_playerInfo.Join(m_fish.m_playerId))
			{
				m_buttons.SetState(m_fish.m_playerId, false);
				SetShadow(false);
			}
		}
		if (m_controller.GetButtonDown(Xbox360ControllerButtonId.B))
		{
			if (m_playerInfo.Unjoin(m_fish.m_playerId))
			{
				m_buttons.SetState(m_fish.m_playerId, true);
				SetShadow(true);
			}
		}
		if (m_controller.GetButtonDown(Xbox360ControllerButtonId.Start))
		{
			if (m_playerInfo.IsJoined(m_fish.m_playerId))
			{
				Systems.Instance.State.MoveToState(StateHandler.State.Game);
			}
		}
	}
}
