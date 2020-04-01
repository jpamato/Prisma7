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
		Data.Instance.ui.HideCapture ();
		sign.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Back2World(){
        sign.SetActive(false);
        Data.Instance.captureScreen.SetActive(false);
	}

    public void Continue() {
        sign.SetActive(false);
        Events.OnRunaCaptureContinue();
        //Data.Instance.captureScreen.SetActive(true);
    }
}
