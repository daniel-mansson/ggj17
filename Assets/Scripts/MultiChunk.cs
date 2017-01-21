﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MultiChunk : MonoBehaviour {

	[SerializeField] GameObject[] m_foodParts;
	[SerializeField] Rigidbody2D m_poopTemplate;
	[SerializeField] float m_massVariance = 0.1f, m_poopForce = 50;

	int m_eaten = 0;
	bool m_eatingBackwards = false;
	bool m_isGrabbed = false;
	Rigidbody2D m_body;

	void Awake() {
		m_poopTemplate.gameObject.SetActive(false);
		m_body = GetComponent<Rigidbody2D>();
		m_body.mass += Random.value * m_massVariance;
	}

	// returns Vector of where the joint anchor should connect after eating & Vector2.zero when done eating
	public Vector2 Eat(Transform ass) {
		if(m_eaten < m_foodParts.Length - 1) {
			int index = m_eatingBackwards ? m_foodParts.Length - m_eaten - 1 : m_eaten;
			m_foodParts[index].SetActive(false);
			m_eaten++;
			Rigidbody2D poop = Instantiate(m_poopTemplate);
			poop.transform.position = ass.position;
			poop.gameObject.SetActive(true);
			// TODO inherit velocity from ass
			poop.AddForce(-ass.right * m_poopForce, ForceMode2D.Impulse);
			return m_foodParts[index].transform.position;
		} else {
			return Vector2.zero;
		}
	}

	public Rigidbody2D Grabbed(bool backwards) {
		if(!m_isGrabbed) {
			m_eatingBackwards = backwards;
			m_isGrabbed = true;
			return m_body;
		}
		return null;
	}

}
