using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class TipsManager : MonoBehaviour {

	public TipsContent tipsContent;

	public float timeToSayWhenNotWalking = 10;
	public float timeToSayWhenNotWalkingCount = 0;

	public bool saludoDone;
	int actualDiamondLevel;

	void Start()
	{
		saludoDone = CheckIfDone ("saludoDone");
		LoadGameData ();
		Events.OnMap += OnMap;
		Events.OnCharacterChangeDirection += OnCharacterChangeDirection;
	}
	void OnDestroy()
	{
		Events.OnMap -= OnMap;
		Events.OnCharacterChangeDirection -= OnCharacterChangeDirection;
	}
	void Update()
	{
		timeToSayWhenNotWalkingCount += Time.deltaTime;
		if (timeToSayWhenNotWalkingCount > timeToSayWhenNotWalking) {
			Events.OnPetSay (tipsContent.mover1);
			timeToSayWhenNotWalkingCount = 0;
		}
	}
	void OnCharacterChangeDirection(){
		timeToSayWhenNotWalkingCount = 0;
	}
	void OnMap()
	{
		timeToSayWhenNotWalkingCount = 0;
		if (!saludoDone) {
			Events.OnPetSay (tipsContent.saludo);
			Save ("saludoDone");
			saludoDone = true;
		} else if (actualDiamondLevel < Data.Instance.levelsData.actualDiamondLevel) {
			if (Data.Instance.levelsData.actualDiamondLevel == 1)
				Events.OnPetSay (tipsContent.color1);
			else
				Events.OnPetSay (tipsContent.color2);
		}

		actualDiamondLevel = Data.Instance.levelsData.actualDiamondLevel;
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

		print (filePath);

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
