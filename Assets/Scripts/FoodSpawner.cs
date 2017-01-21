﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour {

	[System.Serializable]
	struct FoodSpawn {
		public MultiChunk Template;
		public float BaseChance, ChanceMultiplierForEachEat;
		public int CountOffset;
	}
	
	[SerializeField] FoodSpawn[] m_foods;
	[SerializeField] float m_height = 10, m_baseSpawnRate = 5, m_spawnRateMultiplerForEachEat = 0.95f, m_throwDownStrength = 1000, m_maxTorque = 100;
	[SerializeField] Vector2 m_box = 10 * (Vector2.one - Vector2.one / 2);

	void Start() {
		foreach(var spawn in m_foods) {
			spawn.Template.gameObject.SetActive(false);
		}
		StartCoroutine(Spawner());
	}

	IEnumerator Spawner() {
		while(true) {
			yield return new WaitForSeconds(m_baseSpawnRate * Mathf.Pow(m_spawnRateMultiplerForEachEat, EatCounter.NFoodsDestroyed()));
			foreach(var spawn in m_foods) {
				if(spawn.CountOffset <= EatCounter.NFoodsDestroyed()) {
					float spawnChance = spawn.BaseChance * Mathf.Pow(spawn.ChanceMultiplierForEachEat, EatCounter.NFoodsDestroyed());
					if(Random.value > spawnChance) {
						MultiChunk food = Instantiate(spawn.Template);
						food.transform.position = transform.position + Vector3.right * Random.Range(-0.5f, 0.5f) * m_box.x;
						food.Initialize(transform.position.y - m_height + Random.Range(-0.5f, 0.5f) * m_box.y);
						food.gameObject.SetActive(true);
						var body = food.GetComponent<Rigidbody2D>();
						body.AddForce(Vector2.down * m_throwDownStrength, ForceMode2D.Impulse);
						body.AddTorque(m_maxTorque * Random.Range(-1f, 1f), ForceMode2D.Impulse);
					}
				}
			}
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.cyan;
		Gizmos.DrawLine(transform.position - Vector3.right * m_box.x / 2, transform.position + Vector3.right * m_box.x / 2);
		Gizmos.DrawWireCube(transform.position - Vector3.up * m_height, m_box);
	}

}
