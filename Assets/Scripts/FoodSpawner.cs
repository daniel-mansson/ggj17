using System.Collections;
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
	[SerializeField] float m_width = 10, m_baseSpawnRate = 5, m_spawnRateMultiplerForEachEat = 0.95f, m_throwDownStrength = 1000, m_throwVariance = 100, m_maxTorque = 100;

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
						food.transform.position = transform.position + Vector3.right * Random.Range(-0.5f, 0.5f) * m_width;
						food.transform.Rotate(0, 0, 360f * Random.value);
						food.gameObject.SetActive(true);
						var body = food.GetComponent<Rigidbody2D>();
						body.AddForce(Vector2.down * (m_throwDownStrength + Random.Range(-1f, 1f) * m_throwVariance), ForceMode2D.Impulse);
						body.AddTorque(m_maxTorque * Random.Range(-1f, 1f), ForceMode2D.Impulse);
					}
				}
			}
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.cyan;
		Gizmos.DrawLine(transform.position - Vector3.right * m_width / 2, transform.position + Vector3.right * m_width / 2);
	}

}
