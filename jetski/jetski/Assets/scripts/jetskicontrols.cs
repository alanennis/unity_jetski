using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class jetskicontrols : MonoBehaviour {

	public Rigidbody2D rb;
	public SpriteRenderer sr;
	public Collider2D collide;

	public float thrust;
	public float turnThrust;
	private float thrustInput;
	private float turnInput;

	public float screenTop;
	public float screenBottom;
	public float screenLeft;
	public float screenRight;

	public GameObject bullet;

	public float bulletForce;

	public float deathForce;

	public int score;

	public int lives;

	public Text scoreTxt;
	public Text livesTxt;
	public AudioSource audioShipBump;

	public GameObject deathExplosion;

	public Color inColour;
	public Color normalColour;



	public GameObject GameOverPanel;
	public GameObject hiscorepanel;
	public InputField hiScoreInputfld;
	public Text hiScoreListTxt;
	 


	private bool hyperSpace; //true means currently h-spacing

	public AlienScript alien;

	public GameManager gm;








	// Use this for initialization
	void Start () 
	{

		score = 0;
		//lives = 2;
		scoreTxt.text = "Score: " + score;
		livesTxt.text = "Lives: " + lives;
		hyperSpace = false;


			
	}
	
	// Update is called once per frame
	void Update () 
	{

		thrustInput = Input.GetAxis ("Vertical");
		turnInput = Input.GetAxis ("Horizontal");

		//check for bullet
		if (Input.GetButtonDown("Fire1")) {

			GameObject newBullet = Instantiate (bullet, transform.position, transform.rotation); 
			newBullet.GetComponent<Rigidbody2D> ().AddRelativeForce (Vector2.up * bulletForce);
			Destroy (newBullet, 3.0f);

		}

		if(Input.GetButtonDown("Hyperspace") && hyperSpace == false)
		{
			hyperSpace = true;
			sr.enabled = false;

			//GetComponent<Collider2D>().enabled = false;
			collide.enabled = false;
			Invoke("Hyperspace", 1f);



		}

		//rotate ship
		transform.Rotate(Vector3.forward * turnInput * Time.deltaTime * -turnThrust);


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

	void FixedUpdate() 
	{
		rb.AddRelativeForce (Vector2.up * thrustInput * thrust);
		//rb.AddTorque (-turnInput * turnThrust);


	}


	void ScorePoints(int pointsToAdd){


		score += pointsToAdd;
		//Debug.Log (score);


		scoreTxt.text = "Score: " + score;

	}


	void Respawn()
	{
		rb.velocity = Vector2.zero;
		transform.position = Vector2.zero;

		//SpriteRenderer sr = GetComponent<SpriteRenderer> ();
		sr.enabled = true;
		sr.color = inColour;
		Invoke ("Invulnerable", 3f);

	}

	void Invulnerable()
	{
		//SpriteRenderer sr = GetComponent<SpriteRenderer> ();
		sr.color = normalColour;
		//GetComponent<Collider2D> ().enabled = true;
		collide.enabled = true;

	}

	void Hyperspace(){
		
		Vector2 newPosition = new Vector2(Random.Range(-30f, 30f), Random.Range(-20f, 20f));
		transform.position = newPosition;
		sr.enabled = true;
		collide.enabled = true;
		hyperSpace = false;
	}

	void loseLife()
	{
		audioShipBump.Play ();

		GameObject yourDead = Instantiate(deathExplosion, transform.position, transform.rotation);
		Destroy (yourDead, 3f);
		lives--;
		livesTxt.text = "Lives: " + lives;
		//Debug.Log (lives);

		//respawn ship
		//GetComponent<SpriteRenderer>().enabled = false;
		sr.enabled = false;

		//GetComponent<Collider2D>().enabled = false;
		collide.enabled = false;

		Invoke ("Respawn", 3f);

		if (lives <= 0) {

			//Game over
			GameOver();

		}

	}


	void OnCollisionEnter2D(Collision2D col)
	{

		//Debug.Log ("hit");
		//Debug.Log (col.relativeVelocity.magnitude);
		if (col.relativeVelocity.magnitude > deathForce){

			//Debug.Log ("you died");

			loseLife();
			alien.disableAlien ();


		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		//Debug.Log ("beam hit you");
		if (other.CompareTag ("beam"))
		{
			//Debug.Log ("beam hit you");
			loseLife ();
		}

	}


	void GameOver()
	{

		CancelInvoke ();

		//tell gamemanager to check for high score
		if (gm.checkhiscore(score))
		{
			//Debug.Log ("hi score requested");
			hiscorepanel.SetActive (true);

		}
		else
		{
			//Debug.Log ("not a new hiscore");
			GameOverPanel.SetActive (true);
			hiScoreListTxt.text = "HIGH SCORE" + "\n" + "\n" + 
				PlayerPrefs.GetString ("hiScoreName") + " " + PlayerPrefs.GetInt ("hiscore");
		}


	}

	public void hiScoreInput()
	{
		string newInput = hiScoreInputfld.text;
		PlayerPrefs.SetString ("hiScoreName", newInput);
		PlayerPrefs.SetInt ("hiscore", score);
		Debug.Log ("new hiscore name " + newInput);
		hiscorepanel.SetActive (false);
		GameOverPanel.SetActive (true);
		hiScoreListTxt.text = "HIGH SCORE" + "\n" + "\n" + 
			PlayerPrefs.GetString ("hiScoreName") + " " + PlayerPrefs.GetInt ("hiscore");
	}

	public void PlayAgain()
	{

		SceneManager.LoadScene ("SampleScene");

	}
	public void backToMenu()
	{
		SceneManager.LoadScene ("startscene"); 
	}
}
