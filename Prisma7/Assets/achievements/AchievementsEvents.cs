using UnityEngine;
using System.Collections;

public static class AchievementsEvents
{
	public static System.Action<Achievement> OnReady = delegate { };
	public static System.Action<Achievement.types> NewPointToAchievement = delegate { };
}