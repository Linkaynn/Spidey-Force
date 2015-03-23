using UnityEngine;
using System.Collections;

public class Boss_1 : MonoBehaviour {

	private GameController gameController; //Interface of the controller of all the game
	private Sounds sound; //Interface of sound
	private new Camera2DFollow camera; //Interface of camera

	private float random = 0f; //Random number for jumping

	public GUIText bossLifes;


	/**Variables of moving**/
	private Vector3 move;
	public float speed;
	public float originalSpeed;
	private int n = -1;
	private float alpha = 0;
	/**********************/

	public static float lifes; // Life of the boss

    private Animator animator;

    private float delay = 2;

	// Use this for initialization
	void Start () {
		gameController = GameController.instance;
		sound = Sounds.instance;
		lifes = 10;
        animator = GetComponent<Animator>();
        gameObject.rigidbody2D.gravityScale = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (gameController.playerOnBoss) {

            gameObject.rigidbody2D.gravityScale = 1;

            delay -= Time.deltaTime;

            if (delay > 0)
                return;

            animator.SetBool("Walking", true);

			bossLifes.gameObject.SetActive(true);

			bossLifes.text = "" + lifes;

			if (random <= 0)
				random = Random.Range(1,4);

			random -= Time.deltaTime;

			move = new Vector3(n, 0, 0);
			
			transform.position += move * speed * Time.deltaTime;
			
			transform.rotation = new Quaternion (0, alpha, 0, 0);

			if (random < 0)
				checkIfJump();

			if (lifes <= 0){
				bossLifes.gameObject.SetActive(false);
				sound.changeBaseClip("Base_1");
				gameController.playerOnBoss = false;
				gameController.score += 500;
                gameController.changeLifes(true);
                gameController.changeLifes(true);
                gameController.changeLifes(true);
                PlayerPrefs.SetInt("Boss_Killed", PlayerPrefs.GetInt("Boss_Killed",0) + 1);
                PlayerPrefs.SetInt("New_Skill", 1);
				Destroy (this.gameObject);
			}
		}
	
	}

	private void ChangeDirection()
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

		if (other.gameObject.tag == "Weapon") 
		{
			lifes -= 0.25f;
            sound.playSound("swordh");
            Destroy(other.gameObject);
            return;
		}

		if (other.gameObject.tag == "Player") 
		{
			// Physic calculation of the relative velocity of the hitter against the hitted (Do not touch!)
			Vector2 vFinal = other.rigidbody.mass * other.relativeVelocity / (rigidbody2D.mass + other.rigidbody.mass);

            if (vFinal.y > 3)
            {
                lifes--;
                sound.playSound("hit");
                other.gameObject.rigidbody2D.AddForce(new Vector2(0, 800));
            }

            return;
		}
        ChangeDirection();



	}
}
