using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsUI : MonoBehaviour {
	public GameObject achievementsButton;
	public GameObject NewAchievement;
	public GameObject panel;
	public Text field;
	public Transform container;
	public AchievementButton button;

	void Start () {
		AchievementsEvents.OnReady += OnReady;
		NewAchievement.SetActive (false);
		panel.SetActive (false);
	}
	public void SetStatus(bool isOn)
	{
		achievementsButton.SetActive (isOn);
	}
	void OnDestroy () {
		AchievementsEvents.OnReady -= OnReady;
	}
	void OnReady(Achievement ach)
	{
		NewAchievement.SetActive (true);
	}
	public void Open()
	{
		field.text = "Selecciona un logro para ver de qué se trata";
		panel.SetActive (true);
		NewAchievement.SetActive (false);
		Utils.RemoveAllChildsIn (container);
		foreach (Achievement ach in AchievementsManager.Instance.all) {
			AchievementButton b = Instantiate (button);
			b.transform.SetParent (container);
			b.Init (this, ach);
		}
	}
	public void Close()
	{
		panel.SetActive (false);
	}
	public void SetText(string text)
	{
		field.text = text;
	}
}
