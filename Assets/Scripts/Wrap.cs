using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Wrap : MonoBehaviour {

	[SerializeField] Transform m_sendTo;

	void OnTriggerEnter2D(Collider2D other) {
		Transform root = other.transform;
		while(root.parent != null) {
			root = root.parent;
		}
		var a = root.position;
		a.x = m_sendTo.position.x;
		root.position = a;
	}

}
