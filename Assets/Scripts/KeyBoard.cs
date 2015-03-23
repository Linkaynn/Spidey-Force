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

        /*PAUSE*/
        if (isPaused)
        {

            GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 120, 200, 150), "Pause");

            GUILayout.BeginArea(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200));

            GUILayout.Space(20f);

            if (GUILayout.Button("Restart"))
            {
                if (PlayerPrefs.GetInt("haveCheckpoint", 0) == 1)
                {
                    if (UnityEditor.EditorUtility.DisplayDialog("Restart with load file",
                    "Are you sure you want start a new game having one? " +
                    "If you restart your load file will be rewritten.",
                    "Yes, I'm sure",
                    "No, please"))
                    {
                        restart();
                    }
                }
            }

            GUILayout.Space(20f);

            if (PlayerPrefs.GetInt("haveCheckpoint", 0) == 1)
            {
                if (GUILayout.Button("Load checkpoint"))
                {
                    load();
                }
            }
            else
            {
                GUILayout.Button("Can't find load file");
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
        /******/

        /*FIRST DIALOG*/
        if (gameController.firstDialog)
        {
            Time.timeScale = 0;
            GUI.BeginGroup(new Rect((Screen.width - 400) / 2, (Screen.height - 200) / 2, 400, 200));
            GUI.Box(new Rect(0, 0, 400, 200), "Bienvenido a Spidey Force");
            GUI.Label(new Rect(5, 20, 395, 190), "A medida que vayas evolucionando en tu aventura tendrás que enfrentarte"
                + " a enemigos más peligrosos. Si superas una prueba cada tres niveles obtendrás "
                + "poderes o mejoras de estos. Recuerda que cada vez que pasas de nivel se guardará "
                + "automáticamente tu progreso. Por último recordarte que el daño de las espadas es de dos "
                + "vidas pero no obtendrás ninguna recompensa si destruyes al enemigo de esta forma."
                + "\n\n¡Mucha suerte en tu aventura!");
            if (GUI.Button(new Rect((400 - 100) / 2, 160, 100, 30), "¡Entendido!"))
            {
                gameController.firstDialog = false;
            }
            GUI.EndGroup();
        }
        /*********/

        if (PlayerPrefs.GetInt("Boss_Killed", 0) == 1 && PlayerPrefs.GetInt("New_Skill", 0) == 1)
        {
            Time.timeScale = 0;
            GUI.BeginGroup(new Rect((Screen.width - 400) / 2, (Screen.height - 200) / 2, 400, 200));
            GUI.Box(new Rect(0, 0, 400, 200), "¡Nueva habilidad!");
            GUI.Label(new Rect(5, 20, 395, 190), "¡Felicidades has derrotado al primer jefe!"
                + " No creas que este jefe es el más poderoso, ¡encontrarás más por tu aventura!. "
                + "Por tu victoria se te ha concedido un nuevo poder, ¡Impulso! "
                + "Con esta habilidad podrás impulsarte hacia cualquiera de los dos lados. Tan solo "
                + "tienes que pulsar el SHIFT y tu personaje se impulsará."
                + "\n\n¡Mucha suerte en tu aventura!");
            if (GUI.Button(new Rect((400 - 100) / 2, 160, 100, 30), "¡Entendido!"))
            {
                PlayerPrefs.SetInt("New_Skill",0);
            }
            GUI.EndGroup();
        }

    }

    private void restart()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("firstGame", 1);
        Chronometer chronometer = GameObject.FindWithTag("Chronometer").GetComponent<Chronometer>();
        chronometer.setVariables(0, 0);
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

    public void load()
    {
        Chronometer chronometer = GameObject.FindWithTag("Chronometer").GetComponent<Chronometer>();
        chronometer.setVariables(PlayerPrefs.GetInt("Minutos", 0), PlayerPrefs.GetInt("Segundos", 0));
        gameController.setLevel("" + PlayerPrefs.GetInt("Level", 1));

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
