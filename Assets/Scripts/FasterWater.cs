using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FasterWater : MonoBehaviour {

	[SerializeField] float m_fasterPerEat = 1.1f;

	BuoyancyEffector2D m_effector;
	int m_lasteat;
	float m_originalFlow;

	void Start () {
		m_effector = GetComponent<BuoyancyEffector2D>();
		m_lasteat = EatCounter.NFoodsDestroyed();
		m_originalFlow = m_effector.flowMagnitude;
	}
	
	void Update () {
		int k = EatCounter.NFoodsDestroyed();
		if(m_lasteat != k) {
			m_lasteat = k;
			m_effector.flowMagnitude = m_originalFlow * Mathf.Pow(m_fasterPerEat, k);
		}
	}

}
