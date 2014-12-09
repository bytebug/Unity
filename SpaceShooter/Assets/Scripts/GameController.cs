using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
	public GameObject hazard;
	public GameObject enemyShip;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public Text scoreText;
	public Text restartText;
	public Text gameOverText;
	public Text levelText;

	private bool gameOver;
	private bool restart;
	private int score;

	void Start()
	{
		score = 0;
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		levelText.text = "";

		UpdateScore ();

		StartCoroutine(ShowLevel (1));

		StartCoroutine(SpawnWaves ());
	}

	void Update()
	{
		if (restart)
		{
			if(Input.GetKeyDown (KeyCode.R))
			{
				Application.LoadLevel (Application.loadedLevel);
			}
		
		}
	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds (startWait);

		while(true)
		{
			for (int i =0; i<hazardCount;i++) 
			{
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);

				yield return new WaitForSeconds(spawnWait);
			}
			//yield return new WaitForSeconds(waveWait);

			for(int i = 0; i < 3; i++)
			{
				Vector3 spawnPosition = new Vector3(Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Instantiate(enemyShip, spawnPosition, enemyShip.transform.rotation);
				yield return new WaitForSeconds(spawnWait);
			}

			yield return new WaitForSeconds(waveWait);
			/*
			if(gameOver)
			{
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			} */
		}
	}

	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore()
	{
		scoreText.text = "SCORE " + score;
	}

	public void GameOver()
	{
		gameOverText.text = "GAME OVER!";
		gameOver = true;

		restartText.text = "'R' FOR RESTART";
		restart = true;
	}

	IEnumerator ShowLevel(int level)
	{
		levelText.text = "LEVEL " + level;
		yield return new WaitForSeconds (1.0f);
		for (int i = levelText.fontSize; i > 0; i--)
		{
			yield return new WaitForSeconds(0.03f);
			levelText.fontSize--;

		}
		levelText.text = "";
		yield return null;
	
	}
}
