using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class AchievementsManager : MonoBehaviour
{
	public bool force_reset;
    public List<Achievement> all;

    const string PREFAB_PATH = "AchievementsManager";
    static AchievementsManager mInstance = null;

    public static AchievementsManager Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = FindObjectOfType<AchievementsManager>();

                if (mInstance == null)
                {
                    GameObject go = Instantiate(Resources.Load<GameObject>(PREFAB_PATH)) as GameObject;
                    mInstance = go.GetComponent<AchievementsManager>();
                    go.transform.localPosition = new Vector3(0, 0, 0);
                }
            }
            return mInstance;
        }
    }
  
    void Awake()
    {
        if (!mInstance)
            mInstance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this);
    }
    

    void Start()
    {
		string[] PieceTypeNames = System.Enum.GetNames (typeof(Achievement.types));
		for(int i = 0; i < PieceTypeNames.Length; i++){
			string achievementType = (PieceTypeNames[i]);
			int value = PlayerPrefs.GetInt(achievementType);
			List<Achievement> allAchievementsByType = GetAchievementsByType (achievementType);
			foreach (Achievement ach in allAchievementsByType)
				ach.Init (value);
		}

		if (force_reset)
			PlayerPrefs.DeleteAll ();
		AchievementsEvents.NewPointToAchievement += NewPointToAchievement;
    }
	void NewPointToAchievement(Achievement.types _type)
	{
		int points = 0;
		foreach (Achievement achievement in all)
			if (achievement.type == _type && !achievement.ready) {
				achievement.NewPointToAchievement ();
				points = achievement.points;
			}
		PlayerPrefs.SetInt(_type.ToString(), points);
	}
	List<Achievement> GetAchievementsByType(string _type)
	{
		List<Achievement> allAchievementsByType = new List<Achievement> ();
		foreach (Achievement achievement in all) {
			if (achievement.type.ToString () == _type) {
				allAchievementsByType.Add (achievement);
			}
		}
		return allAchievementsByType;
	}

}
