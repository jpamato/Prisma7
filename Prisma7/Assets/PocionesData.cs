using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PocionesData : MonoBehaviour {

	public List<Ingrediente> ingredientes;
	public List<Level> pocionesLevels;
	public int currentLevel;

	[Serializable]
	public class Ingrediente{
		public string name;
		public Sprite sprite;
	}

	[Serializable]
	public class Level{
		public List<Valores> valores;
		public int fraccion;
		public int slots;
	}

	[Serializable]
	public class Valores{
		public int val;
		public int id;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
