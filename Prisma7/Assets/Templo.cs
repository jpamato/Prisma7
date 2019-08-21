using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Templo : MonoBehaviour
{
    public GameObject win;

    // Start is called before the first frame update
    void Start()
    {
        Events.OnLastPortalOpen += OnLastPortalOpen;
        if (Data.Instance.userData.allPortalsOpened)
            win.SetActive(true);
    }

    void OnDestroy() {
        Events.OnLastPortalOpen -= OnLastPortalOpen;
    }

    void OnLastPortalOpen() {
        win.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
