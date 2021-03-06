﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelsData : MonoBehaviour {

	public List<DiamondLevel> diamondLevels;
	public int actualDiamondLevel;
	public float actualLevelPercent;//0-1
	public bool showOutro;

	bool allLevelsComplete;

	// Use this for initialization
	void Start () {

		actualDiamondLevel = PlayerPrefs.GetInt ("actualDiamondLevel");
		actualLevelPercent = PlayerPrefs.GetFloat ("actualLevelPercent");

        //actualDiamondLevel = 5;
        //actualLevelPercent = 0.9f;

        if (actualDiamondLevel >= diamondLevels.Count)
			allLevelsComplete = true;

		Events.OnColorComplete += OnColorComplete;
        Events.OnLastPortalOpen += Restart;
    }

	void OnDestroy(){
		Events.OnColorComplete -= OnColorComplete;
        Events.OnLastPortalOpen -= Restart;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnColorComplete(){
		actualDiamondLevel++;
		if (actualDiamondLevel >= diamondLevels.Count) {
			actualDiamondLevel = diamondLevels.Count - 2;
			if (!allLevelsComplete) {
				Events.AllLevelsComplete ();
			}
		}

		actualLevelPercent = 0;
		PlayerPrefs.SetFloat ("actualLevelPercent", actualLevelPercent);

		PlayerPrefs.SetInt ("actualDiamondLevel", actualDiamondLevel);
		Invoke ("ColorsReset", 4);
	}

	public void AddLevelPercent(float val){
		actualLevelPercent += val;
		PlayerPrefs.SetFloat ("actualLevelPercent", actualLevelPercent);
	}

    public void SetLevelPercent(float val){
        actualLevelPercent = val;
        PlayerPrefs.SetFloat("actualLevelPercent", actualLevelPercent);
    }

    public void SetDiamondLevel(int dLevel)
    {
        actualDiamondLevel = dLevel;
        PlayerPrefs.SetInt("actualDiamondLevel", actualDiamondLevel);
        Invoke("ColorsReset", 4);
    }

    void ColorsReset(){
		Data.Instance.ui.colorBar.Reset ();
		Data.Instance.ui.prisma.Reset ();
	}

	public DiamondLevel GetActualLevel(){
		return diamondLevels [(actualDiamondLevel%diamondLevels.Count)];
	}

	public DiamondLevel GetNextLevel(){
		return diamondLevels [(actualDiamondLevel + 1 )% diamondLevels.Count];
	}

	public DiamondLevel GetInProgressLevel(){
		return diamondLevels [(actualDiamondLevel + 1) % diamondLevels.Count];
    }

	[Serializable]
	public class DiamondLevel{
		public int levelNumber;
		public Color color;
	}

    void Restart() {        
        actualDiamondLevel = 0;
        actualLevelPercent = 0;
        PlayerPrefs.SetInt("actualDiamondLevel", actualDiamondLevel);
        PlayerPrefs.SetFloat("actualLevelPercent", actualLevelPercent);
        ColorsReset();        
        allLevelsComplete = false;
    }
}
