using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapButton : MonoBehaviour
{
    public GameObject panel;
    public Map map;

    void Start()
    {
        SetStatus(false);
    }
    public void SetStatus(bool isOn)
    {
        panel.SetActive(isOn);
    }
    public void Clicked()
    {
        print("abre");
        map.Init();
    }
}
