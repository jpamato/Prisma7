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
	}

	[Serializable]
	public class DiamondLevel{
		public int levelNumber;
		public Color color;
	}
}
