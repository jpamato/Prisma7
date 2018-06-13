using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MateGame : MonoBehaviour {

	public Slider colorBar;
	public Text timerField;
	public Image timerImage;
	public GameObject doneSign;
	public GameObject loseSign;

	public int totalTime = 20;
	public int actualTime;
	private bool timeRunning;

	Color clockColor;

	public states state;
	public enum states
	{
		PAUSED,
		PLAYING,
		ENDED
	}

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {

	}

	public void SetBarColor(){		
		colorBar.fillRect.GetComponent<Image>().color = Data.Instance.levelsData.diamondLevels[Data.Instance.levelsData.actualDiamondLevel+1].color;
	}

	public void InitTimer(){
		timerField.text = "";
		if (!timeRunning) {
			TimerLoop ();
			timeRunning = true;
			state = states.PLAYING;
		}
		actualTime = totalTime;
	}

	public void TimerLoop()
	{
		Invoke ("TimerLoop", 1);

		if(state == states.PLAYING)
			actualTime--;
		else
			return;
		/*if (lives == 0) {
			return;
		}*/

		string secText = "";
		if (actualTime < 10)
			secText = "0" + actualTime.ToString();
		else
			secText = actualTime.ToString();
		timerField.text = "00:" + secText;

		if (actualTime < 1)
			Events.OnTimeOver ();


		timerImage.fillAmount = 1-((float)(totalTime-actualTime)/(float)totalTime);

	}

	public void TimePenalty(){
		clockColor = timerImage.color;
		timerImage.color = Color.red;
		
		actualTime--;
		string secText = "";
		if (actualTime < 10)
			secText = "0" + actualTime.ToString();
		else
			secText = actualTime.ToString();
		timerField.text = "00:" + secText;

		if (actualTime < 1)
			Events.OnTimeOver ();


		timerImage.fillAmount = 1-((float)(totalTime-actualTime)/(float)totalTime);
		Invoke ("ResetClockColor", 0.5f);
	}

	void ResetClockColor(){
		timerImage.color = clockColor;
	}
}
