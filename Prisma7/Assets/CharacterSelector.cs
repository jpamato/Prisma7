using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour {

	void Start(){
		Data.Instance.ui.SetStatus (false);
	}
	public void SelectCharacter(int id)
	{
		if(id==0)
			Data.Instance.userData.sex = UserData.sexs.HE;
		else
			Data.Instance.userData.sex = UserData.sexs.SHE;

		Data.Instance.ui.SetStatus (true);

		Data.Instance.LoadScene ("World");
	}

}
