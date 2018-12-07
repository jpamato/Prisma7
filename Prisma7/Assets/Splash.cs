using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour {

	public void SplashClicked()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene ("CharacterSelector");
	}
}
