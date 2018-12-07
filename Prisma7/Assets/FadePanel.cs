using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadePanel : MonoBehaviour {

	public GameObject panel;
	public Animator anim;
	public Image bar; 
	float timer = 0.02f;

	void Start () {
		panel.SetActive (false);
	}
	public void SetOn () {
		StartCoroutine (Transition ());
		bar.fillAmount = 0;
	}
	IEnumerator Transition() {
		panel.SetActive (true);
		anim.Play ("loadingScreenOn");
		yield return new WaitForSeconds (0.35f);
		float i = 0;
		while (i < 1) {
			i += timer;
			yield return new WaitForSeconds(timer);
			bar.fillAmount = i;
		}
		bar.fillAmount = 1;
		anim.Play ("loadingScreenOff");
		yield return new WaitForSeconds (1);

		panel.SetActive (false);
	}
}
