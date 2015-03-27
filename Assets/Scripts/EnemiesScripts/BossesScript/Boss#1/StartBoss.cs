using UnityEngine;
using System.Collections;

public class StartBoss : MonoBehaviour {
	
	private Collider2D[] colliders;

	private GameController gameController;
	private Sounds sound;

	public Vector2 position;

	public static Vector2 positionOfCamera;

	void Start(){
		colliders = this.gameObject.GetComponentsInChildren<Collider2D>();

		gameController = GameController.instance;

		sound = Sounds.instance;

		positionOfCamera = position;
	}

	void Update(){
		if (!gameController.playerOnBoss && Boss_1.lifes <= 0) {
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			for (int i = 1; i < colliders.Length; i++){
				colliders[i].isTrigger = !colliders[i].isTrigger;
			}
			Destroy(this.gameObject.collider2D);

			sound.changeBaseClip("Boss");

			gameController.playerOnBoss = true;
		}
	}

	public static Vector2 getPosition(){
		return positionOfCamera;
	}
}
