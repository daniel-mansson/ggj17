using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
	public float m_speed = 0;
	public Material m_material;

	MeshRenderer m_renderer;
	Material m_materialClone;
	// Use this for initialization
	void Start () {
		m_materialClone = new Material(m_material);
		m_renderer = GetComponent<MeshRenderer>();
		m_renderer.material = m_materialClone;
	}
	
	// Update is called once per frame
	void Update () {
		m_materialClone.mainTextureOffset += Vector2.right * Time.deltaTime * m_speed;
		m_renderer.material = m_materialClone;
	}
}
