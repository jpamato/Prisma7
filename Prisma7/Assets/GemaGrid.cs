using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemaGrid : MonoBehaviour {
	
	public Image image;
	public Color activeColor;
	public Color inactiveColor;
	public Vector2 id;

	Button button;
	bool active;

	// Use this for initialization
	void Start () {
		button = GetComponent<Button> ();
			
		button.onClick.AddListener (OnClick);
		image.color = inactiveColor;

		Events.OnMathGameComplete += OnMathGameComplete;
	}

	void OnDestroy(){
		Events.OnMathGameComplete -= OnMathGameComplete;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMathGameComplete(){
		if (active)
			image.color = Data.Instance.levelsData.GetNextLevel ().color;
	}

	void OnClick(){
		active = !active;
		if (active)
			image.color = activeColor;
		else
			image.color = inactiveColor;

		Events.OnGridClick (id, active);
	}
}
