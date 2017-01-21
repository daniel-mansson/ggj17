using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPart : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D other) {
		Shit shit = other.collider.GetComponent<Shit>();
		if(shit) {
			WhaleFish fish = transform.root.GetComponent<WhaleFish>();
			if(fish) {
				fish.Drop();
			}
		}
	}

}
