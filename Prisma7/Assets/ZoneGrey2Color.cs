using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneGrey2Color : MonoBehaviour {

	public int diamondLevel;
	public string colorName;

	public bool greyTexture;

	string greyMark = "_gris";

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
			//print (r.gameObject.name+" texture: " + r.material.mainTexture.name);
			if(colorName!="")
				r.material.SetColor(colorName,Color.white);

			if(greyTexture){
				string actualName = r.material.mainTexture.name;
				//print (actualName + greyMark);
				if (!actualName.Contains ("_gris")) {
					Texture2D tex = (Texture2D)Resources.Load ("Textures/" + actualName + greyMark, typeof(Texture2D));
					//print (tex);
					r.material.mainTexture = tex;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
