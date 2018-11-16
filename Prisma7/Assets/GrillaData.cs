﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GrillaData : MonoBehaviour {

	public List<Level> grillaLevels;
	public int currentLevel;

	[Serializable]
	public class Level{
		public string consigna;
		public int area;
		public int columnas;
		public int filas;
		public Vector2 size;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
