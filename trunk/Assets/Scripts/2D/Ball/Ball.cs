using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider))]
[RequireComponent (typeof (Rigidbody))]

public class Ball : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		Spawn();
		
		// Start particle effects
	}
	
	/*
	// Update is called once per frame
	void Update () 
	{
		
	}
	*/
	
	// Spawn the ball at the BallStart position.
	void Spawn()
	{
		// Get a ref to the BallStart object to get it's position and velocity
		GameObject ballStart = GameObject.Find("BallStart");
		
		if( ballStart != null )
		{
			// Reset position
			transform.position = ballStart.transform.position;
			
			// Reset physics
			Rigidbody rb = GetComponent<Rigidbody>();
			Vector3 temp = new Vector3(0,0,0);
			rb.velocity = temp;					
			rb.angularVelocity = temp;
		}
		else
		{
			// This means that the BallStart object has not been added to the scene.
			print("ERROR BallStart object has not been added to this scene.");
		}
	}
	
	void OnDeath() 
	{		
		Destroy(gameObject);
	}
	
	void Respawn()
	{
		Spawn();
	}
}
