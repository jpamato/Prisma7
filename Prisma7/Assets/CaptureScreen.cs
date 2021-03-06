﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureScreen : MonoBehaviour
{
    public bool isVisible;
    public GameObject captureCam;
    public GameObject captureCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActive(bool enable) {
        captureCam.SetActive(enable);
        captureCanvas.SetActive(enable);
        isVisible = enable;
        UptadeWorld(enable);
    }

    void UptadeWorld(bool enable) {
        if (!enable) {
            if (Game.Instance != null) {
                Data.Instance.ui.SetStatus(true);
                Data.Instance.ui.ShowCapture(false);
                Data.Instance.ui.ShowBack(false);
                Data.Instance.ui.HideTimer();
                Game.Instance.mode = Game.modes.WORLD;
            } else {
                Data.Instance.ui.progressBar.gameObject.SetActive(true);
                Data.Instance.ui.timer.gameObject.SetActive(true);
                Data.Instance.ui.ShowBack(true);
                Data.Instance.ui.HideCapture();
                Events.OnUpdateRunas();
            }
        } else {
            Data.Instance.ui.SetStatus(false);
            Data.Instance.ui.ShowBack(false);
            Data.Instance.ui.ShowCapture(true);
            if (Game.Instance != null)
                Game.Instance.mode = Game.modes.FREEZED;
        }
    }
}
