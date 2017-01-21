using UnityEngine;
using System.Collections;

public class Systems : MonoBehaviour
{
	public static Systems Instance
	{
		get
		{
			return s_instance;
		}
	}

	static Systems s_instance;

	[SerializeField]
	GameObject m_systemsPrefab;

	GameObject m_systems;

	public InputManager Input { get; private set; }
	public StateHandler State { get; private set; }
	public TransitionSystem Transition { get; private set; }
	public PlayerInfo Players { get; private set; }

	void Awake()
	{
		if (s_instance == null)
		{
			s_instance = this;
			DontDestroyOnLoad(this.gameObject);

			m_systems = Instantiate(m_systemsPrefab);
			m_systems.transform.parent = transform;

			Input = m_systems.GetComponent<InputManager>();
			State = m_systems.GetComponent<StateHandler>();
			Transition = m_systems.GetComponent<TransitionSystem>();
			Players = m_systems.GetComponent<PlayerInfo>();
		}
		else
		{
			Destroy(this.gameObject);
		}
	}
}
