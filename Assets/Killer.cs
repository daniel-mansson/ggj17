using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Destroy(collision.transform.root.gameObject);
	}
}
