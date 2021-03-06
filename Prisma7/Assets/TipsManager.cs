﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class TipsManager : MonoBehaviour {

	public bool loadData;
	public TipsContent tipsContent;

	public float timeToSayWhenNotWalking = 10;
	public float timeToSayWhenNotWalkingCount = 0;

	public float timeToNotEnterPortal = 20;
	public float timeToNotEnterPortalCount = 0;

	public bool saludoDone;
	public bool fruitWinDone;
	public bool sawMago;
	public bool sawMagoFirstTime;
	public bool faltaFigura;
	public int actualDiamondLevel;

    void Start() {
        saludoDone = CheckIfDone("saludoDone");
        fruitWinDone = CheckIfDone("fruitWinDone");

        

        if (loadData)
            LoadGameData();

        Events.OnMap += OnMap;
        Events.PortalUnavailable += PortalUnavailable;
        Events.PortalFinalUnavailable += PortalFinalUnavailable;
        Events.OnCharacterChangeDirection += OnCharacterChangeDirection;
        Events.OpenFruitNinja += OpenFruitNinja;
        Events.CloseFruitNinja += CloseFruitNinja;
        Events.OnPetSay += OnPetSay;
        Events.OnRunaFound += OnRunaFound;
        Events.OnLastPortalOpen += Restart;

        Invoke("Init", 2);        
    }

    void Init() {
        actualDiamondLevel = Data.Instance.levelsData.actualDiamondLevel;
    }

	void OnDestroy()
	{
		Events.OnMap -= OnMap;
		Events.PortalUnavailable -= PortalUnavailable;
		Events.PortalFinalUnavailable -= PortalFinalUnavailable;
		Events.OnCharacterChangeDirection -= OnCharacterChangeDirection;
		Events.OpenFruitNinja  -= OpenFruitNinja;
		Events.CloseFruitNinja  -= CloseFruitNinja;
		Events.OnPetSay -= OnPetSay;
        Events.OnRunaFound -= OnRunaFound;
        Events.OnLastPortalOpen -= Restart;
}
	void Update()
	{
		string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name;
		if (sceneName != "World" && sceneName != "World2" && sceneName != "World3")
			return;
		
		if (Game.Instance != null && (Game.Instance.mode == Game.modes.FRUIT_NINJA || Game.Instance.mode == Game.modes.FREEZED))
			return;
		timeToSayWhenNotWalkingCount += Time.deltaTime;
		timeToNotEnterPortalCount += Time.deltaTime;
		if (timeToNotEnterPortalCount > timeToNotEnterPortal) {
			Events.OnPetSay (tipsContent.apura);
			timeToNotEnterPortalCount = 0;
		} else
		if (timeToSayWhenNotWalkingCount > timeToSayWhenNotWalking) {
			if(UnityEngine.Random.Range(0,10)<5)
				Events.OnPetSay (tipsContent.mover1);
			else
				Events.OnPetSay (tipsContent.mover2);
		}
	}
    bool fruitNinjaGood_played;
    bool fruitNinjaBad_played;
    void CloseFruitNinja(bool result)
	{
        if (!result)
        {
            if (!fruitNinjaBad_played)
            {
                Events.OnPetSay(tipsContent.fruit);
                fruitNinjaBad_played = true;
            }
        }
        else if (!fruitWinDone)
        {
            if (!fruitNinjaGood_played)
            {
                Events.OnPetSay(tipsContent.fruitWin);
                Save("fruitWinDone");
                fruitNinjaGood_played = true;
            }
        }
		timeToSayWhenNotWalkingCount = 0;
		timeToNotEnterPortalCount = 0;
	}
	void OpenFruitNinja(InteractiveObject io)
	{
		timeToNotEnterPortalCount = 0;
	}
	void OnCharacterChangeDirection(){
		ResetTimers ();
	}
	void PortalUnavailable()
	{
		Events.OnPetSay (tipsContent.portalBlocked);
		timeToNotEnterPortalCount = 0;
	}
	void PortalFinalUnavailable()
	{
		Events.OnPetSay (tipsContent.portalFinal);
	}
	void OnMap()
	{
		timeToNotEnterPortalCount = 0;
		ResetTimers ();
		Invoke ("Delayed", 1);
	}
	void ResetTimers()
	{
		timeToSayWhenNotWalkingCount = 0;
	}
	public void SawMago()
	{
		if (sawMagoFirstTime)
			return;
		sawMagoFirstTime = true;
		sawMago = true;
	}
	public void FaltaFigura()
	{
		faltaFigura = true;
	}
    void OnRunaFound() {
        faltaFigura = false;
    }
    void Delayed()
	{
		if (faltaFigura) {
			Events.OnPetSay (tipsContent.faltaFigura);
			faltaFigura = false;
		} else
		if (sawMago) {
			Events.OnPetSay (tipsContent.mago);
			sawMago = false;
		} else
		if (!saludoDone) {
			Events.OnPetSay (tipsContent.saludo);
			Save ("saludoDone");
			saludoDone = true;
		} else if (actualDiamondLevel < Data.Instance.levelsData.actualDiamondLevel) {
			if (Data.Instance.levelsData.actualDiamondLevel == 1)
				Events.OnPetSay (tipsContent.color1);
			else if (Data.Instance.levelsData.actualDiamondLevel == 6)
				Events.OnPetSay (tipsContent.colorFinal);
			else
				Events.OnPetSay (tipsContent.color2);
		}
		actualDiamondLevel = Data.Instance.levelsData.actualDiamondLevel;
	}
	void OnPetSay(string text)
	{
		ResetTimers ();
	}

	void Save(string key)
	{
		PlayerPrefs.SetInt (key, 1);
	}
	bool CheckIfDone(string key)
	{
		if (PlayerPrefs.GetInt (key) == 1)
			return true;
		return false;
	}
	private void LoadGameData()
	{
		string filePath = Application.streamingAssetsPath + "/tips.json";

		if (File.Exists (filePath)) {
			string dataAsJson = File.ReadAllText (filePath);
			tipsContent = JsonUtility.FromJson<TipsContent> (dataAsJson);
		}
	}

	[Serializable]
	public class TipsContent
	{
		public string saludo;
		public string apura;
		public string mago;
		public string color1;
		public string color2;
		public string portalBlocked;
		public string portalFinal;
		public string mover1;
		public string mover2;
		public string fruit;
		public string fruitWin;
		public string colorFinal;
		public string faltaFigura;
	}
	public string GetNameByValue(string value)
	{
		if(tipsContent.saludo == value)
			return "saludo";
		if( tipsContent.apura == value)
			return "apura";
		if(tipsContent.mago == value)
			return "mago";
		if(tipsContent.color1 == value)
			return "color1";
		if(tipsContent.color2 == value)
			return "color2";
		if(tipsContent.portalBlocked == value)
			return "portalBlocked";
		if(tipsContent.portalFinal == value)
			return "portalFinal";
		if(tipsContent.mover1 == value)
			return "mover1";
		if(tipsContent.mover2 == value)
			return "mover2";
		if(tipsContent.fruit == value)
			return "fruit";
		if(tipsContent.fruitWin == value)
			return "fruitWin";
		if(tipsContent.colorFinal == value)
			return "colorFinal";	
		if(tipsContent.faltaFigura == value)
			return "faltaFigura";
		return "";
	}

    void Restart() {
        PlayerPrefs.DeleteKey("saludoDone");
        PlayerPrefs.DeleteKey("fruitWinDone");
        saludoDone = false;
        fruitWinDone = false;
        sawMago = false;
        sawMagoFirstTime = false;
        faltaFigura = false;
        actualDiamondLevel = 0;
    }
}
