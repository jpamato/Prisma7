using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorBar : MonoBehaviour {

	public List<GameObject> colors;
	public float animSpeedStep = 0.01f;
	int index;
	Image image;

	float newVal;
	bool animateBar;

	// Use this for initialization
	void Start () {
		Reset ();
	}

	void OnColorComplete(){

	}

	// Update is called once per frame
	void Update () {
		if (animateBar) {
			image.fillAmount += animSpeedStep;
			if (image.fillAmount >= newVal) {
				image.fillAmount = newVal;
				animateBar = false;
			}
		}
	}

	public void Reset(){
		index = Data.Instance.levelsData.actualDiamondLevel;
		image = colors [index].GetComponent<Image> ();
		SetColorBar ();
		SetValue(Data.Instance.levelsData.actualLevelPercent);
	}

	public void SetColorBar(){
		for (int i = 0; i < colors.Count; i++)
			colors [i].SetActive (i == index);
	}

	public void SetValue(float val){
		if (val > 0) {			
			newVal = val;
			animateBar = true;
		} else {
			image.fillAmount = val;
		}
	}
}
