using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadePanel : MonoBehaviour {

	public GameObject panel;
	public Animator anim;

	void Start () {
		panel.SetActive (false);
	}
	public void SetOn () {
		StartCoroutine (Transition ());
	}
	IEnumerator Transition() {
		panel.SetActive (true);
		anim.Play ("loadingScreenOn");
		yield return new WaitForSeconds (2);
		anim.Play ("loadingScreenOff");
		yield return new WaitForSeconds (1);
		panel.SetActive (false);
	}
}
