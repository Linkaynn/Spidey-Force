using UnityEngine;
using System.Collections;

public class KeyBoard : MonoBehaviour {

	public GameController gameController = null;

	public Sounds sound = null;


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
		if (Input.GetKey(KeyCode.Q))
			Application.Quit();

        sound = Sounds.instance;

		if (Input.GetKey (KeyCode.R)) {
			sound.changeBaseClip("Base_1");
			gameController.level = -1;
			gameController.changeLevel();
			gameController.nlifes = 3;
			gameController.nSwords = 5;
			for (int i = 0; i < gameController.lifes.Length; i++){
				gameController.lifes[i].enabled = true;
			}
			for (int i = 0; i < gameController.swords.Length; i++){
				gameController.swords[i].enabled = true;
			}
			gameController.score = 0;
		}
	}
}
