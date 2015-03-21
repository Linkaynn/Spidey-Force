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

	public int getScore(){
		return score;
	}

    void Awake(){
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

    }

	// Use this for initialization
	void Start ()
    {
        score = 0;
        scoreGUI = GameObject.FindWithTag("Score"); 


        lifes = GameObject.FindWithTag("GuiLife").GetComponentsInChildren<GUITexture>();
        level = 0;

		swords = GameObject.FindWithTag ("GuiSword").GetComponentsInChildren<GUITexture> ();
		nSwords = 5;
    }
	
	// Update is called once per frame
	void Update () 
    {
		if (scoreGUI == null)
			scoreGUI = GameObject.FindWithTag("Score");
		scoreGUI.guiText.fontSize = 20;
		scoreGUI.guiText.color = Color.black;

        scoreGUI.guiText.text = " " + score;
	}

	//Controller of change lifes (True: +1, False: -1)
    public void changeLifes(bool a)
    {
        if (nlifes == 0)
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
		Application.LoadLevel(level);
    }
}
