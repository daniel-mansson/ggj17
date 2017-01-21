using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mouth : MonoBehaviour {

	[SerializeField] HingeJoint2D m_joint;
	[SerializeField] Transform m_ass;
	MultiChunk m_inMouth;

	void Start() {
		StartCoroutine(AutoEat());
		m_joint.enabled = false;
	}

	IEnumerator AutoEat() {
		while(true) {
			yield return new WaitForSeconds(1);
			if(m_inMouth)
				Eat();
		}
	}

	void Eat() {
		var anchorPos = m_inMouth.Eat(m_ass);
		if(anchorPos == Vector2.zero) {
			Destroy(m_inMouth.gameObject);
			m_inMouth = null;
			m_joint.connectedBody = null;
			m_joint.enabled = false;
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
					m_joint.enabled = true;
				}
			}
		}
	}

}
