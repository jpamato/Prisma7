using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MateGameBG : MonoBehaviour {

	public List<GameObject> backgrounds;

	public void Start(){
		SetBackground (Data.Instance.levelsData.actualDiamondLevel);
	}

	public void SetBackground(int index){
		for (int i = 0; i < backgrounds.Count; i++)
			backgrounds [i].SetActive (i == index);
	}
}
