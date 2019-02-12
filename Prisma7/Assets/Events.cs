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
	public static System.Action OnMathGameComplete = delegate {	};
	public static System.Action OnColorComplete = delegate {	};
	public static System.Action<Fruit.types> OnFruitNinjaClickedBubble = delegate {	};
	public static System.Action<InteractiveObject> OpenFruitNinja = delegate {	};
	public static System.Action<bool> CloseFruitNinja = delegate {	};
	public static System.Action<bool> OnDragger = delegate {	};
	public static System.Action OnCharacterChangeDirection = delegate {	};
	public static System.Action<string> OnPetSay = delegate {	};

	public static System.Action<Vector2,bool> OnGridClick = delegate { };

	public static System.Action<GameObject> DroppedUI = delegate {	};
	public static System.Action OnDropingOut = delegate {	};

	public static System.Action OnMap = delegate {	};
	public static System.Action PortalUnavailable = delegate {	};
	public static System.Action PortalFinalUnavailable = delegate {	};
	public static System.Action<int> OnChangeWorld = delegate {	};

	public static System.Action OnRunaFound = delegate {};

	public static System.Action NotRuna = delegate {};

	public static System.Action ClickSfx = delegate {};
	public static System.Action<string> OnVoice = delegate {};
	public static System.Action AllLevelsComplete = delegate {};

	public static System.Action OnMinigameDone = delegate {};

}
