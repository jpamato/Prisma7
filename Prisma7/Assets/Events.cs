using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Events {
	
	public static System.Action<GameObject> OnMouseCollide = delegate { };
	public static System.Action<InteractiveObject> OnCharacterHitInteractiveObject = delegate { };
	public static System.Action<Vector3> OnFloorClicked = delegate { };
	public static System.Action OnTimeOver = delegate {	};
	public static System.Action OnCharacterStopWalking = delegate {	};
	public static System.Action<string> FiguraComplete = delegate {	};
	public static System.Action<Fruit.types> OnFruitNinjaClickedBubble = delegate {	};
	public static System.Action<InteractiveObject> OpenFruitNinja = delegate {	};
	public static System.Action CloseFruitNinja = delegate {	};
}
