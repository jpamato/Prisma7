using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsignaPociones : MonoBehaviour {
	
	public Text texto;
	public Text valor;

	public Image[] images;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ResetIcons(){
		foreach (Image i in images)
			i.color = new Color (0, 0, 0, 0);
	}
}
