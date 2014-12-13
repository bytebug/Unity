using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
	public GameObject hazard;
	public GameObject enemyShip;
	public Vector3 spawnValues;
	public int hazardCount;
	public int enemyCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public Text scoreText;
	public Text restartText;
	public Text gameOverText;
	public Text levelText;

	private bool gameOver;
	private bool restart;
	private bool spawnFinished;
	private int score;

	private const int MaxLevel = 10;
	private const int MaxLives = 3;

	private int currentLevel = 1;
	private int livesLeft = 3;



	void Start()
	{
		score = 0;
		gameOver = false;
		restart = false;
		spawnFinished = false;
		restartText.text = "";
		gameOverText.text = "";
		levelText.text = "";

		UpdateScore ();

		StartCoroutine(ShowLevel (currentLevel));

		StartCoroutine(SpawnWaves ());
	}

	void Update()
	{
		if(currentLevel < 11 && !gameOver && spawnFinished)
		{
			spawnFinished = false;
			StartCoroutine(ShowLevel (currentLevel));
			StartCoroutine (SpawnWaves ());

		}


		//if(!gameOver)
		//	StartCoroutine(SpawnWaves ());


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
			for (int i =0; i < hazardCount * currentLevel; i++) 
			{
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);

				yield return new WaitForSeconds(spawnWait);
			}

			// spawn enemy ships
			for(int i = 0; i < enemyCount * currentLevel; i++)
			{
				Vector3 spawnPosition = new Vector3(Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Instantiate(enemyShip, spawnPosition, enemyShip.transform.rotation);
				yield return new WaitForSeconds(spawnWait);
			}
			//increse current level by 1 and ends the coroutine
			if(currentLevel < 9)
				currentLevel++;

			spawnFinished = true;

			yield return new WaitForSeconds(waveWait);  // wait for a few seconds before spawning next wave
			yield return null;

			//yield return new WaitForSeconds(waveWait);
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
		levelText.fontSize = 80;	//resets text to its default size
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
