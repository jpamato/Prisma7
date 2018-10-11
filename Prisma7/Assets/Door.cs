using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractiveObject {
	public ParticleSystem damageSystem;
	public MeshRenderer hieloMesh;
	public Texture2D[] textures_hielo;
	public int id;
	public int diamondLevel;
	public Data.minigamesScenes minigame;
	public states state;
	public enum states
	{
		UNAVAILABLE,
		CLOSED,
		OPENED,
		OPENING
	}
	public GameObject closed;
	public GameObject opened;
	public GameObject opening;

	public void SetState(states state)
	{
		closed.SetActive (false);
		opened.SetActive (false);
		opening.SetActive (false);

		this.state = state;
		switch (state) {
		case states.CLOSED:
			SetProgress (1);
			closed.SetActive (true);
			damageSystem.Stop ();
			break;
		case states.OPENING:
			opening.SetActive (true);
			opened.SetActive (true);
			Data.Instance.userData.OnDoorPlayed (id);
			break;
		case states.OPENED:
			opened.SetActive (true);
			break;
		}
	}
	int lastValue = 0;
	public void SetProgress(float value)
	{
		int v = (int)((1f - value) * 10);
		if (lastValue >= v)
			return;
		lastValue = v;
		hieloMesh.material.mainTexture = textures_hielo[v];

		if(v != 0)
			damageSystem.Play ();
	}
}
