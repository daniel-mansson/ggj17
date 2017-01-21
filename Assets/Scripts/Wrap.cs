using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Wrap : MonoBehaviour {

	[SerializeField] Transform m_sendTo;

	void OnTriggerEnter2D(Collider2D other) {
		var root = other.attachedRigidbody.transform;
		var a = root.position;
		a.x = m_sendTo.position.x;
		root.position = a;
	}

}
