using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FigurasGame : MonoBehaviour {

	public GameObject figura;
	public List<GameObject> runasButtons;
	public Text timerField;
	public Image timerImage;

	private int totalSec = 20;
	public int sec;
	bool timeRunning;

	bool playing;

	// Use this for initialization
	void Start () {
		Events.OnMouseCollide += FigureSelect;
		foreach (GameObject go in runasButtons) {
			bool enabled = Data.Instance.figurasManager.runas.Find (x=>x.name ==go.name).enabled;
			if (!enabled)
				go.GetComponent<Renderer> ().material.color = new Color (0.5F, 0.5F, 0.5F, 0.5f);
		}

		timerField.text = "";

		if (!timeRunning) {
			TimerLoop ();
			timeRunning = true;
			playing = true;
		}
		sec = totalSec;
	}

	void OnDestroy(){
		Events.OnMouseCollide -= FigureSelect;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FigureSelect(GameObject hit){		
		if (hit.tag == "Runa") {
			FigurasManager.Runa runa = Data.Instance.figurasManager.runas.Find (x=>x.name ==hit.name);
			if (runa.enabled) {
				Transform t = figura.transform.Find (runa.name);
				if (t != null) {
					t.GetComponent<Renderer> ().material.color = Color.red;
				}
			}
		}
	}

	void TimerLoop()
	{
		Invoke ("TimerLoop", 1);

		if(playing)
			sec--;
		else
			return;
		/*if (lives == 0) {
			return;
		}*/

		string secText = "";
		if (sec < 10)
			secText = "0" + sec.ToString();
		else
			secText = sec.ToString();
		timerField.text = "00:" + secText;

		if (sec < 1)
			playing = false;


		timerImage.fillAmount = 1-((float)(totalSec-sec)/(float)totalSec);

	}
}
