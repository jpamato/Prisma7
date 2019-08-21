using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{    

    public Image area1;
    public Image area2;
    public Image area3;

    public GameObject current1;
    public GameObject current2;
    public GameObject current3;

    void Start()
    {
        Close();
    }
    public void Init()
    {
        this.gameObject.SetActive(true);

        area1.color = Color.white;        
        area2.color = new Color(1, 1, 1, 0.2f);
        area3.color = new Color(1, 1, 1, 0.2f);

        current1.SetActive(false);
        current2.SetActive(false);
        current3.SetActive(false);

        if (Data.Instance.userData.actualWorld == 1)
            current1.SetActive(true);
        else if (Data.Instance.userData.actualWorld == 2)
            current2.SetActive(true);
        else if (Data.Instance.userData.actualWorld == 3)
            current3.SetActive(true);


        if (Data.Instance.userData.portalOpenedID > 0)
            area2.color = Color.white;
        if (Data.Instance.userData.portalOpenedID > 1)
            area3.color = Color.white;

    }
    public void Close()
    {
        this.gameObject.SetActive(false);
    }
}
