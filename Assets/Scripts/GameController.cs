using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public int score; //Number of the score (Coins)
	private GameObject scoreGUI; //Gui of score
    
	public int nSwords; //Number of swords that pj have
	public GUITexture[] swords = new GUITexture[5]; //Gui of the swords

    public int nlifes = 3; //Number of initial lifes 
	public GUITexture[] lifes = new GUITexture[3]; //Gui of the lifes

    public static GameController instance = null;
    public int level;

	public bool playerOnBoss = false;

    public bool firstDialog = false;

    private Chronometer chronometer;
    public int[] timeVariables = new int[2];

    //FOR DEBUG
    public bool activateDelete = false;

    private void deleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    void Awake(){

        if (instance == null)
        {
            instance = this;
            if (PlayerPrefs.GetInt("firstGame", 0) == 0)
            {
                firstDialog = true;
            }
        }
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

    }

	// Use this for initialization
	void Start ()
    {

        chronometer = GameObject.FindWithTag("Chronometer").GetComponent<Chronometer>();

        score = 0;
        scoreGUI = GameObject.FindWithTag("Score"); 


        lifes = GameObject.FindWithTag("GuiLife").GetComponentsInChildren<GUITexture>();
        level = 1;

		swords = GameObject.FindWithTag ("GuiSword").GetComponentsInChildren<GUITexture> ();
		nSwords = 5;
    }
	
	// Update is called once per frame
	void Update () 
    {

        timeVariables = chronometer.getVariables();

        if (activateDelete)
            deleteAll();

        if (PlayerPrefs.GetInt("isLoading", 0) == 1)
        {
            PlayerPrefs.SetInt("isLoading",0);
            GameObject.FindWithTag("Player").GetComponent<KeyBoard>().load();
        }

        if (scoreGUI == null)
        {
            scoreGUI = GameObject.FindWithTag("Score");
        }
        else
        {
            scoreGUI.guiText.fontSize = 20;
            scoreGUI.guiText.color = Color.black;

            scoreGUI.guiText.text = " " + score;
        }
	}

	//Controller of change lifes (True: +1, False: -1)
    public void changeLifes(bool a)
    {
        if (nlifes == 0)
            return;

        if (nlifes == 3 && a)
            return;

        if (a)
        {
            lifes[nlifes].enabled = a;
            nlifes++;
        }
        else
        {
            lifes[nlifes - 1].enabled = a;
            nlifes--;
        }
    }

	//Controller of change swords (True: +1, False: -1)
	public void changeSwords(bool a)
	{
		if (a)
		{
			swords[nSwords].enabled = a;
			nSwords++;
		}
		else
		{
			swords[nSwords - 1].enabled = a;
			nSwords--;
		}
	}

	//Controller of change levels
	public void changeLevel()
	{
		level++;
        if (level > 1)
            save();
		Application.LoadLevel(level);
    }

    //Set the level
    public void setLevel(string level)
    {
        Application.LoadLevel(level);
    }

    private void save()
    {
        Chronometer chronometer = GameObject.FindWithTag("Chronometer").GetComponent<Chronometer>();
        int[] timeVariables = chronometer.getVariables();
        PlayerPrefs.SetInt("Segundos", timeVariables[0]);
        PlayerPrefs.SetInt("Minutos", timeVariables[1]);
        PlayerPrefs.SetInt("Lifes", nlifes);
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.SetInt("Swords", nSwords);
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("haveCheckpoint", 1);
        PlayerPrefs.SetInt("firstGame", 1);

        int aux;

        if (playerOnBoss)
            aux = 1;
        else
            aux = 0;

        PlayerPrefs.SetInt("InBoss", aux);
    }
}
