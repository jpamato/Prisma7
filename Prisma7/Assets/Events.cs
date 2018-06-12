using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Events {
	
	public static System.Action<GameObject> OnMouseCollide = delegate { };
	public static System.Action OnTimeOver = delegate {	};

	public static System.Action<string> FiguraComplete = delegate {	};

}
