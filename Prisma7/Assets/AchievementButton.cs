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
		image.sprite = achievement.image;
		Color c = Color.white;
		if (achievement.ready) {
			ready.gameObject.SetActive (true);
			unready.gameObject.SetActive (false);
			c.a = 1;
			image.color = c;
		} else {
			ready.gameObject.SetActive (false);
			unready.gameObject.SetActive (false);
			c.a = 0.35f;
			image.color = c;
		}

	}
	public void OnClick()
	{
		ui.SetText (achievement.text);
	}

}
