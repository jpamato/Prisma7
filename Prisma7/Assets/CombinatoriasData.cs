using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CombinatoriasData : MonoBehaviour {

	public List<Gema> gemas;
	public List<Level> combinatoriasLevels;
	public int currentLevel;

	[Serializable]
	public class Gema{
		public Sprite sprite;
		public int size;
	}

	[Serializable]
	public class Level{
		public int[] valores;
		public int combinaciones;
		public int resultado;
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
