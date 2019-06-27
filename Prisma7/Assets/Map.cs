using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public GameObject lock2;
    public GameObject lock3;

    public Image area1;
    public Image area2;
    public Image area3;

    void Start()
    {
        Close();
    }
    public void Init()
    {
        this.gameObject.SetActive(true);

        area1.color = new Color(1, 1, 1, 0.5f);
        area2.color = new Color(1, 1, 1, 0.5f);
        area3.color = new Color(1, 1, 1, 0.5f);

        if (Data.Instance.userData.actualWorld == 1)
            area1.color = Color.white;
        else if (Data.Instance.userData.actualWorld == 2)
            area2.color = Color.white;
        else if (Data.Instance.userData.actualWorld == 3)
            area3.color = Color.white;

        lock2.gameObject.SetActive(true);
        lock3.gameObject.SetActive(true);

        if (Data.Instance.userData.portalOpenedID > 0)
            lock2.gameObject.SetActive(false);
        if (Data.Instance.userData.portalOpenedID > 1)
            lock3.gameObject.SetActive(false);

    }
    public void Close()
    {
        this.gameObject.SetActive(false);
    }
}
