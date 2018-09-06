using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour {

	public ProgressBar timer;
	public ProgressBar progressBar;
	public ColorBar colorBar;
	public Prisma prisma;

	AudioSource clockSource;

	public void Start(){
		clockSource = timer.gameObject.GetComponent<AudioSource> ();
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
	}

	public void ClockSfx(bool play){
		if (play)
			clockSource.Play ();
		else
			clockSource.Stop ();
	}
}
