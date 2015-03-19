using UnityEngine;
using System.Collections;

public class KeyBoard : MonoBehaviour {

	public static GameController instance = null;


	void Start(){
		instance = GameController.instance;		
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Q))
			Application.Quit();
		
		if (Input.GetKey (KeyCode.R)) {
			instance.level = -1;
			instance.ChangeLevel();
			instance.nlifes = 3;
			for (int i = 0; i < instance.lifes.Length; i++){
				instance.lifes[i].enabled = true;
			}
			instance.score = 0;
		}
	}
}
