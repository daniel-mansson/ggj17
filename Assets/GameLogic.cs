using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
	public WhaleFish m_fishPrefab;
	public List<Transform> m_spawnPos;
	public Camera m_camera;

	bool m_isGameOver = false;
	List<WhaleFish> m_fish = new List<WhaleFish>();
	PlayerInfo m_players;
	// Use this for initialization
	void Start () {
		m_players = Systems.Instance.Players;
		Time.timeScale = 1f;

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
			fish.m_mouth.OnTimeToDie += OnFishAtePoop;
			m_fish.Add(fish);
		}

		m_cameraStartPos = m_camera.transform.position;
		m_cameraStartSize = m_camera.orthographicSize;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	int GetNumFishAlive()
	{
		return m_fish.Count(f => !f.IsDead);
	}

	void OnFishAtePoop(Mouth fish)
	{
		if (m_currentPoopFish == null)
			StartCoroutine(AtePopSeq(fish));
	}

	[Header("poop cam")]
	public float m_zoomAmount = 5;
	public float m_zoomTime = 0.4f;
	public float m_waitTime = 0.5f;
	public AnimationCurve m_zoomCurve;
	public AnimationCurve m_moveCurve;

	Vector3 m_cameraStartPos;
	float m_cameraStartSize;

	Mouth m_currentPoopFish = null;
	IEnumerator AtePopSeq(Mouth fish)
	{
		m_currentPoopFish = fish;

		float timer;

		timer = 0f;
		while (timer < m_zoomTime)
		{
			float t = timer / m_zoomTime;
			Time.timeScale = 1f - t;

			m_camera.transform.position = (Vector3)Vector2.Lerp(m_cameraStartPos, m_currentPoopFish.transform.position, m_moveCurve.Evaluate( t)) + Vector3.forward * m_cameraStartPos.z;

			m_camera.orthographicSize = Mathf.Lerp(m_cameraStartSize, m_zoomAmount, m_zoomCurve.Evaluate(t));

			yield return null;
			timer += Time.unscaledDeltaTime;
		}
		Time.timeScale = 0f;

		timer = 0;
		while (timer < m_waitTime)
		{
			m_camera.transform.position = (Vector3)(Vector2)m_currentPoopFish.transform.position + Vector3.forward * m_cameraStartPos.z;

			yield return null;
			timer += Time.unscaledDeltaTime;
		}

		timer = 0f;
		while (timer < m_zoomTime)
		{
			float t = timer / m_zoomTime;
			Time.timeScale = t;

			m_camera.orthographicSize = Mathf.Lerp(m_cameraStartSize, m_zoomAmount, m_zoomCurve.Evaluate(1f - t));
			m_camera.transform.position = (Vector3)Vector2.Lerp(m_cameraStartPos, m_currentPoopFish.transform.position, m_moveCurve.Evaluate( 1f - t)) + Vector3.forward * m_cameraStartPos.z;
			yield return null;

			timer += Time.unscaledDeltaTime;
		}
		Time.timeScale = 1f;
		m_camera.transform.position = m_cameraStartPos;

		m_currentPoopFish = null;
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
