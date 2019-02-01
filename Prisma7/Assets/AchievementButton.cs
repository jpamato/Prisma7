using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementButton : MonoBehaviour {

	public Image image;
	public Image ready;
	public Image unready;

	AchievementsUI ui;
	Achievement achievement;

	public void Init(AchievementsUI ui, Achievement achievement) {
		this.ui = ui;
		this.achievement = achievement;
		if (achievement.ready) {
			ready.gameObject.SetActive (true);
			unready.gameObject.SetActive (false);
		} else {
			ready.gameObject.SetActive (false);
			unready.gameObject.SetActive (true);
		}
	}
	public void OnClick()
	{
		ui.SetText (achievement.text);
	}

}
