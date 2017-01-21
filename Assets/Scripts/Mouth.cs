using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mouth : MonoBehaviour {

	[SerializeField] Transform m_ass;
	[SerializeField] float m_deathTime = 3;

	MultiChunk m_foodInMouth;
	Shit m_shitInMouth;
	bool m_haveThingInMouth = false;

	public void Eat() {
		if(m_haveThingInMouth) {
			bool done = m_foodInMouth.Eat(m_ass);
			if(m_foodInMouth.CurrentEatThing)
				m_foodInMouth.transform.position += transform.position - m_foodInMouth.CurrentEatThing.position;
			if(done) {
				EatCounter.FoodEatenCompletely();
				Drop();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log(other.name + " " + m_haveThingInMouth + " " + m_foodInMouth);
		if(!m_haveThingInMouth) {
			FoodHead foodHead = other.GetComponent<FoodHead>();
			if(foodHead) {
				var body = foodHead.Parent.Grabbed(foodHead.Backwards, this);
				if(body) {
					m_foodInMouth = foodHead.Parent;
					Destroy(foodHead.Parent.GetComponent<Rigidbody2D>());
					foreach (var c in foodHead.Parent.GetComponentsInChildren<Collider2D>())
					{
						c.enabled = false;
					}
					m_foodInMouth.transform.SetParent(transform);
					m_foodInMouth.transform.position += transform.position - m_foodInMouth.CurrentEatThing.position;
				}
				m_haveThingInMouth = true;
			}
			Shit shit = other.GetComponent<Shit>();
			if(shit) {
				Destroy(shit.GetComponent<Rigidbody2D>());
				shit.transform.SetParent(transform);
				shit.transform.position = transform.position;
				m_shitInMouth = shit;
				StartCoroutine(TimeToDie());
				m_haveThingInMouth = true;
			}
		}
	}

	IEnumerator TimeToDie() {
		yield return new WaitForSeconds(m_deathTime);
		var fish = transform.root.GetComponent<WhaleFish>();
		Destroy(m_shitInMouth.gameObject);
		m_shitInMouth = null;
		m_haveThingInMouth = false;
		fish.SetDead();
	}

	public void Drop() {
		Destroy(m_foodInMouth.gameObject);
		m_foodInMouth = null;
		m_haveThingInMouth = false;
	}

}
