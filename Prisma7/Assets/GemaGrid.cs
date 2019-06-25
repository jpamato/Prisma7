using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.EventSystems;

public class GemaGrid : MonoBehaviour {
	
	public Image image;
	public Color activeColor;
	public Color inactiveColor;
	public Vector2 id;
	AudioSource audiosource;

	public Button button;
	public bool active;

	// Use this for initialization
	void Awake () {
		
		button = GetComponent<Button> ();
			
		//button.onClick.AddListener (OnClick);
		image.color = inactiveColor;

		audiosource = GetComponent<AudioSource> ();

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

    void OnClick() {
        active = !active;
        if (active) {
            image.color = activeColor;
            audiosource.Play();
        } else
            image.color = inactiveColor;

        Events.OnGridClick(id, active);
    }
      
    public void OnPointerDown() {
        active = !active;
        if (active) {
            image.color = activeColor;
            audiosource.Play();
        } else
            image.color = inactiveColor;
        
        Events.OnGridClick(id, active);
    }

    public void OnPointerEnter() {
        if (Data.Instance.grillaData.overActiveState != GrillaData.ActiveState.off) {                
            if(Data.Instance.grillaData.overActiveState== GrillaData.ActiveState.active)
                active = true;
            else
                active = false;
            if (active) {
                image.color = activeColor;
                audiosource.Play();
            } else
                image.color = inactiveColor;

            Events.OnGridOver(id);
        }
    }

    public void SetInteractable(bool enable){
		button.interactable = enable;
	}

	public void SetActive(bool enable){
		active = enable;
		ColorBlock cb = button.colors;
		if (active) {
			image.color = activeColor;
			cb.disabledColor = activeColor;
		} else {
			image.color = inactiveColor;
			cb.disabledColor = inactiveColor;
		}		
		button.colors = cb;
	}
}
