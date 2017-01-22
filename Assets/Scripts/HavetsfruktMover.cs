using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HavetsfruktMover : MonoBehaviour {

    public float speed;
    public int direction;
    public float maxAngle;

	// Use this for initialization
	void Start () {
        speed = Random.Range(0.05f, 0.1f);
        maxAngle = 8.0f;
        direction = (Random.Range(0, 1) * 2) - 1;
	}
	
	// Update is called once per frame
	void Update () {
        var rotation = new Vector3(0, 0, 1.0f);

        if (transform.eulerAngles.z > maxAngle)
        {
            direction *= -1;
        }
        else if (transform.eulerAngles.z < -maxAngle)
        {
            direction *= -1;
        }

        transform.Rotate(rotation * speed * direction);
	}
}
