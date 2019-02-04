using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

[Serializable]
public class Achievement   {

    public types type;

    public enum types
    {
        FRUIT_NINJA_WIN,
		DONE_ZONE_1,
		DONE_ZONE_2,
		DONE_ZONE_3,
		FIGURAS,
		COMBINATORIAS,
		POCIONES,
		GRILLA
    }

    public Image image;
	public string text;

    public bool ready;

    public int points;
    public int pointsToBeReady;

	public void Init(int value)
	{
		points = value;
		if (value >= pointsToBeReady) 
			Ready ();
	}
	public void NewPointToAchievement()
	{
		points++;
		if (points >= pointsToBeReady)
			Completed ();
	}
	void Completed ()
	{
		AchievementsEvents.OnReady(this);
		Ready ();
	}
    public void Ready()
    {
        this.ready = true;
    }
}
