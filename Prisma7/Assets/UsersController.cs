using UnityEngine;
using System.Collections;
using System;

public class UsersController : MonoBehaviour {

	const string URL = "http://belatekallim.buber.org.ar/";//PRD

	private string getUser_URL = URL + "getUser.php?";	

	private string saveData_URL = URL + "saveData.php?";

	private string secretKey = "mondongo";

    public User user;
    public bool logged;

    [Serializable]
    public class User
    {
        public int id;
        public string username;
        public string password;
        public int diamondLevel;
        public float levelPercent;
        public int currentLevelIndex;
    }

	// Use this for initialization
	void Start () {

        Events.SendData += SendData;

        //StartCoroutine(GetUser("juan", "amato"));
        if (PlayerPrefs.GetInt("Logged") > 0)
        {
            logged = true;
            user.username = PlayerPrefs.GetString("user");
            user.password = PlayerPrefs.GetString("pass");
            user.id = PlayerPrefs.GetInt("id");
        }
    }

    private void OnDestroy()
    {
        Events.SendData -= SendData;
    }

    // Update is called once per frame
    void Update () {
	
	}

	public IEnumerator GetUser(string _username, string pass)
	{		
			string post_url = getUser_URL + "username=" + WWW.EscapeURL (_username) + "&password=" + WWW.EscapeURL (pass);
			print ("GetUser : " + post_url);
			WWW hs_post = new WWW (post_url);
			yield return hs_post;
        if (hs_post.error != null)
        {
            print("No pudo obtener el user: " + hs_post.error);
        }
        else
        {            
            if (hs_post.text == "FORBIDDEN ACCESS")
            {
                print("FORBIDDEN ACCESS");
            }
            else
            {
                print("user: " + hs_post.text);
                JsonUtility.FromJsonOverwrite(hs_post.text.Substring(8, hs_post.text.Length - 2 - 8),user);
                    
                if (user.id > -1)
                {
                    logged = true;
                    PlayerPrefs.SetInt("Logged", 1);
                    PlayerPrefs.SetString("user", user.username);
                    PlayerPrefs.SetString("pass", user.password);
                    PlayerPrefs.SetInt("id", user.id);
                    Data.Instance.userData.actualWorld = user.currentLevelIndex;
                    Data.Instance.levelsData.SetLevelPercent(user.levelPercent);
                    Data.Instance.levelsData.SetDiamondLevel(user.diamondLevel);
                }
            }
        }		
	}

    public void SendData()
    {
        StartCoroutine(SaveData(Data.Instance.levelsData.actualDiamondLevel,
            Data.Instance.levelsData.actualLevelPercent,Data.Instance.userData.actualWorld));

    }

    public IEnumerator SaveData(int diamondLevel, float levelpercent, int levelIndex)
    {
        string post_url = saveData_URL + "username=" + WWW.EscapeURL(user.username) + "&password=" + WWW.EscapeURL(user.password) + "&id=" + user.id +
            "&diamondLevel=" + diamondLevel + "&levelPercent=" + levelpercent + "&currentLevelIndex=" + levelIndex;
        print("SaveUser : " + post_url);
        WWW hs_post = new WWW(post_url);
        yield return hs_post;
        if (hs_post.error != null)
        {
            print("No se pudo salvar: " + hs_post.error);
        }
        else
        {
            
            if (hs_post.text == "FORBIDDEN ACCESS")
            {
                print("FORBIDDEN ACCESS");
            }
            else
            {
                print("saved data: " + hs_post.text);
            }
        }
    }
}
