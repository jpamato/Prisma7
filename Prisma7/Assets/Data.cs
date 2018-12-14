using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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

	public enum minigamesScenes
	{
		Figuras=0,
		Combinatorias=1,
		Pociones=2,
		Grilla=3
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
		} 

        this.currentLevel = aLevelName;
        Time.timeScale = 1;
		fadePanel.SetOn ();
		currentLevelIndex = SceneManager.GetSceneByName (aLevelName).buildIndex;
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
		Scene actual = SceneManager.GetActiveScene ();
		currentLevelIndex = actual.buildIndex;
		currentLevel = actual.name;

    }
    
	public void Reset(){
		PlayerPrefs.DeleteAll ();
	}

	public void CaptureScreen(){
		if(currentLevel=="CaptureQR")
			LoadScene ("World");
		else
			LoadScene ("CaptureQR");
	}

}