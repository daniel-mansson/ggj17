using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
	public List<int> m_activePlayers = new List<int>();
	
	public int Count { get { return m_activePlayers.Count; } }

	public bool Join(int id)
	{
		if (!IsJoined(id))
		{
			m_activePlayers.Add(id);
			return true;
		}

		return false;
	}

	public bool Unjoin(int id)
	{
		if (IsJoined(id))
		{
			m_activePlayers.Remove(id);
			return true;
		}

		return false;
	}

	public bool IsJoined(int id)
	{
		return m_activePlayers.Contains(id);
	}
}
