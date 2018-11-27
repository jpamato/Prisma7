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
	public UI ui;

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
        this.currentLevel = aLevelName;
        Time.timeScale = 1;
		currentLevelIndex = SceneManager.GetSceneByName (aLevelName).buildIndex;
        SceneManager.LoadScene(aLevelName);
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
		Scene actual = SceneManager.GetActiveScene ();
		currentLevelIndex = actual.buildIndex;
		currentLevel = actual.name;

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //Application.Quit();
			SceneManager.LoadScene(0);
		}else if(Input.GetKeyDown(KeyCode.Alpha2))
		{
			//Application.Quit();
			SceneManager.LoadScene(1);
		}
    }
}