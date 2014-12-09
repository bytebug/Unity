using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour 
{
	public GameObject enemyShot;
	public Transform shotSpawn;
	int lastTime;

	private float speed;	// speed at enemyShips move
	private float nextFire;
	private float fireRate = 1.5f;


	// Use this for initialization
	void Start () 
	{
		speed = Random.Range(2.0f, 4.0f);

		//sets the enemyShip in motion
		rigidbody.velocity = transform.forward*speed;

		nextFire = Time.time + fireRate;
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			//GameObject clone = 
			Instantiate (enemyShot, shotSpawn.position, shotSpawn.rotation); // as GameObject;
			audio.Play();

		}

	}

}
