using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
	public List<GameObject> m_buttons;
	public GameObject m_startText;

	// Use this for initialization
	void Start ()
	{
		int id = 0;
		foreach (var go in m_buttons)
		{
			SetState(id, !Systems.Instance.Players.IsJoined(id));
			++id;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetState(int id, bool state)
	{
		m_buttons[id].SetActive(state);
		m_startText.SetActive(Systems.Instance.Players.Count > 1);
	}
}
