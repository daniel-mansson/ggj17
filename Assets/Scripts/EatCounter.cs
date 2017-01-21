using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatCounter : MonoBehaviour {

	static EatCounter s_instance;

	int m_nFoodsEaten = 0;

	void Awake() {
		s_instance = this;
	}

	public static void FoodEatenCompletely() {
		s_instance.m_nFoodsEaten++;
	}

	public static int NFoodsDestroyed() {
		return s_instance.m_nFoodsEaten;
	}
}
