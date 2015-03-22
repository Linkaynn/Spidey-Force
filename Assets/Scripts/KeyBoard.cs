using UnityEngine;
using System.Collections;

public class KeyBoard : MonoBehaviour {

	public GameController gameController = null;

	public Sounds sound = null;

    private bool isPaused = false;

	void Awake(){
		gameController = GameController.instance;		
		sound = Sounds.instance;
	}

	void Start(){
		gameController = GameController.instance;		
		sound = Sounds.instance;
	}

	// Update is called once per frame
	void Update () {

        sound = Sounds.instance;

        if (Input.GetKeyDown(KeyCode.Escape))
            isPaused = !isPaused;   
	}

    private void OnGUI()
    {
        if (isPaused)
        {
            GUILayout.BeginArea(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200));

            if (GUILayout.Button("Restart"))
            {
                PlayerPrefs.DeleteAll();
                Chronometer chronometer = GameObject.FindWithTag("Chronometer").GetComponent<Chronometer>();
                chronometer.setVariables(0,0);
                sound.changeBaseClip("Base_1");
                gameController.level = 0;
                gameController.changeLevel();
                gameController.nlifes = 3;
                gameController.nSwords = 5;
                for (int i = 0; i < gameController.lifes.Length; i++)
                {
                    gameController.lifes[i].enabled = true;
                }
                for (int i = 0; i < gameController.swords.Length; i++)
                {
                    gameController.swords[i].enabled = true;
                }
                gameController.score = 0;

                gameController.playerOnBoss = false;
            }

            GUILayout.Space(20f);

            if(GUILayout.Button("Load checkpoint"))
            {
                load();
            }

            GUILayout.Space(20f);

            if (GUILayout.Button("Quit"))
            {
                Application.Quit();
            }
            GUILayout.EndArea();
            Time.timeScale = 0;
            sound.stopMusic();
        }
        else
        {
            if (Time.timeScale == 0 && gameController.nlifes > 0)
            {
                Time.timeScale = 1;
                sound.playMusic();
            }
        }
    }

    public void load()
    {
        Chronometer chronometer = GameObject.FindWithTag("Chronometer").GetComponent<Chronometer>();
        chronometer.setVariables(PlayerPrefs.GetInt("Minutos", 0), PlayerPrefs.GetInt("Segundos", 0));
        gameController.setLevel(PlayerPrefs.GetInt("Level", 1));

        for (int i = 0; i < gameController.swords.Length; i++)
        {
            gameController.swords[i].enabled = true;
        }

        gameController.nlifes = PlayerPrefs.GetInt("Lifes", 3);
        for (int i = gameController.lifes.Length - 1; i >= 0; i--)
        {
            if (gameController.nlifes <= i)
                gameController.lifes[i].enabled = false;
            else
                gameController.lifes[i].enabled = true;
        }

        gameController.nSwords = PlayerPrefs.GetInt("Swords", 5);

        for (int i = gameController.swords.Length - 1; i >= 0; i--)
        {
            if (gameController.nSwords <= i)
                gameController.swords[i].enabled = false;
            else
                gameController.swords[i].enabled = true;
        }

        gameController.score = PlayerPrefs.GetInt("Score", 0);

        sound.changeBaseClip("Base_1");

        int aux = PlayerPrefs.GetInt("InBoss", 0); ;

        if (aux == 1)
            gameController.playerOnBoss = true;
        else
            gameController.playerOnBoss = false;
    }
}
