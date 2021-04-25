using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienScript : MonoBehaviour {

	public Rigidbody2D rb;
	public Vector2 direction;
	public float speed;
	public Transform player;
	public GameObject bullet;
	public float bulletSpeed;
	public float shootingDelay; //time between shots in seconds
	public float lastTimeShot = 0f;

	public GameObject explosion;
	public SpriteRenderer sr;
	public Collider2D alienCollider;
	public bool disabled; //true when disabled
	public int points;
	public float timeBeforeSpawn;
	//float levelStartTIme;
	public Transform startPos;
	public int currentLevel = 0;








	// Use this for initialization
	void Start () {

		player = GameObject.FindWithTag ("Player").transform;
		//levelStartTIme = Time.time;
		//timeBeforeSpawn = Random.Range (5f, 20f); 
		//Invoke ("enableAlien", timeBeforeSpawn);
		newLevel();
		//disableAlien ();

	}
	
	// Update is called once per frame
	void Update () {

		if (disabled)
		{
			
			return;
		}

		if (Time.time > lastTimeShot + shootingDelay){


			float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg - 90f;
			Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);

			GameObject newBullet = Instantiate (bullet, transform.position, q);
			newBullet.GetComponent<Rigidbody2D> ().AddRelativeForce (new Vector2 (0f, bulletSpeed));
			lastTimeShot = Time.time;



		}

	}


	void FixedUpdate(){

		if (disabled)
		{
			return;
		}

		direction = (player.position - transform.position).normalized;
		rb.MovePosition (rb.position + direction * speed * Time.fixedDeltaTime);



	}

	public void newLevel()
	{
		currentLevel ++;
		disableAlien();
		timeBeforeSpawn = Random.Range (5f, 20f);
		Invoke ("enableAlien", timeBeforeSpawn);
		speed = currentLevel * 2;
		bulletSpeed = currentLevel * 200;
	}



	void enableAlien()
	{

		//move to start pos
		transform.position = startPos.position;
		 

		//enable collider and sprite
		sr.enabled = true;
		alienCollider.enabled = true;
		disabled = false;

	}

	public void disableAlien()
	{	
		

		//turn off collider and sprite
		sr.enabled = false;
		alienCollider.enabled = false;
		disabled = true;


	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("bullet"))
		{
			//destroy alien
			//explosion

			//killShip ();
			GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation);
			Destroy (newExplosion, 3f);
			player.SendMessage("ScorePoints", points * currentLevel);
			disableAlien ();

		}

	}

	void killShip()
	{
		return;
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.transform.CompareTag("Player"))
		{
			//killShip ();
			GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation);
			Destroy (newExplosion, 3f);
			player.SendMessage("ScorePoints", points);
			disableAlien ();

		}

	}
}

