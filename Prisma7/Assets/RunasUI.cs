using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RunasUI : MonoBehaviour
{
    public List<Runa> runas;

    [Serializable]
    public class Runa {
        public string name;
        public Image image;
        public bool enabled;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach(Runa r in runas) {
            r.enabled = Data.Instance.figurasData.GetRuna(r.name).enabled;
            SetRunaState(r);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetRunaState(Runa r) {
        if (r.enabled)
            r.image.color = Color.white;
        else
            r.image.color = Color.red;
    }

    public void EnableRuna(string name) {
        Runa r = runas.Find(x => x.name == name);
        r.enabled = true;
        if (r != null)
            SetRunaState(r);
    }
}
