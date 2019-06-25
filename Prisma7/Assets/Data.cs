using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Android;
using System;

public class Data : MonoBehaviour
{

    public bool isArcade;

    const string PREFAB_PATH = "Data";
    
    static Data mInstance = null;
	public FigurasData figurasData;
	public CombinatoriasData combinatoriasData;
	public PocionesData pocionesData;
	public GrillaData grillaData;
	public LevelsData levelsData;
	public InputManager inputManager;
	public UserData userData;
	public TipsManager tipsManager;
	public UI ui;
	public FadePanel fadePanel;
	public MusicManager musicManager;
    public UsersController usersDB;

    public int lastMinigame;
    public enum minigamesScenes {
        Figuras = 0,
        Combinatorias = 1,
        Pociones = 2,
        Grilla = 3
    }

    public string GetNextMGame() {
        int mgCount = Enum.GetNames(typeof(Data.minigamesScenes)).Length;
        lastMinigame++;
        if (lastMinigame >= mgCount)
            lastMinigame = 0;

        return ((Data.minigamesScenes)lastMinigame).ToString();
    }

	public static Data Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = FindObjectOfType<Data>();

                if (mInstance == null)
                {
                    GameObject go = Instantiate(Resources.Load<GameObject>(PREFAB_PATH)) as GameObject;
                    mInstance = go.GetComponent<Data>();
                }
            }
            return mInstance;
        }
    }
    public string currentLevel;
	public int currentLevelIndex;
	public string lastLevel = "World";
    public void LoadScene(string aLevelName)
    {
		if (aLevelName == "World") {


			musicManager.SetIngameMusic ();

			switch (userData.actualWorld) {
			case 1:
				aLevelName = "World";
				break;
			case 2:
				aLevelName = "World2";
				break;
			case 3:
				aLevelName = "World3";
				break;
			default:
				aLevelName = "World";
				break;
			}

            if (currentLevelIndex != userData.actualWorld){
                currentLevelIndex = userData.actualWorld;
            }
        }
		lastLevel = currentLevel;
        currentLevel = aLevelName;
        Time.timeScale = 1;
		fadePanel.SetOn ();		
        Invoke ("Delayed", 1);
    }
	void Delayed()
	{	
		SceneManager.LoadScene(currentLevel);
	}
    void Awake()
    {
		QualitySettings.vSyncCount = 1;

        if (!mInstance)
            mInstance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }
        if(isArcade)
            Cursor.visible = false;

        DontDestroyOnLoad(this.gameObject);

		figurasData = GetComponent<FigurasData> ();
		levelsData = GetComponent<LevelsData> ();
		combinatoriasData = GetComponent<CombinatoriasData> ();
		pocionesData = GetComponent<PocionesData> ();
		grillaData = GetComponent<GrillaData> ();
		inputManager = GetComponent<InputManager> ();
		tipsManager = GetComponent<TipsManager> ();
		musicManager = GetComponent<MusicManager> ();
        usersDB = GetComponent<UsersController>();
        Scene actual = SceneManager.GetActiveScene ();
		currentLevelIndex = actual.buildIndex;
		currentLevel = actual.name;

    }
    
	public void Reset(){
		PlayerPrefs.DeleteAll ();
        Application.Quit();
    }

	public void Back(){
		LoadScene (lastLevel);
	}

	public void CaptureScreen(){

        if (Permission.HasUserAuthorizedPermission(Permission.Camera)) {
            Events.ClickSfx();
            if (currentLevel == "CaptureQR")
                LoadScene("World");
            else
                LoadScene("CaptureQR");
        } else {
            Permission.RequestUserPermission(Permission.Camera);
        }
        
	}

}