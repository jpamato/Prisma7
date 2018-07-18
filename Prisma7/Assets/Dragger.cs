using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragger : MonoBehaviour {

	public void Start()
	{
		Events.OpenFruitNinja += OpenFruitNinja;
		Events.CloseFruitNinja += CloseFruitNinja;
	}
	void Update()
	{
		
	}
	void OpenFruitNinja(InteractiveObject io)
	{
		
	}
	void CloseFruitNinja()
	{
	}
}
