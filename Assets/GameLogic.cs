using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
	public WhaleFish m_fishPrefab;
	public List<Transform> m_spawnPos;

	bool m_isGameOver = false;
	List<WhaleFish> m_fish = new List<WhaleFish>();
	PlayerInfo m_players;
	// Use this for initialization
	void Start () {
		m_players = Systems.Instance.Players;

		//If game is started from Game scene.
		if (m_players.Count == 0)
		{
			//Add mock players
			m_players.m_activePlayers.Add(0);
			m_players.m_activePlayers.Add(1);
			m_players.m_activePlayers.Add(2);
			m_players.m_activePlayers.Add(3);
		}

		foreach (var id in m_players.m_activePlayers)
		{
			var fish = (WhaleFish)Instantiate(m_fishPrefab, m_spawnPos[id].position, Quaternion.identity);
			fish.m_playerId = id;
			fish.OnDeath += OnFishDied;
			m_fish.Add(fish);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	int GetNumFishAlive()
	{
		return m_fish.Count(f => !f.IsDead);
	}

	void OnFishDied(WhaleFish fish)
	{
		if (GetNumFishAlive() == 1)
		{
			SetGameOver(m_fish.First(f => !f.IsDead));
		}
	}

	void SetGameOver(WhaleFish winnerFish)
	{
		StartCoroutine(GameOverSeq(winnerFish));
	}

	IEnumerator GameOverSeq(WhaleFish winnerFish)
	{
		yield return new WaitForSeconds(1f);

		Systems.Instance.State.MoveToState(StateHandler.State.Menu);
	}
}
