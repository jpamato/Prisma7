using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunaNotFound : MonoBehaviour {

	public int times;
	public int frame2Flash;

	public List<MaskableGraphic> graf;

	/*public Image image;
	public Image image2;
	public Text text;*/
	List<Color> colors;

	bool show;
	bool flash;
	bool still;

	int count;

	int lastF;

	// Use this for initialization
	void Start () {		
		colors = new List<Color> ();
		foreach (MaskableGraphic m in graf)
			colors.Add (m.color);
		Events.NotRuna += NotRuna;
	}

	void OnDestroy(){
		Events.NotRuna -= NotRuna;
	}

	void NotRuna(){
		Data.Instance.tipsManager.FaltaFigura ();
		show = true;
		flash = true;
		foreach (MaskableGraphic m in graf)
			m.enabled = flash;
	}
	
	// Update is called once per frame
	void Update () {
		/*if(still){
			if (Time.frameCount >= lastF + (20*frame2Flash)) {
				flash = false;
				still = false;
				foreach (MaskableGraphic m in graf)
					m.enabled = flash;
			}
		}else if (show) {
			if (Time.frameCount % frame2Flash == 0) {
				flash = !flash;
				if (!flash) {
					count++;
					if (count > times) {
						show = false;
						still = true;
						flash = true;
						count = 0;
						lastF = Time.frameCount;
					}
				}
				float alpha = flash ? 1f : 0;
				for(int i=0;i<graf.Count;i++)
					graf[i].color = new Color(colors[i].r,colors[i].g,colors[i].b,alpha);
			}			
		}*/
	}
}
