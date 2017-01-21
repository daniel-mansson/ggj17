using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
	public float m_speed = 0, m_speedupPerEat = 1.05f;
	public Material m_material;

	MeshRenderer m_renderer;
	Material m_materialClone;

	void Start () {
		m_materialClone = new Material(m_material);
		m_renderer = GetComponent<MeshRenderer>();
		m_renderer.material = m_materialClone;
	}
	
	void Update () {
		m_materialClone.mainTextureOffset += Vector2.right * Time.deltaTime * m_speed * Mathf.Pow(m_speedupPerEat, EatCounter.NFoodsDestroyed());
		m_renderer.material = m_materialClone;
	}
}
