using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mouth : MonoBehaviour {

	[SerializeField] Transform m_ass;
	MultiChunk m_inMouth;

	void Start() {
		StartCoroutine(AutoEat());
	}

	IEnumerator AutoEat() {
		while(true) {
			yield return new WaitForSeconds(1);
			if(m_inMouth)
				Eat();
		}
	}

	void Eat() {
		Vector2 anchorPos = m_inMouth.Eat(m_ass);
		m_inMouth.transform.position += transform.position - m_inMouth.CurrentEatThing.position;
		if(anchorPos == Vector2.zero) {
			Destroy(m_inMouth.gameObject);
			m_inMouth = null;
			EatCounter.FoodEatenCompletely();
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(!m_inMouth) {
			FoodHead foodHead = other.GetComponent<FoodHead>();
			if(foodHead) {
				var body = foodHead.Parent.Grabbed(foodHead.Backwards);
				if(body) {
					m_inMouth = foodHead.Parent;
					Destroy(foodHead.Parent.GetComponent<Rigidbody2D>());
					m_inMouth.transform.SetParent(transform);
					m_inMouth.transform.position += transform.position - m_inMouth.CurrentEatThing.position;
				}
			}
		}
	}

}
