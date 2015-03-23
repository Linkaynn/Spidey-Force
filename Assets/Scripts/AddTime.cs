using UnityEngine;
using System.Collections;

public class AddTime : MonoBehaviour {

    private GameController gameController = null;

    private int minutes = 0;
    private int seconds = 0;

    // Use this for initialization
    void Start()
    {
        gameController = GameController.instance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (gameController.timeVariables[0] > seconds)
            seconds++;

        if (gameController.timeVariables[1] > minutes)
            minutes++;

        this.gameObject.guiText.text = minutes + ":" + seconds + ":00";
    }
}
