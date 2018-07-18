using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorBar : MonoBehaviour {

	public List<GameObject> colors;
	int index;
	Image image;

	// Use this for initialization
	void Start () {
		index = Data.Instance.levelsData.actualDiamondLevel;
		image = colors [index].GetComponent<Image> ();
		SetColorBar ();
		SetValue(Data.Instance.levelsData.actualLevelPercent);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetColorBar(){
		for (int i = 0; i < colors.Count; i++)
			colors [i].SetActive (i == index);
	}

	public void SetValue(float val){
		image.fillAmount = val;
	}
}
