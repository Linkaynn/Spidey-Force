using UnityEngine;
using System.Collections;

public class StartBoss : MonoBehaviour {
	
	private Collider2D[] colliders;

	private new Camera2DFollow camera;

	public Vector2 positionOfCamera;

	void Start(){
		colliders = this.gameObject.GetComponentsInChildren<Collider2D>();
		camera = Camera2DFollow.instance;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			for (int i = 1; i < colliders.Length; i++){
				colliders[i].isTrigger = !colliders[i].isTrigger;
			}
			camera.positionation(positionOfCamera);
			Destroy(this.gameObject.collider2D);
		}
	}
}
