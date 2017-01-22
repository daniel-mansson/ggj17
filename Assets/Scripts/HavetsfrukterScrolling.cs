using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HavetsfrukterScrolling : MonoBehaviour {
    private float speed = 0.04f;
    private float maxLeft;
    private float speedMagnifier;
    private int lastEat;

	// Use this for initialization
	void Start () {
        maxLeft = -70.0f;
        speedMagnifier = 1.02f;
        lastEat = 0;
	}
	
	// Update is called once per frame
	void Update () {
        var movement = new Vector3(-1.0f, 0, 0);
        var noOfFoodsEaten = EatCounter.NFoodsDestroyed();

        if (noOfFoodsEaten != lastEat)
        {
            lastEat = noOfFoodsEaten;
            speed *= Mathf.Pow(speedMagnifier, noOfFoodsEaten);
        }

        transform.Translate(movement * speed);

        if (transform.position.x < maxLeft)
        {
            transform.Translate(160.0f, 0, 0);
        }
	}
}
