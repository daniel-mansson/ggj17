using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class WhaleFish : MonoBehaviour
{
	public float m_force;
	public int m_playerId = 0;
	public float m_rotScale = 0f;
	public float m_rotTorque = 1f;
	public float m_deadFloatForce = 3f;
	public Mouth m_mouth;
	public GameObject m_deathParticles;
	public bool m_isAI = false;

	public Rigidbody2D m_body;
	public Controller m_controller;
	float m_targetRot = 0f;
	bool m_dead = false;

	public bool IsDead { get { return m_dead; } }
	public event System.Action<WhaleFish> OnDeath;

	void Awake()
	{
		m_deathParticles.SetActive(false);
		ReplaceSprites();
		m_body = GetComponent<Rigidbody2D>();
	}

	void Start ()
	{
		ReplaceSprites();
		m_controller = Systems.Instance.Input.GetController(m_playerId);
	}

	void ReplaceSprites()
	{
		var fishSprites = Resources.LoadAll<Sprite>("fish" + m_playerId);

		var allSprites = GetComponentsInChildren<SpriteRenderer>();
		foreach (var sr in allSprites)
		{
			var match = fishSprites.FirstOrDefault(f => f.name == sr.sprite.name);
			if (match != null)
			{
				sr.sprite = match;
			}
		}
	}

	Vector2 m_aiTarget = Vector2.zero;
	float m_aiTargetTimer = 0;
	float m_aiSpeed = 1f;

	void GetRandomAiTarget()
	{
		m_aiTarget.x = Random.Range(-33f, 33f);
		m_aiTarget.y = Random.Range(-19f, 19f);
		m_aiSpeed = Random.Range(0.5f, 1f);
		m_aiTargetTimer = Random.Range(3f, 10f);
	}

	void FixedUpdate ()
	{
		if(!m_dead) {
			var joy = m_controller.GetJoystick(Xbox360ControllerJoystickId.Left);

			if (m_isAI)
			{
				m_aiTargetTimer -= Time.deltaTime;
				if (m_aiTargetTimer < 0)
					GetRandomAiTarget();

				var d = m_aiTarget - m_body.position;
				d.Normalize();
				d *= m_aiSpeed;
				joy = d;
			}

			m_body.AddForce(m_force * joy);
			m_targetRot = m_body.velocity.y * m_rotScale;
		} else {
			m_body.AddForce(Vector2.up * m_deadFloatForce);
		}
		m_body.AddTorque((m_targetRot - m_body.rotation) * m_rotTorque);
	}

	float m_aiEatTimer = 0f;
	void Update() {
		if (m_isAI)
		{
			m_aiEatTimer -= Time.deltaTime;
			if (m_aiEatTimer < 0f)
			{
				m_aiEatTimer = Random.Range(0.5f, 1.3f);
				m_mouth.Eat();
			}
		}
		else
		{
			if (m_controller.GetButtonDown(Xbox360ControllerButtonId.A))
			{
				m_mouth.Eat();
			}
		}
	}

	public void SetDead() {
		if (!m_dead)
		{
			m_dead = true;
			m_deathParticles.SetActive(true);
			m_mouth.enabled = false;
			m_targetRot = 180;

			if (OnDeath != null)
			{
				OnDeath(this);
			}
		}
	}

	public void Drop() {
		m_mouth.ForceDrop();
	}

}
