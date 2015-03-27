using UnityEngine;
using System.Collections;

public class FinalScriptLoad : MonoBehaviour {

    private GameController gameController = null;

    private Sounds sound = null;

    void Start()
    {
        gameController = GameController.instance;
        sound = Sounds.instance;
    }

    void OnMouseOver()
    {
        guiText.color = Color.green;
        guiText.fontSize = 32;
    }


    void OnMouseExit()
    {
        guiText.color = Color.black;
        guiText.fontSize = 30;
    }

    void OnMouseDown()
    {
        load();
    }
    
    private void load()
    {
        PlayerPrefs.SetInt("End", 0);

        Chronometer chronometer = GameObject.FindWithTag("Chronometer").GetComponent<Chronometer>();
        chronometer.setVariables(PlayerPrefs.GetInt("Minutos", 0), PlayerPrefs.GetInt("Segundos", 0));

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

        gameController.level = PlayerPrefs.GetInt("Level", 1) - 1;
        gameController.changeLevel();
    }
}
