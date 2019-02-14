using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MateGame : MonoBehaviour {

	public ColorBar colorBar;
	public Text timerField;
	public Image timerImage;
	public GameObject doneSign;
	public GameObject colorDoneSign;
	public GameObject loseSign;
	public GameObject consigna;

	public int times2FullBar = 6;
	[Tooltip("Tiempo en segundos")]
	public int totalTime = 20;
	public int actualTime;
	private bool timeRunning;
	protected float levelBarStep;

	private float timerStep = 0.1f; // 100 milisegundos
	private int totalTimeSteps;
	Color clockColor;

	public states state;
	public enum states
	{
		PAUSED,
		PLAYING,
		ENDED
	}

	public void SetBarColor(){		
		//colorBar.SetValue(Data.Instance.levelsData.diamondLevels[Data.Instance.levelsData.actualDiamondLevel+1].color);
	}

	public void InitTimer(){
		Data.Instance.ui.StartTimer ();
		timerField.text = "";
		if (!timeRunning) {
			TimerLoop ();
			timeRunning = true;
			state = states.PLAYING;
		}
		totalTimeSteps = (int)(totalTime / timerStep);
		actualTime = totalTimeSteps;
	}

	public void TimerLoop()
	{
		Invoke ("TimerLoop", timerStep);

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


		//timerImage.fillAmount = 1-((float)(totalTimeSteps-actualTime)/(float)totalTimeSteps);
		Data.Instance.ui.timer.image.fillAmount = 1-((float)(totalTimeSteps-actualTime)/(float)totalTimeSteps);

	}

	public void TimePenalty(){
		/*clockColor = timerImage.color;
		timerImage.color = Color.red;*/

		clockColor = Data.Instance.ui.timer.image.color;
		Data.Instance.ui.timer.image.color = Color.red;
		
		actualTime--;
		string secText = "";
		if (actualTime < 10)
			secText = "0" + actualTime.ToString();
		else
			secText = actualTime.ToString();
		timerField.text = "00:" + secText;

		if (actualTime < 1)
			Events.OnTimeOver ();


		//timerImage.fillAmount = 1-((float)(totalTime-actualTime)/(float)totalTime);
		Data.Instance.ui.timer.image.fillAmount = 1-((float)(totalTime-actualTime)/(float)totalTime);
		Invoke ("ResetClockColor", 0.5f);
	}

	void ResetClockColor(){		
		//timerImage.color = clockColor;
		Data.Instance.ui.timer.image.color = clockColor;
	}
}
