using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider))]

public class SpringBoard : MonoBehaviour
{
	public float jumpingPower = 1250.0f;
	
	private float undersidePower = 300.0f;
	
	/*
	// Use this for initialization
	void Start ()
	{
		
	}

	// Update is called once per frame
	void Update ()
	{
		
	}
	*/
	
	// Add an upward force to the object to cause it to jump
	void OnCollisionEnter ( Collision collision )
	{								
		// Make sure the object is tagged as a Ball
		if( collision.gameObject.tag == "Ball" )			
		{														
			// Check for RB and check that the point of contact is above the platform 
			// to prevent collisions underneath.
			Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
			if( rb != null )
			{
				Vector3 force = new Vector3(0.0f, 0.0f, 0.0f);
				if( collision.contacts[0].point.y >= (gameObject.transform.position.y-0.1) )
				{					
					// Make the ball jump perpendicular to the platform.
					force = gameObject.transform.up;
					force.y *= jumpingPower;
					
					//print("Jump up");
				}
				else
				{				
					// Give it a little push in the direction it's heading
					Vector3 ballVelocity = rb.velocity;
					if( ballVelocity.x > 0 )
					{
						force.x = undersidePower;
					}
					else
					{
						force.x = -undersidePower;
					}					
					//print("Bottom hit");					
				}
				
				// Add the force to the ball.
				rb.AddForce( force );
			}		
		}
	}
}

