using UnityEngine;
using System.Collections;

public class FinalScriptRestart : MonoBehaviour {

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
        restart();
    }

    private void restart()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("End", 0);
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
}
