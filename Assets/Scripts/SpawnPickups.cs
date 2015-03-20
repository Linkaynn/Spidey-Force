using UnityEngine;
using System.Collections;

public class SpawnPickups : MonoBehaviour {

	private float random;

	public GameObject[] pickups = new GameObject[2];

	public float ini, end;
	

	// Use this for initialization
	void Start () {
		random = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Boss_1.playerOn)
		{
			if (random <= 0)
				random = Random.Range(3,8);
			
			random -= Time.deltaTime;
			
			if (random < 0)
				spawnRandomPickup();
		}
	}

	private void spawnRandomPickup(){

		float aux = Random.Range (0, 101);

		float randomRange = Random.Range (ini, end);

		if (aux <= 30) 
		{
			Instantiate(pickups[0], new Vector3(randomRange, transform.position.y - 1), transform.rotation);
		}else if (aux > 30 && aux <= 60)
		{
			Instantiate(pickups[1], new Vector3(randomRange, transform.position.y - 1), transform.rotation);
		}

	}
}
