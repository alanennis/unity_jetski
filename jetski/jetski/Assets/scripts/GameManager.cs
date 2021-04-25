using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour {



	public int numberOfAsteroids; //the current asteroid count in scene
	public int levelNumber = 1;
	public GameObject asteroid;
	public AlienScript alien;



	public void updateAsteroidCount(int change)
	{
		numberOfAsteroids += change;

		//check to see how many are left

		if (numberOfAsteroids <= 0)
		{
		
			//start new level
			Invoke("startNewLevel", 3f);

		}


	}

	void startNewLevel()
	{

		//new level stuff in here
		levelNumber ++;
		//spawn new rocks

		for (int i = 0; i < levelNumber; i++) {
			Vector2 spawnPosition = new Vector2 (Random.Range (-35f, 35f),26f);

			Instantiate (asteroid, spawnPosition, Quaternion.identity);
			numberOfAsteroids ++;

		}
		//alien setup
		alien.newLevel();

	}

	public bool checkhiscore(int score)
	{
		int hiscore = PlayerPrefs.GetInt ("hiscore");
//		Debug.Log("hiscore" + hiscore);
		if (score > hiscore)
		{
			return true;
		}

		return false;

	}
}