using UnityEngine;
using System.Collections;
using System;

public class UsersController : MonoBehaviour {

	const string URL = "http://belatekallim.buber.org.ar/";//PRD

	private string getUser_URL = URL + "getUser.php?";	

	private string saveData_URL = URL + "saveData.php?";

	private string secretKey = "mondongo";

    private char fieldSeparator = '&';

    [SerializeField] int usersCount;
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

        usersCount = PlayerPrefs.GetInt("usersCount", 0);
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
    public void Logout() {
        PlayerPrefs.DeleteKey("user");
        PlayerPrefs.DeleteKey("pass");
        PlayerPrefs.DeleteKey("id");
        PlayerPrefs.SetInt("Logged", 0);
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

                SetupUser();
            }
        }		
	}

    public void GetUser(string _username) {
        string userData = PlayerPrefs.GetString(_username, "");
        if (userData != "") {
            string[] data = userData.Split(fieldSeparator);
            user.id = int.Parse(data[0]);
            user.username = _username;
            user.diamondLevel = int.Parse(data[1]);
            user.levelPercent = float.Parse(data[2]);
            user.currentLevelIndex = int.Parse(data[3]);
            Events.LoadUserAchievmentsLocal();
            Data.Instance.figurasData.LoadUserAchievmentsLocal();
        } else {
            user.id = usersCount;
            user.username = _username;
            user.diamondLevel = 0;
            user.levelPercent = 0;
            user.currentLevelIndex = 0;
            usersCount++;
            PlayerPrefs.SetInt("usersCount", usersCount);
        }
        SetupUser();
    }

    void SetupUser() {
        if (user.id > -1) {
            logged = true;
            PlayerPrefs.SetInt("Logged", 1);
            PlayerPrefs.SetString("user", user.username);
            PlayerPrefs.SetString("pass", user.password);
            PlayerPrefs.SetInt("id", user.id);
            if (user.currentLevelIndex <= 0)
                user.currentLevelIndex = 1;
            Data.Instance.userData.actualWorld = user.currentLevelIndex;
            Data.Instance.levelsData.SetLevelPercent(user.levelPercent);
            Data.Instance.levelsData.SetDiamondLevel(user.diamondLevel);
        }
    }

    public void SendData()
    {
        /*StartCoroutine(SaveData(Data.Instance.levelsData.actualDiamondLevel,
            Data.Instance.levelsData.actualLevelPercent,Data.Instance.userData.actualWorld));*/
        SaveDataLocal(Data.Instance.levelsData.actualDiamondLevel,
            Data.Instance.levelsData.actualLevelPercent, Data.Instance.userData.actualWorld);
    }

    public void SaveDataLocal(int diamondLevel, float levelpercent, int levelIndex) {
        user.diamondLevel = diamondLevel;
        user.levelPercent = levelpercent;
        user.currentLevelIndex = levelIndex;
        PlayerPrefs.SetString(user.username, ""+user.id+fieldSeparator+diamondLevel+fieldSeparator+levelpercent+fieldSeparator+ levelIndex);
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
