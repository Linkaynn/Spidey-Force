using UnityEngine;
using System.Collections;

public class Boss_1 : MonoBehaviour {

	public static bool playerOn = false;

	private Sounds sound;

	private new Camera2DFollow camera;

	private float random = 0f;

	private Vector3 move;
	public float speed;
	public float originalSpeed;
	private int n = -1;
	private float alpha = 0;

	private float lifes;

	// Use this for initialization
	void Start () {
		sound = Sounds.instance;
		camera = Camera2DFollow.instance;
		lifes = 0.25f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (playerOn) {

			if (random <= 0)
				random = Random.Range(1,4);

			random -= Time.deltaTime;

			move = new Vector3(n, 0, 0);
			
			transform.position += move * speed * Time.deltaTime;
			
			transform.rotation = new Quaternion (0, alpha, 0, 0);

			if (random < 0)
				checkIfJump();

			if (lifes <= 0){
				sound.changeBaseClip("Base_1");
				playerOn = false;
				Destroy (this.gameObject);
				camera.finishBoss();
			}
		}
	
	}

	public void ChangeDirection()
	{
		n *= -1;
		alpha = (alpha == 180) ? 0 : 180;
		transform.rotation = new Quaternion(0, alpha, 0, 0);
	}

	private void checkIfJump()
	{
		if (Random.Range (1, 101) > 50) 
		{
			rigidbody2D.AddForce (new Vector2 (0, 500));
		}
	}

	void OnCollisionEnter2D (Collision2D other){
		ChangeDirection ();
		if (other.gameObject.tag == "Player") 
		{
			// Physic calculation of the relative velocity of the hitter against the hitted (Do not touch!)
			Vector2 vFinal = other.rigidbody.mass * other.relativeVelocity / (rigidbody2D.mass + other.rigidbody.mass);

			if (vFinal.y > 3)
			{
				lifes--;
				sound.playSound("hit");
			}

			other.gameObject.rigidbody2D.AddForce(new Vector2(0, 800));
		}

		if (other.gameObject.tag == "Weapon") 
		{
			lifes -= 0.25f;
		}
	}
}
