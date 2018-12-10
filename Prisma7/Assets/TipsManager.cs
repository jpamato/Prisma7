using System.Collections;
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

	int actualDiamondLevel;

	void Start()
	{
		saludoDone = CheckIfDone ("saludoDone");
		fruitWinDone = CheckIfDone ("fruitWinDone");

		if(loadData)
			LoadGameData ();
		
		Events.OnMap += OnMap;
		Events.PortalUnavailable += PortalUnavailable;
		Events.PortalFinalUnavailable += PortalFinalUnavailable;
		Events.OnCharacterChangeDirection += OnCharacterChangeDirection;
		Events.OpenFruitNinja += OpenFruitNinja;
		Events.CloseFruitNinja += CloseFruitNinja;
		Events.OnPetSay += OnPetSay;
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
	}
	void Update()
	{
		timeToSayWhenNotWalkingCount += Time.deltaTime;
		timeToNotEnterPortalCount += Time.deltaTime;
		if (timeToNotEnterPortalCount > timeToNotEnterPortal) {
			Events.OnPetSay (tipsContent.apura);
		} else
		if (timeToSayWhenNotWalkingCount > timeToSayWhenNotWalking) {
			if(UnityEngine.Random.Range(0,10)<5)
				Events.OnPetSay (tipsContent.mover1);
			else
				Events.OnPetSay (tipsContent.mover2);
		}
	}
	void CloseFruitNinja(bool result)
	{
		if (!result)
			Events.OnPetSay (tipsContent.fruit);
		else if (!fruitWinDone) {
			Events.OnPetSay (tipsContent.fruitWin);
			Save ("fruitWinDone");
		}
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
	void Delayed()
	{
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
	}
}
