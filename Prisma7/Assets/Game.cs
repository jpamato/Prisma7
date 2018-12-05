using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

	public modes mode;
	public Character character;

	public enum modes
	{
		WORLD,
		FRUIT_NINJA,
		FREEZED
	}
	static Game mInstance = null;

	public static Game Instance
	{
		get
		{
			return mInstance;
		}
	}
	void Awake()
	{
		mInstance = this;
	}
	void Start()
	{		
		mode = modes.WORLD;
		Data.Instance.ui.HideTimer ();
		Invoke ("Saluda", 1.5f);
	}
	void Saluda()
	{
		Events.OnMap ();
	}
	public void ChangeMode(modes _mode)
	{
		this.mode = _mode;
	}
}
