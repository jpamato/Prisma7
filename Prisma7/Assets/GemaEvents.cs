using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GemaEvents : EventTrigger {
    // Start is called before the first frame update

    GemaGrid ggrid;
    void Start()
    {
        ggrid = GetComponent<GemaGrid>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnPointerUp(PointerEventData data) {
        Data.Instance.grillaData.overActiveState = GrillaData.ActiveState.off;
    }

    public override void OnPointerDown(PointerEventData data) {
        if (ggrid.button.interactable)
            ggrid.OnPointerDown();
    }

    public override void OnPointerEnter(PointerEventData data) {
        if (ggrid.button.interactable)
            ggrid.OnPointerEnter();
    }
}
