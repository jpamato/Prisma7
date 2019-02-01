using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

	public ProgressBar timer;
	public ProgressBar progressBar;
	public ColorBar colorBar;
	public Prisma prisma;

	public Image captureBtn;
	public Sprite capture,captureBack;

	public AudioSource clockSource;

	public GameObject back;

	public void SetStatus(bool isOn)
	{
		timer.gameObject.SetActive (isOn);
		progressBar.gameObject.SetActive (isOn);
		GetComponent<AchievementsUI>(). SetStatus(isOn);
	}
	public void HideTimer()
	{
		timer.gameObject.SetActive (false);
		clockSource.Stop ();
	}
	public void StartTimer()
	{
		timer.gameObject.SetActive (true);
		clockSource.Play ();
		GetComponent<AchievementsUI>(). SetStatus(false);
	}

	public void ClockSfx(bool play){
		if (play)
			clockSource.Play ();
		else
			clockSource.Stop ();
	}


	public void ShowCapture(bool back){
		captureBtn.gameObject.SetActive (true);
		if(back)
			captureBtn.sprite = captureBack;
		else
			captureBtn.sprite = capture;
	}

	public void ShowBack(bool enable){ back.SetActive (enable);	} 

	public void HideCapture(){ 
		captureBtn.gameObject.SetActive (false);
	}
}
