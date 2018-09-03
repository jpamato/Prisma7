using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour {

	public ProgressBar timer;
	public ProgressBar progressBar;
	public ColorBar colorBar;
	public Prisma prisma;

	public void HideTimer()
	{
		timer.gameObject.SetActive (false);
	}
	public void StartTimer()
	{
		timer.gameObject.SetActive (true);
	}
}
