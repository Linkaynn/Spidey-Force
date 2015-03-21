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
    public Transform checkGround;  //Object about the script will check if is on ground or not
    public float checkRadius = 0.07f;
	public LayerMask groundMask; //Capa del suelo para saber sobre qué queremos saltar
	/******************/

	/*SPAWN THINGS*/
	public GameObject[] pickups = new GameObject[2]; 
	public GameObject sword;
	public ParticleSystem particles;
	/*************/

	private Sounds sound;

    private GameController gameController;

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
            return;

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

        if (((Input.GetAxis("Vertical") > 0) || Input.GetKey(KeyCode.Space)) && onGround)
        {
            animator.SetTrigger("isJumping");
            rigidbody2D.AddForce(new Vector2(0, force));  
			sound.playSound("jump");

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
			if (rigidbody2D.velocity.y < -18.5) {
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
        if (other.gameObject.tag == "Enemy"){

            Enemy o = other.gameObject.GetComponent<Enemy>();
            
            if (other.gameObject.name == "RedEnemy")
                o.ChangeDirection();

			// Physic calculation of the relative velocity of the hitter against the hitted (Do not touch!)
            Vector2 vFinal = other.rigidbody.mass * other.relativeVelocity / (rigidbody2D.mass + other.rigidbody.mass);

            if (vFinal.y < -3){

				instantiateParticles(particles, other);

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

	private void instantiateParticles(ParticleSystem particles, Collision2D other){
		Instantiate (particles, other.transform.position, new Quaternion(0,1356,0,0));
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

