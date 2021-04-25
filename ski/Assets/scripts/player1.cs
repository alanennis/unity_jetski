using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player1 : MonoBehaviour {

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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		thrustInput = Input.GetAxis ("power");
		Debug.Log (thrustInput);
		turnInput = Input.GetAxis ("steering");

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



	}


}
