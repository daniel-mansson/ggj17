using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mouth : MonoBehaviour {

	FixedJoint2D m_joint;
	MultiChunk m_inMouth;

	void Start() {
		m_joint = GetComponent<FixedJoint2D>();
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
		var anchorPos = m_inMouth.Eat();
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
					m_joint.connectedBody = body;
					m_joint.connectedAnchor = foodHead.transform.position;
				}
			}
		}
	}

}
