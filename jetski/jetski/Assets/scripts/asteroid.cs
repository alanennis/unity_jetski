﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroid : MonoBehaviour {

	public float maxThrust;
	public float maxTorque;
	public Rigidbody2D rb;

	public float screenTop;
	public float screenBottom;
	public float screenLeft;
	public float screenRight;

	public int asteroidSize; // 3 is large, 2 med, 1 is small
	public GameObject asteroidMedium;
	public GameObject asteroidSmall;

	public int points;
	public GameObject player;

	public GameObject explosion;
	public AudioSource asteroidDie;

	public GameManager gm;






	// Use this for initialization
	void Start () {
		// add a random torque and thrust
		Vector2 thrust = new Vector2(Random.Range (-maxThrust, maxThrust), Random.Range (-maxThrust, maxThrust));
		float torque = Random.Range (-maxTorque, maxTorque);

		rb.AddForce (thrust);
		rb.AddTorque (torque);

		//find the player
		player = GameObject.FindWithTag("Player");

		//find the game manager
		gm = GameObject.FindObjectOfType<GameManager>();



		
	}

	// Update is called once per frame
	void Update () {

		Vector2 newPos = transform.position;
	
	

		if (transform.position.y > screenTop)
		{
			newPos.y = screenBottom;
		}

		if (transform.position.y < screenBottom)
		{
			newPos.y = screenTop;
		}

		if (transform.position.x > screenRight)
		{
			newPos.x = screenLeft;
		}

		if (transform.position.x < screenLeft)
		{
			newPos.x = screenRight;
		}

		transform.position = newPos;
		
	}
		
	void OnTriggerEnter2D(Collider2D other){

		if (other.CompareTag ("bullet") || other.CompareTag("beam")) {
			Destroy (other.gameObject);
		
			if (asteroidSize == 3) {
			
				//spawn 2 mediums
				Instantiate(asteroidMedium, transform.position, transform.rotation);
				Instantiate(asteroidMedium, transform.position, transform.rotation);
				asteroidDie.Play ();
				gm.updateAsteroidCount (1);
			


			} else if (asteroidSize == 2) {
			
				//spawn small
				Instantiate(asteroidSmall, transform.position, transform.rotation);
				Instantiate(asteroidSmall, transform.position, transform.rotation);
				asteroidDie.Play ();
				gm.updateAsteroidCount (1);


			} else if (asteroidSize == 1) {
			
				gm.updateAsteroidCount (-1);

				//remove the asteroid
				asteroidDie.Play ();
			}

			//give player points
			//Debug.Log(points);
			player.SendMessage("ScorePoints", points);

			GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation);

		
			Destroy (newExplosion, 3f);
			Destroy (gameObject);
		
		}

	}
}
