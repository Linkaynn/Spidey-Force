using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	/*MOVING*/
    public float speed = 1.0f; //Player velocity
    public float force; //Jump force
	private RaycastHit hit; 
	private Vector3 move;
	private Animator animator;
	public float moveTime = 0.1f;
	/*******/

	/*JUMPING VARIABLES*/
    public bool onGround = true; //Means if the player is on ground or not
    public bool doubleJump = false; //Means if the player have a second jump or not
    float auxTime = 0; //Delay time to double jump
    public Transform checkGround;  //Object about the script will check if is on ground or not
    public float checkRadius = 0.07f;
	public LayerMask groundMask; //Capa del suelo para saber sobre qué queremos saltar
	/******************/

	/*SPAWN THINGS*/
	public GameObject[] pickups = new GameObject[2]; 
	public GameObject sword;
	public GameObject[] particles;
    /*PARTICLES
     * 0: Enemy Death particles
     * 1: Player jum particles
     */
	/*************/

	private Sounds sound;

    private GameController gameController;

    private float timeToFinal = 1.5f;

    void Start()
    {
        gameController = GameController.instance;
		sound = Sounds.instance;
        animator = GetComponent<Animator>();
    }

    // se llama en cada intervención de las físicas
    void FixedUpdate()
    {
        if (gameController.nlifes == 0)
        {
            sound.stopMusic();
            PlayerPrefs.SetInt("End", 1);
            timeToFinal -= Time.deltaTime;
            if (timeToFinal <= 0)
                gameController.setLevel("Final");
            return;
        }

        /***MOVIMIENTO***/
        move = new Vector2(Input.GetAxis("Horizontal"), 0);
        Vector3 newPosition = transform.position + (move * speed * Time.deltaTime);

        double aux = transform.position.x - newPosition.x;
       
        if (aux < 0)
            transform.rotation = new Quaternion(0, 0, 0, 0);
        else if (aux>0)
            transform.rotation = new Quaternion(0, 180, 0, 0);

        if (transform.position != newPosition)
        {
            transform.position += move * speed * Time.deltaTime;
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
              
        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            Vector3 objectForward = transform.InverseTransformDirection(Vector3.forward);
            transform.rotation = Quaternion.LookRotation(objectForward, hit.normal);            
        }


        /***SALTO***/

        onGround = Physics2D.OverlapCircle(checkGround.position, checkRadius, groundMask);

        if (onGround)
            doubleJump = true;

        if (((Input.GetAxis("Vertical") > 0) || Input.GetKey(KeyCode.Space)) && onGround)
        {
            auxTime = 0.5f;
            animator.SetTrigger("isJumping");
            rigidbody2D.AddForce(new Vector2(0, force));
			sound.playSound("jump");
            Vector3 location = transform.position + new Vector3(0, -0.75f);
            instantiateParticles(particles[1], location);

        }

        if (PlayerPrefs.GetInt("Boss_Killed", 0) >= 2)
        {
            if (!onGround)
                auxTime -= Time.deltaTime;

            if (((Input.GetAxis("Vertical") > 0) || Input.GetKey(KeyCode.Space)) && doubleJump && auxTime <= 0)
            {
                doubleJump = false;
                animator.SetTrigger("isJumping");
                rigidbody2D.AddForce(new Vector2(0, force));
                sound.playSound("jump");
                Vector3 location = transform.position + new Vector3(0, -0.75f);
                instantiateParticles(particles[1], location);

            }
        }

        /***SPRINT***/

        if (Input.GetKeyDown(KeyCode.LeftShift) && PlayerPrefs.GetInt("Boss_Killed", 0) >= 1)
        {
            if (transform.rotation.y == 0)
                rigidbody2D.AddForce(new Vector2(300,0));
            else
                rigidbody2D.AddForce(new Vector2(-300,0));
            sound.playSound("shift");
            Vector3 location = transform.position + new Vector3(0, -0.75f);
            instantiateParticles(particles[1], location);
        }

		/***ESPADA***/

		if (Input.GetKeyUp (KeyCode.E) && gameController.nSwords > 0) {

			GameObject gSword;
			Quaternion actual = transform.rotation;

			if (actual.y == 0){
				gSword = Instantiate(sword, (transform.position + new Vector3(1,0,0)), transform.rotation) as GameObject;
				gSword.rigidbody2D.AddForce(new Vector2(450,0));
			}else{
				gSword = Instantiate(sword, (transform.position + new Vector3(-1,0,0)), transform.rotation) as GameObject;
				gSword.rigidbody2D.AddForce(new Vector2(-450,0));
			}

			gameController.changeSwords(false);

			sound.playSound("knife");

		}

		/***CAIDA***/
		if (!gameController.playerOnBoss) {
			if (rigidbody2D.velocity.y < -25) {
				gameController.changeLifes(false);
				rigidbody2D.AddForce(new Vector2(0, force));
				CheckIfGameOver();
				sound.playSound("hit");
			}
		}
    }

    /***COLISIONES***/
    void OnTriggerEnter2D(Collider2D other)
    {

        if (gameController.nlifes == 0)
            return;

        /***MONEDAS***/
        if (other.gameObject.tag == "Coin")
        {
            gameController.score += 10;
            Destroy(other.gameObject);
			sound.playSound("coin");
        }
        /***VIDAS***/
        else if (other.gameObject.tag == "Life")
        {
                       
            Destroy(other.gameObject);

            if (gameController.nlifes < 3) //Sólo incrementamos las vidas si tenemos menos de 3
            {
                gameController.changeLifes(true);          
            }

			sound.playSound("life");
		/***ESPADA***/
        } else if (other.gameObject.tag == "Sword"){
			Destroy(other.gameObject);

			if (gameController.nSwords < 5){
				gameController.changeSwords(true);
			}

			sound.playSound("sword");
		}
        /***PUERTA***/
        else if (other.gameObject.tag == "Door")
        {
            ChangeLevel();            
        }
    }

    /***ENEMIGOS***/
    void OnCollisionEnter2D(Collision2D other)
    {

        if (gameController.nlifes == 0)
            return;

        if (other.gameObject.tag == "Enemy"){

            Vector2 vFinal = calculateVFinal(other);

            Enemy o = other.gameObject.GetComponent<Enemy>();
            
            if (other.gameObject.name == "RedEnemy")
                o.ChangeDirection();

            if (vFinal.y < -3){

				instantiateParticles(particles[0], other.transform.position);

                o.lifes--;

                if (o.lifes <= 0)
                {
                    gameController.score += (int)o.score;
                    instantiatePickup(other);
                }

                rigidbody2D.AddForce(new Vector2(0, -vFinal.y * 100));

				sound.playSound("enemiedie");
            }
            else{
                gameController.changeLifes(false);
                CheckIfGameOver();
				sound.playSound("hit");
            }
        }
        else if (other.gameObject.tag == "Boss")
        {
            Vector2 vFinal = calculateVFinal(other);
            if (vFinal.y > -3)
            {
                gameController.changeLifes(false);
                CheckIfGameOver();
            }
        }
    }

    private Vector2 calculateVFinal(Collision2D other)
    {
        // Physic calculation of the relative velocity of the hitter against the hitted (Do not touch!)
        return (other.rigidbody.mass * other.relativeVelocity / (rigidbody2D.mass + other.rigidbody.mass));
    }

	/* PICKUPS
	 * 0: Life
	 * 1: Sword
	 */
	private void instantiatePickup(Collision2D other){
		int numero = Random.Range(0, 101);
		
		if(numero >= 0 && numero <= 35)
		{
			Instantiate (pickups[0], other.transform.position, other.transform.rotation);
		} else if (numero > 35 && numero <= 60){
			Instantiate (pickups[1], other.transform.position, other.transform.rotation);
		}
	}

	private void instantiateParticles(GameObject particles, Vector3 other){
		Instantiate (particles, other, new Quaternion(0,1356,0,0));
	}

    void CheckIfGameOver()
    {
        if (gameController.nlifes == 0)
        {
            animator.SetTrigger("isDead");
        }
    }

    void ChangeLevel()
    {
        gameController.changeLevel();
    }

}

