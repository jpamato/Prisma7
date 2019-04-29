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

    public bool unviewed;
    bool sistole;
    public float factor = 1;
    public Vector3 scale;

	void Start () {
		AchievementsEvents.OnReady += OnReady;
		NewAchievement.SetActive (false);
		panel.SetActive (false);
        scale = achievementsButton.transform.localScale;
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
        unviewed = true;

	}
	public void Open()
	{
        if (unviewed)
            unviewed = false;
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

    private void Update() {
        if (unviewed) {
            if (sistole) {                
                factor *= 0.99f;
                if (factor < 0.85f)
                    sistole = false;
            } else {
                factor *= 1.01f;
                if (factor > 1.25f)
                    sistole = true;
            }
            achievementsButton.transform.localScale = factor * scale;
        } else {
            factor = 1f;
            achievementsButton.transform.localScale = scale;
        }
    }
}
