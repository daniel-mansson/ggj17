using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionSystem : MonoBehaviour
{
	bool m_isShowingLoading = false;
	public Image m_image;
	public float m_transitionTime = 1f;

	[ContextMenu("show")]
	void Show()
	{
		ShowLoading(()=> { });
	}

	[ContextMenu("hide")]
	void Hide()
	{
		HideLoading(() => { });
	}

	public void ShowLoading(System.Action onDone)
	{
		StartCoroutine(ShowLoadingRoutine(onDone));
	}

	public void HideLoading(System.Action onDone)
	{
		StartCoroutine(HideLoadingRoutine(onDone));

	}

	IEnumerator ShowLoadingRoutine(System.Action onDone)
	{
		yield return new WaitForSeconds(0.5f);

		SetRandomSettings();
		float timer = 0f;

		while (timer < m_transitionTime)
		{
			float t = timer / m_transitionTime;
			m_image.fillAmount = t;
			yield return null;
			timer += Time.deltaTime;
		}
		m_image.fillAmount = 1f;

		m_isShowingLoading = true;
		onDone();
	}

	IEnumerator HideLoadingRoutine(System.Action onDone)
	{
		yield return new WaitForSeconds(0.5f);
		//m_image.fillClockwise = !m_image.fillClockwise;

		float timer = 0f;
		while (timer < m_transitionTime)
		{
			float t = timer / m_transitionTime;
			m_image.fillAmount = 1f - t;
			yield return null;
			timer += Time.deltaTime;
		}
		m_image.fillAmount = 0f;

		m_isShowingLoading = true;
		onDone();
	}

	void SetRandomSettings()
	{
		m_image.fillClockwise = Random.Range(0, 2) == 0;
		m_image.fillMethod = (Image.FillMethod)Random.Range(0, 5);
		m_image.fillOrigin = Random.Range(0, 4);
	}
}
