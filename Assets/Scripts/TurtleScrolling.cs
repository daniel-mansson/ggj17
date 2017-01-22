using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleScrolling : MonoBehaviour {

    public float speed;
    private float maxLeft;
	// Use this for initialization
	void Start () {
        maxLeft = -240.0f;
	}
	
	// Update is called once per frame
	void Update () {
        var movement = new Vector3(-1.0f, 0, 0);

        transform.Translate(movement * speed);

        if (transform.position.x < maxLeft)
        {
            transform.Translate(480.0f, 0, 0);
        }
	}
}
