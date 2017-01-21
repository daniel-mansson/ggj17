using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MultiChunk : MonoBehaviour {

	[SerializeField] GameObject[] m_foodParts;
	[SerializeField] Rigidbody2D m_poopTemplate;
	[SerializeField] float m_massVariance = 0.1f, m_poopForce = 50;

	int m_eaten = 0, m_nextToBeEaten;
	bool m_eatingBackwards = false;
	bool m_isGrabbed = false;
	Rigidbody2D m_body;
	Mouth m_who;

	void Awake() {
		m_poopTemplate.gameObject.SetActive(false);
		m_body = GetComponent<Rigidbody2D>();
		m_body.mass += Random.value * m_massVariance;
	}

	public Transform CurrentEatThing {
		get {
			if(0 <= m_nextToBeEaten && m_nextToBeEaten < m_foodParts.Length)
				return m_foodParts[m_nextToBeEaten].transform;
			else
				return null;
		}
	}

	// returns true when done eating
	public bool Eat(Transform ass) {
		int index = m_eatingBackwards ? m_foodParts.Length - m_eaten - 1 : m_eaten;
		m_foodParts[index].SetActive(false);
		m_eaten++;
		m_nextToBeEaten = m_eatingBackwards ? m_foodParts.Length - m_eaten - 1 : m_eaten;
		Rigidbody2D poop = Instantiate(m_poopTemplate);
		poop.transform.position = ass.position;
		poop.gameObject.SetActive(true);
		// TODO inherit velocity from ass
		poop.AddForce(-ass.right * m_poopForce, ForceMode2D.Impulse);
		return m_eaten == m_foodParts.Length;
	}

	public Rigidbody2D Grabbed(bool backwards, Mouth who) {
		if(!m_isGrabbed) {
			m_who = who;
			m_eatingBackwards = backwards;
			if(backwards)
				m_nextToBeEaten = m_foodParts.Length -1;
			m_isGrabbed = true;
			return m_body;
		}
		return null;
	}

}
