﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelsData : MonoBehaviour {

	public List<DiamondLevel> diamondLevels;
	public int actualDiamondLevel;
	public float actualLevelPercent;//0-1

	// Use this for initialization
	void Start () {
		Events.OnColorComplete += OnColorComplete;
	}

	void OnDestroy(){
		Events.OnColorComplete -= OnColorComplete;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnColorComplete(){
		actualDiamondLevel++;
		if (actualDiamondLevel >= diamondLevels.Count)
			actualDiamondLevel = diamondLevels.Count - 1;
		actualLevelPercent = 0;

		Invoke ("ColorsReset", 1);
	}

	void ColorsReset(){
		Data.Instance.ui.colorBar.Reset ();
		Data.Instance.ui.prisma.Reset ();
	}

	public DiamondLevel GetActualLevel(){
		return diamondLevels [actualDiamondLevel];
	}

	public DiamondLevel GetNextLevel(){
		return diamondLevels [actualDiamondLevel+1];
	}

	public DiamondLevel GetInProgressLevel(){
		return diamondLevels [actualDiamondLevel+1];
	}

	[Serializable]
	public class DiamondLevel{
		public int levelNumber;
		public Color color;
	}
}
