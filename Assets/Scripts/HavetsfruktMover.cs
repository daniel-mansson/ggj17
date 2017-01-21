using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HavetsfruktMover : MonoBehaviour {

    public float speed;
    public float maxAngle;

	// Use this for initialization
	void Start () {
        speed = Random.Range(0.10f, 0.30f);
        maxAngle = 8.0f;
	}
	
	// Update is called once per frame
	void Update () {
        var rotation = new Vector3(0, 0, 1.0f);

        if (transform.eulerAngles.z > maxAngle)
        {
            speed *= -1;
        }
        else if (transform.eulerAngles.z < -maxAngle)
        {
            speed *= -1;
        }

        transform.Rotate(rotation * speed);
	}
}
