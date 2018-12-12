using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnQRFound : MonoBehaviour {

	public GameObject sign;

	// Use this for initialization
	void Start () {
		Events.OnRunaFound += OnRunaFound;
	}

	void OnDestroy(){
		Events.OnRunaFound -= OnRunaFound;
	}

	void OnRunaFound(){
		sign.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Back2World(){
		Data.Instance.LoadScene ("World");
	}
}
