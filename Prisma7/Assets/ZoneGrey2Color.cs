﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneGrey2Color : MonoBehaviour {

	public int diamondLevel;
	public string colorName;

	// Use this for initialization
	void Start () {
		if (Data.Instance.levelsData.actualDiamondLevel < diamondLevel)
			SetGrey();
	}

	void SetGrey(){
		Renderer[] renderers = GetComponentsInChildren<Renderer>();
		foreach (Renderer r in renderers) {	
			/*print (r.gameObject.name);
			Color c = r.material.GetColor (colorName);
			float h, s, v;
			Color.RGBToHSV (c, out h, out s, out v);
			//r.material.SetColor(colorName,Color.HSVToRGB (h, 0f, v));*/
			r.material.SetColor(colorName,Color.black);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
