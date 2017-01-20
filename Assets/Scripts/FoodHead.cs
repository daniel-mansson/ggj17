using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FoodHead : MonoBehaviour {

	public bool Backwards = false;
	[HideInInspector] public MultiChunk Parent;

	void Awake() {
		Parent = transform.parent.GetComponent<MultiChunk>();
	}

}
