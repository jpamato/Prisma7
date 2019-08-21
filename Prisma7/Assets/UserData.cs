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
    public bool allPortalsOpened;

	void Start()
	{
		portalOpenedID = PlayerPrefs.GetInt ("portalOpenedID", 0);
		actualWorld = PlayerPrefs.GetInt ("OnChangeWorld");
        allPortalsOpened = PlayerPrefs.GetFloat("OnLastPortalOpen", 0)==1?true:false;
        if (actualWorld <= 0)
            actualWorld = 1;
        Events.OnChangeWorld += OnChangeWorld;
        Events.OnLastPortalOpen += OnLastPortalOpen;        
    }
	void OnDestroy()
	{
		Events.OnChangeWorld -= OnChangeWorld;
        Events.OnLastPortalOpen -= OnLastPortalOpen;
    }
	void OnChangeWorld(int worldID)
	{
		actualWorld = worldID;
		doorsPlayed.Clear ();
		PlayerPrefs.SetInt ("OnChangeWorld", worldID);
        Events.SendData();
    }

    void OnLastPortalOpen() {
        allPortalsOpened = true;
        PlayerPrefs.SetFloat("OnLastPortalOpen", 1);
    }


    public int GetPortalIDOpened()
	{
		return PlayerPrefs.GetInt ("portalOpenedID", 0);
	}

    public int GetLastDoorPlayed() {
        if (doorsPlayed.Count == 0)
            return -1;
        return doorsPlayed[doorsPlayed.Count - 1];
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
	public void ResetDoors()
	{
		foreach (int num in doorsPlayed) {
			doorsPlayed [num] = 0;
		}
	}
}

