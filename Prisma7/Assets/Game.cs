using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

	public modes mode;
	public enum modes
	{
		WORLD,
		FRUIT_NINJA
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
	}
	public void ChangeMode(modes _mode)
	{
		this.mode = _mode;
	}
}
