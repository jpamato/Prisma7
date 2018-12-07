using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour {

	public sexs sex;
	public enum sexs
	{
		HE,
		SHE
	}
	public int portalOpenedID;
	public List<int> doorsPlayed;
	public Vector3 lastPosition;
	public int actualWorld;

	void Start()
	{
		portalOpenedID = PlayerPrefs.GetInt ("portalOpenedID", 0);
		actualWorld = PlayerPrefs.GetInt ("OnChangeWorld");
		Events.OnChangeWorld += OnChangeWorld;
	}
	void OnDestroy()
	{
		Events.OnChangeWorld -= OnChangeWorld;
	}
	void OnChangeWorld(int worldID)
	{
		actualWorld = worldID;
		doorsPlayed.Clear ();
		PlayerPrefs.SetInt ("OnChangeWorld", worldID);
	}
	public int GetPortalIDOpened()
	{
		return PlayerPrefs.GetInt ("portalOpenedID", 0);
	}
	public void OnDoorPlayed(int doorId)
	{
		doorsPlayed.Add (doorId);
	}
	public void SaveLastPosition()
	{
		Vector3 pos = Game.Instance.character.transform.localPosition;
		pos.z -= 1f;
		lastPosition = pos;
	}
	public void SaveSpecificLastPosition(Vector3 pos)
	{
		lastPosition = pos;
	}
	public void OpenPortal(int _portalOpenedID)
	{
		PlayerPrefs.SetInt ("portalOpenedID", _portalOpenedID);
		portalOpenedID = _portalOpenedID;
	}
}

