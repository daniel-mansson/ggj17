using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mouth : MonoBehaviour {

	[SerializeField] Transform m_ass;
	[SerializeField] float m_deathTime = 3, m_hitstopTimePerEat = 0.01f;
	[SerializeField] int m_hitstopThresholdEats = 5;
	[SerializeField] AudioSource m_eatSound, m_forceDropSound;

	MultiChunk m_foodInMouth;
	Shit m_shitInMouth;
	bool m_haveThingInMouth = false;
	public event System.Action<Mouth> OnTimeToDie;

	public void Eat() {
		if(m_haveThingInMouth && m_foodInMouth) {
			var anim = transform.root.GetComponent<CodeAnimation>();
			anim.Chew();
			anim.Poop();
			m_eatSound.Play();
			bool done = m_foodInMouth.Eat(m_ass);
			if(m_foodInMouth.CurrentEatThing)
				m_foodInMouth.transform.position += transform.position - m_foodInMouth.CurrentEatThing.position;
			if(done) {
				EatCounter.FoodEatenCompletely();
				Drop();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(!m_haveThingInMouth) {
			FoodHead foodHead = other.GetComponent<FoodHead>();
			if(foodHead) {
				var body = foodHead.Parent.Grabbed(foodHead.Backwards, this);
				if(body) {
					m_foodInMouth = foodHead.Parent;
					Destroy(foodHead.Parent.GetComponent<Rigidbody2D>());
					foreach (Transform c in foodHead.Parent.transform)
					{
						if(c.GetComponent<FoodPart>())
							c.gameObject.layer = LayerMask.NameToLayer("Default");
					}
					m_foodInMouth.transform.SetParent(transform);
					m_foodInMouth.transform.position += transform.position - m_foodInMouth.CurrentEatThing.position;
				}
				m_haveThingInMouth = true;
			}
			Shit shit = other.GetComponent<Shit>();
			if(shit) {
				Destroy(shit.GetComponent<Rigidbody2D>());
				shit.transform.SetParent(transform);
				shit.transform.position = transform.position;
				m_shitInMouth = shit;
				StartCoroutine(TimeToDie());
				m_haveThingInMouth = true;
			}
		}
	}

	IEnumerator TimeToDie() {

		if (OnTimeToDie != null)
			OnTimeToDie(this);

		if(EatCounter.NFoodsDestroyed() > m_hitstopThresholdEats) {
			Debug.Log("Hitstop!");
			float originalTimeScale = Time.timeScale;
			//Time.timeScale = 0;
			float clock = Time.realtimeSinceStartup;
			yield return new WaitForSecondsRealtime(m_hitstopTimePerEat * EatCounter.NFoodsDestroyed());
		//	Time.timeScale = originalTimeScale;
		}
		yield return new WaitForSeconds(m_deathTime);
		var fish = transform.root.GetComponent<WhaleFish>();
		Destroy(m_shitInMouth.gameObject);
		m_shitInMouth = null;
		m_haveThingInMouth = false;
		fish.SetDead();
	}

	public void ForceDrop() {
		if(m_foodInMouth) {
			m_forceDropSound.Play();
			Drop();
		}
	}

	void Drop() {
		Destroy(m_foodInMouth.gameObject);
		m_foodInMouth = null;
		m_haveThingInMouth = false;
	}

}
