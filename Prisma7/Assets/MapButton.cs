using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapButton : MonoBehaviour
{
    public GameObject panel;
    public Map map;

    public GameObject map1;
    public GameObject map2;
    public GameObject map3;

    bool newworld;

    void Start()
    {
        Events.OnChangeWorld += OnChangeWorld;
        Events.OnMap += OnMap;
        newworld = true;
        SetStatus(false);
        Invoke("Init", 0.5f);
    }

    void Init() {
        SetLayers(Data.Instance.userData.actualWorld);
    }

    void OnDestroy() {
        Events.OnChangeWorld -= OnChangeWorld;
        Events.OnMap -= OnMap;
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

    void OnChangeWorld(int world) {
        SetLayers(world);
        newworld = true;
    }

    void OnMap() {
        if (newworld) {
            Invoke("ShowMap", 2);
            newworld = false;
        }
    }

    void ShowMap() {
        map.Init();
        Invoke("CloseMap", 3);
    }

    void CloseMap() {
        map.Close();
    }

    void SetLayers(int id) {
        map1.SetActive(false);
        map2.SetActive(false);
        map3.SetActive(false);

        if (id == 1)
            map1.SetActive(true);
        else if (id == 2)
            map2.SetActive(true);
        else if (id == 3)
            map3.SetActive(true);
    }
}
