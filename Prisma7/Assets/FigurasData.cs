using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FigurasData : MonoBehaviour {

	public List<Runa> runas;
	public List<Figura> figuras;

	public int[] currentLevel;
    public List<int>[] levelsDone;
    public FigurasLevels figurasLevels;
	public string filename = "figurasLevels.json";

	char fieldSeparator = '&';
	void Start () {
		foreach (Runa r in runas) {
			string s = PlayerPrefs.GetString (r.go.name);
			if(!r.enabled)
				r.enabled = s == "done"?true:false;
			//r.enabled = true;
		}

		//figurasLevels.intermedio = figurasLevels.basico;
		//figurasLevels.avanzado = figurasLevels.basico;

        Utils.Shuffle<Level>(figurasLevels.basico);
        Utils.Shuffle<Level>(figurasLevels.intermedio);
        Utils.Shuffle<Level>(figurasLevels.avanzado);

        levelsDone = new List<int>[3];
        for (int i = 0; i < levelsDone.Length; i++)
            levelsDone[i] = new List<int>();

        Events.OnLastPortalOpen += Restart;
    }

    private void OnDestroy() {
        Events.OnLastPortalOpen -= Restart;
    }

    // Update is called once per frame
    void Update () {
		
	}

	[Serializable]
	public class FigurasLevels{
		public Level[] basico;
		public Level[] intermedio;
		public Level[] avanzado;
	}

	[Serializable]
	public class Level{
		public Figura figura;
		public List<GameObject> opciones;
		public int time;
		public bool done;
	}

	[Serializable]
	public class Runa{
		public GameObject go;
		public bool enabled;
	}

	[Serializable]
	public class Figura{
		public GameObject go;
		public List<Runa> runas;
		public bool done;

		public bool CheckRuna(string n){
			done = true;
			foreach(Runa r in runas){
				if (r.go.name == n)
					r.enabled = true;
				if (!r.enabled)
					done = false;
			}
			return done;
		}

		public void ClearRunas(){
			foreach(Runa r in runas)
				r.enabled = false;
		}
	}

	public Runa GetRandomRuna(){
		return runas [UnityEngine.Random.Range (0,runas.Count)];
	}

	public Runa GetRuna(string s){
		return runas.Find(x => x.go.name == s);
	}

	public void ResetFiguresDone(){
		foreach (Figura f in figuras)
			f.done = false;
	}

	public Level GetLevel(){

        int dlevel = Data.Instance.userData.actualWorld;
        Debug.Log(levelsDone[0]);
        if (dlevel == 1) {
            while (levelsDone[0].Contains(currentLevel[0]) && levelsDone[0].Count < figurasLevels.basico.Length) {
                currentLevel[0]++;
                if (currentLevel[0] >= figurasLevels.basico.Length)
                    currentLevel[0] = 0;
            }
            return figurasLevels.basico[currentLevel[0]];
        } else if (dlevel == 2) {
            while (levelsDone[1].Contains(currentLevel[1]) && levelsDone[1].Count < figurasLevels.intermedio.Length) {
                currentLevel[1]++;
                if (currentLevel[1] >= figurasLevels.intermedio.Length)
                    currentLevel[1] = 0;
            }
            return figurasLevels.intermedio[currentLevel[1]];
        } else if (dlevel == 3) {
            while (levelsDone[2].Contains(currentLevel[2]) && levelsDone[2].Count < figurasLevels.avanzado.Length) {
                currentLevel[2]++;
                if (currentLevel[2] >= figurasLevels.avanzado.Length)
                    currentLevel[2] = 0;
            }
            return figurasLevels.avanzado[currentLevel[2]];
        } else {
            return null;
        }
	}

	public void AddCurrentLevel(){
		int dlevel = Data.Instance.userData.actualWorld;
        if (dlevel == 1) {
            levelsDone[0].Add(currentLevel[0]);
            currentLevel [0]++;
			if (currentLevel [0] >= figurasLevels.basico.Length)
				currentLevel [0] = 0;
		} else if (dlevel == 2) {
            levelsDone[1].Add(currentLevel[1]);
            currentLevel [1]++;
			if (currentLevel [1] >= figurasLevels.intermedio.Length)
				currentLevel [1] = 0;
        } else if (dlevel == 3) {
            levelsDone[2].Add(currentLevel[2]);
            currentLevel [2]++;
			if (currentLevel [2] >= figurasLevels.avanzado.Length)
				currentLevel [2] = 0;
		}
	}

	public void EnableRuna(string name){
		Runa r = runas.Find (x => x.go.name == name);
		if (r == null)
			return;

		if (!r.enabled) {
			r.enabled = true;
			Debug.Log (name);
			PlayerPrefs.SetString (name, "done");
			SaveUserRunasLocal();
			Events.OnRunaFound ();
		}
	}

	public void SaveUserRunasLocal() {
		string data = "";
		foreach (Runa r in runas)
			data += "" + r.enabled + fieldSeparator;

		Debug.Log("SaveUserRunasLocal: " + data);
		PlayerPrefs.SetString(Data.Instance.usersDB.user.username + "_runas", data);
	}

	public void LoadUserAchievmentsLocal() {
		string s = PlayerPrefs.GetString(Data.Instance.usersDB.user.username + "_runas", "");
		Debug.Log(s);
		if (s == "")
			return;
		string[] data = s.Split(fieldSeparator);
		int i = 0;
		foreach (Runa r in runas) {
			if (!r.enabled) {
				r.enabled = bool.Parse(data[i]);
				if(r.enabled)
					PlayerPrefs.SetString(r.go.name, "done");
			}
			i++;
		}
	}

	void Restart() {
		Debug.Log("REstart");
        foreach (Runa r in runas) {
            string s = PlayerPrefs.GetString(r.go.name);
            if (s == "done")
                r.enabled = false;
            //r.enabled = true;
            PlayerPrefs.DeleteKey(r.go.name);
        }
    }
}
