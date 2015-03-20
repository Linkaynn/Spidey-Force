using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour {

	private float smoothValor = 1f;

	void Start(){
		Invoke ("destroySword", 1f);
	}

	private void destroySword(){
		Destroy(this.gameObject);
	}

	void Update(){

		smoothValor = smoothValor * 1.1f;
		transform.Rotate (0f, 0f, -smoothValor);
	}

	void OnCollisionEnter2D (Collision2D other){
		if (other.gameObject.tag == "Enemy") {
			Destroy(other.gameObject);
		}
		Destroy (this.gameObject);
	}
	

}
