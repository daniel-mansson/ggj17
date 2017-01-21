using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateHandler : MonoBehaviour
{
	public enum State
	{
		Menu,
		Game
	}

	public State m_state;
	TransitionSystem m_transition;

	public void Start()
	{
		m_transition = Systems.Instance.Transition;
#pragma warning disable CS0618 // Type or member is obsolete
		m_state = Application.loadedLevelName == "TheMenu" ? State.Menu : State.Game;
#pragma warning restore CS0618 // Type or member is obsolete
	}

	public Dictionary<State, string> m_stateToScene = new Dictionary<State, string>()
	{
		{ State.Menu, "TheMenu" },
		{ State.Game, "TheRealGame" }
	};

	public void MoveToState(State state)
	{
		if (m_state != state)
		{
			m_state = state;

			StartCoroutine(StateSeq(m_state));
		}
	}

	IEnumerator StateSeq(State toState)
	{
		bool done = false;
		m_transition.ShowLoading(() => 
		{
			done = true;
		});

		while (!done)
			yield return null;

		yield return null;
		yield return null;
		yield return null;

		SceneManager.LoadScene(m_stateToScene[toState]);

		yield return null;
		yield return null;
		yield return null;

		done = false;
		m_transition.HideLoading(() =>
		{
			done = true;
		});

		while (!done)
			yield return null;
	}
}
