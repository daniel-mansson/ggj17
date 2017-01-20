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

	void Awake()
	{
		if (s_instance == null)
		{
			s_instance = this;
			DontDestroyOnLoad(this.gameObject);

			m_systems = Instantiate(m_systemsPrefab);
			m_systems.transform.parent = transform;

			Input = m_systems.GetComponent<InputManager>();
		}
		else
		{
			Destroy(this.gameObject);
		}
	}
}
