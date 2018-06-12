using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FigurasData : MonoBehaviour {

	public List<Runa> runas;
	public List<Figura> figuras;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	[Serializable]
	public class Runa{
		public GameObject go;
		public bool enabled;
	}

	[Serializable]
	public class Figura{
		public GameObject go;
		public List<Runa> runas;
		public bool done;

		public bool CheckRuna(string n){
			done = true;
			foreach(Runa r in runas){
				if (r.go.name == n)
					r.enabled = true;
				if (!r.enabled)
					done = false;
			}
			return done;
		}
	}
}
