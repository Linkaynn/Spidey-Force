using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    private Vector3 move;
    public float speed;
	public float originalSpeed;
    private int n = 1;
    private float alpha = 0;

    public float score;
    public int lifes;

	private Transform[] groundCheckers;
	public bool[] booleans;
	/* Checkers position
	 * 0: Enemie
	 * 1: right sup
	 * 2: right up
	 * 3: right
	 * 4: right down
	 * 5: right sdown
	 */

	public float checkRadius;
	public LayerMask groundMask;
	public LayerMask playerMask;
	public LayerMask enemyMask;

	public bool isJumping;
	public bool playerNear;

	void Start(){

		groundCheckers = GetComponentsInChildren<Transform> ();
		booleans = new bool[6];

		for (int i = 0; i < booleans.Length; i++){
			booleans[i] = false;
		}

		isJumping = false;
		playerNear = false;

		if (speed < 2)
			speed = 2;

		originalSpeed = speed;

	}

    void FixedUpdate()
    {
        if (lifes <= 0)
            Destroy(this.gameObject);

        move = new Vector3(n, 0, 0);

        transform.position += move * speed * Time.deltaTime;

		transform.rotation = new Quaternion (0, alpha, 0, 0);

		if (!isJumping) {
            checkEnemyNear();
			checking ();
		}

		checkDown ();
		checkSide ();
    }

	private void checking(){
		for (int i = 0; i < booleans.Length; i++){
			booleans[i] = Physics2D.OverlapCircle(groundCheckers[i].transform.position, checkRadius, groundMask);
		}

		if (gameObject.name == "RedEnemy")
			playerNear = Physics2D.OverlapCircle(groundCheckers[3].transform.position, checkRadius, playerMask);

	}

	private void checkDown(){
		if (booleans [4] == false && booleans[5] == false)
			ChangeDirection ();
	}

	private void checkSide(){

		if ((booleans [3] == true && booleans [2] == true)) {
			ChangeDirection ();
			return;
		}

		if ((booleans[3] == true && booleans[2] == false) || playerNear) {
			rigidbody2D.AddForce (new Vector2 (0, 300));  
			isJumping = true;
			playerNear = false;
			booleans[3] = false;
			speed /= 1.5f;
		}
	}

	private void cancelJump(){
		isJumping = false;
	}

	private void checkEnemyNear(){
		if (Physics2D.OverlapCircle (groundCheckers [3].transform.position, checkRadius, enemyMask))
			ChangeDirection ();
	}

    public void ChangeDirection()
    {
        n *= -1;
        alpha = (alpha == 180) ? 0 : 180;
        transform.rotation = new Quaternion(0, alpha, 0, 0);
    }

	void OnCollisionEnter2D(Collision2D other){
		isJumping = false;
		speed = originalSpeed;
		if (other.gameObject.tag == "Player") {
			isJumping = true;
			rigidbody2D.AddForce (new Vector2 (0, 50));
		
		}
	}
}
