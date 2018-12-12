﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour {

	public void SplashClicked()
	{
		Data.Instance.LoadScene ("CharacterSelector");
	}

	public void Reiniciar(){
		Data.Instance.Reset ();
		Application.Quit ();
	}
}
