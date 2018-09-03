using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prisma : MonoBehaviour {

	public List<GameObject> colors;

	// Use this for initialization
	void Start () {
		Reset ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Reset(){
		SetColors (Data.Instance.levelsData.actualDiamondLevel);
	}

	public void SetColors(int index){
		for (int i = 0; i < colors.Count; i++)
			colors [i].SetActive (i == index);
	}
}
