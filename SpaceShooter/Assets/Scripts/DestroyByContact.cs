﻿using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameController;

	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");

		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter(Collider other)
	{
		//Debug.Log (other.name);
		if (other.tag == "Boundary") 
		{
			return;
		}
		if (other.tag == "PlayerBolt")
		{
			Instantiate (explosion, transform.position, transform.rotation);
			gameController.AddScore(scoreValue);
			Destroy(other.gameObject);
			Destroy(gameObject);
		}
		else if (other.tag == "Player")
		{
			Instantiate (explosion, transform.position, transform.rotation);
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver();
			Destroy(other.gameObject);
			Destroy(gameObject);
		}

		/*gameController.AddScore (scoreValue);

		Destroy (other.gameObject);
		Destroy (gameObject);
		*/
	}
}
