using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeAnimation : MonoBehaviour
{
	public GameObject m_root;
	public GameObject m_body;
	public GameObject m_top;
	public GameObject m_jaw;
	public GameObject m_back1;
	public GameObject m_back2;

	[Header("body")]
	public float m_bodyFreq = 0f;
	public float m_bodyScale = 0f;
	[Header("back")]
	public float m_backFreq = 0f;
	public float m_backScale = 0f;
	[Header("jaw")]
	public float m_jawSpeed = 0f;
	public float m_jawRestPos = 0f;
	[Header("top")]
	public float m_topSpeed = 0f;
	public float m_topRestPos = 0f;
	[Header("back1")]
	public float m_back1Speed = 0f;
	public float m_back1RestPos = 0f;
	[Header("back2")]
	public float m_back2Speed = 0f;
	public float m_back2RestPos = 0f;

	float m_bodyTimer = 0;
	float m_backTimer = 0;
	float m_jawPos;
	float m_topPos;
	float m_back1Pos;
	float m_back2Pos;

	public float m_scale = 1f;

	WhaleFish m_fish;
	MenuFish m_menu;

	void Start ()
	{
		m_fish = GetComponent<WhaleFish>();
		m_menu = GetComponent<MenuFish>();

		m_jawPos = m_jawRestPos;
		m_topPos = m_topRestPos;
	}
	
	void Update ()
	{
		m_scale = 1f;
		if (m_fish.IsDead)
			m_scale = 0f;
		if (m_menu.m_isInMenu && !m_menu.IsReadyToPlay)
			m_scale = 0f;

		m_scale *= (m_fish.m_body.velocity.x + 25f + m_fish.m_body.velocity.y) / 25f;

		m_bodyTimer += Time.deltaTime * m_bodyFreq * m_scale;
		m_backTimer += Time.deltaTime * m_backFreq * m_scale;

		m_root.transform.localScale = new Vector3(1f + Mathf.Sin(m_bodyTimer) * m_bodyScale , 1f + Mathf.Sin(m_bodyTimer + Mathf.PI) * m_bodyScale, 1f);
		m_back1.transform.localScale = new Vector3(1f + Mathf.Sin(m_backTimer) * m_backScale , 1f, 1f);
		m_back2.transform.localScale = new Vector3(1f + Mathf.Sin(m_backTimer) * m_backScale , 1f, 1f);

		m_jawPos += (m_jawRestPos - m_jawPos) * Time.deltaTime * m_jawSpeed;
		m_topPos += (m_topRestPos - m_topPos) * Time.deltaTime * m_topSpeed ;
		m_back1Pos += (m_back1RestPos - m_back1Pos) * Time.deltaTime * m_back1Speed;
		m_back2Pos += (m_back2RestPos - m_back2Pos) * Time.deltaTime * m_back2Speed ;

		m_jaw.transform.localRotation = Quaternion.Euler(0,0, m_jawPos);
		m_top.transform.localRotation = Quaternion.Euler(0, 0, m_topPos);
		m_back1.transform.localRotation = Quaternion.Euler(0, 0, m_back1Pos);
		m_back2.transform.localRotation = Quaternion.Euler(0, 0, m_back2Pos);

		if (Input.GetKeyDown(KeyCode.U))
			Chew();
		if (Input.GetKeyDown(KeyCode.P))
			Poop();
	}

	public void Chew()
	{
		m_jawPos = 0;
		m_topPos = 0;
	}

	public void Poop()
	{
		m_back1Pos = m_back1RestPos * 0.0f;
		m_back2Pos = m_back2RestPos * 0.0f;
	}
}
