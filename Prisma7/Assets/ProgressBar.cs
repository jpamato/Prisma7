using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

	public Image image;

	void Start () {
		//image.fillAmount = 0;
	}

	public void SetValue(float _value) {
		image.fillAmount = _value;
	}
}
