using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour {

	private float smoothValor = 1f;

    private Sounds sound;

	void Start(){
		Invoke ("destroySword", 1f);
        sound = Sounds.instance;
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

            Enemy o = other.gameObject.GetComponent<Enemy>();

            if (other.gameObject.name == "RedEnemy")
                o.ChangeDirection();

            o.lifes -= 2;

            sound.playSound("swordh");

            Destroy(this.gameObject);
		} else if (other.gameObject.tag != "Weapon") {
            sound.playSound("swordnh");
            gameObject.SetActive(false);
            Invoke("destroySword", 1f);
        }
		
	}
	

}
