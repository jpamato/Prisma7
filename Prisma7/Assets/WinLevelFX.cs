using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLevelFX : MonoBehaviour {

	public List<GameObject> levelFX;
	int index;

	// Use this for initialization
	void Start () {
		index = Data.Instance.levelsData.actualDiamondLevel;
		Events.OnMathGameComplete += OnMathGameComplete;
	}

	void OnDestroy(){
		Events.OnMathGameComplete -= OnMathGameComplete;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnMathGameComplete(){
		//Debug.Log ("aca index: "+index);
		for (int i = 0; i < levelFX.Count; i++) {			
			levelFX [i].SetActive (i == index);
			if (i == index)
				Play (levelFX [i]);
		}
	}

	void Play(GameObject go){
		go.GetComponent<ParticleSystem> ().Play (true);
	}
}
