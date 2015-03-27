using UnityEngine;
using System.Collections;

public class AddScore : MonoBehaviour {

    private GameController gameController = null;

    private int score = 0;

	// Use this for initialization
	void Start () {
        gameController = GameController.instance;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (gameController.score > score)
            score++;
        this.gameObject.guiText.text = "" + score;
	}
}
