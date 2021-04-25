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

	private float testforceangle;

	public float accel = 2f;
	public float turnSpeed = 0.1f;
	public float maxSpeed = 2f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {



	}





//		thrustInput = Input.GetAxis ("power");
//
//		//Debug.Log (thrustInput);
//		turnInput = Input.GetAxis ("steering");
//
//		transform.Rotate(Vector3.forward * turnInput * Time.deltaTime * -turnThrust);
//
//
//		Vector2 newPos = transform.position;
//
//		if (transform.position.y > screenTop)
//		{
//			newPos.y = screenBottom;
//		}
//
//		if (transform.position.y < screenBottom)
//		{
//			newPos.y = screenTop;
//		}
//
//		if (transform.position.x > screenRight)
//		{
//			newPos.x = screenLeft;
//		}
//
//		if (transform.position.x < screenLeft)
//		{
//			newPos.x = screenRight;
//		}
//
//
//		transform.position = newPos;
//
//		testforceangle = Vector2.Angle (rb.velocity, transform.up);
//		Debug.Log (testforceangle);

//	}
//
	void FixedUpdate() 
	{
		//rb.AddRelativeForce (Vector2.up * thrustInput * thrust);

		//if up pressed
		if (Input.GetAxis("power") > 0)
		{
			//add force
			rb.AddRelativeForce(Vector2.up * accel);

			//if we are going too fast, cap speed
			if (rb.velocity.magnitude > maxSpeed)
			{
				rb.velocity = rb.velocity.normalized * maxSpeed +
					Vector2.Reflect (rb.velocity * -1, transform.up);
			}
		}

		//if right/left pressed add torque to turn
		if (Input.GetAxis("steering") != 0)
		{
			//scale the amount you can turn based on current velocity so slower turning below max speed
			float scale = Mathf.Lerp(0f, turnSpeed, rb.velocity.magnitude / maxSpeed);
			if (scale < 0.01f)
			{
				scale = 0.1f;
			}
			//			Debug.Log (scale);
			//axis is opposite what we want by default
			rb.AddTorque(-Input.GetAxis("steering") * scale * 0.1f );
		}



	}


}
