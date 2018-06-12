using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelsData : MonoBehaviour {

	public List<DiamondLevel> diamondLevels;
	public int actualDiamondLevel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[Serializable]
	public class DiamondLevel{
		public int levelNumber;
		public Color color;
	}
}
