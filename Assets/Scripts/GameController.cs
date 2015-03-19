using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	private GameObject scoreGUI; //Texto con la puntuación
    public int score; //Puntuación (Monedas)

	public int nSwords;
	public GUITexture[] swords = new GUITexture[5];

    public int nlifes = 3; //Número de vidas iniciales del jugador 
	public GUITexture[] lifes = new GUITexture[3]; //Texturas para representar las vidas del jugador

    public static GameController instance = null;
    public int level;

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

	public void changeSwords(bool a)
	{
		if (nSwords == 0)
			return;
		
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
	
	public void changeLevel()
	{
		//print("Cargando Level1");
		level++;
		Application.LoadLevel(level);
    }

	public int getScore(){
		return score;
	}
}
