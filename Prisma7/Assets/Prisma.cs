using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prisma : MonoBehaviour {

	public List<GameObject> colors;

	// Use this for initialization
	void Start () {
        Invoke("Reset", 2);
        Events.OnLastPortalOpen += Reset;
    }

    private void OnDestroy() {
        Events.OnLastPortalOpen -= Reset;
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
