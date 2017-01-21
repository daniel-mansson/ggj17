using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HavetsfrukterScrolling : MonoBehaviour {
    public float speed = 0.15f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var movement = new Vector3(-1.0f, 0, 0);

        transform.Translate(movement * speed);
	}
}
