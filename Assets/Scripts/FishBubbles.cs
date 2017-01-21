using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBubbles : MonoBehaviour {

    private ParticleSystem ps;

	// Use this for initialization
	void Start () {
        ps = GetComponent<ParticleSystem>();

        // Randomize the burst intevals
        ps.emission.SetBursts(new ParticleSystem.Burst[] {
            new ParticleSystem.Burst(Random.Range(0.0f, 3.0f), 3, 11),
            new ParticleSystem.Burst(Random.Range(4.0f, 7.0f), 3, 11)
        });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
