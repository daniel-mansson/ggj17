﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HavetsfrukterScrolling : MonoBehaviour {
    private float speed = 0.02f;
    private float maxLeft;

	// Use this for initialization
	void Start () {
        speed = 0.1f;
        maxLeft = -70.0f;
	}
	
	// Update is called once per frame
	void Update () {
        var movement = new Vector3(-1.0f, 0, 0);

        transform.Translate(movement * speed);

        if (transform.position.x < maxLeft)
        {
            transform.Translate(160.0f, 0, 0);
        }
	}
}
