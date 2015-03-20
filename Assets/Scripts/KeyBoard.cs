using UnityEngine;
using System.Collections;

public class KeyBoard : MonoBehaviour {

	public GameController gameController = null;

	public Sounds sound;

	public new Camera2DFollow camera;


	void Awake(){
		gameController = GameController.instance;		
		sound = Sounds.instance;
		camera = Camera2DFollow.instance;
	}

	void Start(){
		gameController = GameController.instance;		
		sound = Sounds.instance;
		camera = Camera2DFollow.instance;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Q))
			Application.Quit();

		camera = Camera2DFollow.instance;

		if (Input.GetKey (KeyCode.R)) {
			sound.changeBaseClip("Base_1");
			camera.finishBoss();
			gameController.level = -1;
			gameController.changeLevel();
			gameController.nlifes = 3;
			for (int i = 0; i < gameController.lifes.Length; i++){
				gameController.lifes[i].enabled = true;
			}
			gameController.score = 0;
		}
	}
}
