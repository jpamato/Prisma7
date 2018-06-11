using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FigurasManager : MonoBehaviour {

	public List<Runa> runas;
	public List<Figuras> figuras;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	[Serializable]
	public class Runa{
		public string name;
		public GameObject go;
		public bool enabled;
	}

	[Serializable]
	public class Figuras{
		public string id;
		public GameObject go;
		public List<Runa> runas;
	}
}
