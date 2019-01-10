using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunaNotFound : MonoBehaviour {

	public int times;
	public int frame2Flash;

	Image image;
	Color color;
	bool show;
	bool flash;

	int count;

	// Use this for initialization
	void Start () {
		image = GetComponent<Image> ();
		color = image.color;
		Events.NotRuna += NotRuna;
	}

	void OnDestroy(){
		Events.NotRuna -= NotRuna;
	}

	void NotRuna(){
		Data.Instance.tipsManager.FaltaFigura ();
		show = true;
		flash = true;
		image.enabled = flash;
	}
	
	// Update is called once per frame
	void Update () {
		if (show) {
			if (Time.frameCount % frame2Flash == 0) {
				flash = !flash;
				float alpha = flash ? 1f : 0;
				image.color = new Color(color.r,color.g,color.b,alpha);
				if (!flash) {
					count++;
					if (count > times) {
						show = false;
						image.enabled = flash;
						count = 0;
					}
				}
			}			
		}
	}
}
