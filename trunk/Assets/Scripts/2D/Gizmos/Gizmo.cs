using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider))]

public class Gizmo : MonoBehaviour {
			
	// Privates
	
	// Keep a reference to the ball objects that will be used as input and output
	protected GameObject ballObject;
	
	// Keep track of how many balls are in this gizmo
	protected int ballCounter = 0;	
	
	// Keep track of when the actual gizmo processing is taking place
	protected bool processing = false;
	
	// Keep track of where the ball output will come out from
	protected Vector3 ballSpawnPoint;
		
	// Keep track of ball size
	protected Vector3 ballSize;
	
	// Keep track of gizmo size
	protected Vector3 gizmoSize;
	
	// This is the amount of time to wait before automatically starting the BeginProcessing function.
	// This can be set through the Editor per Gizmo.
	protected float autoProcessingTimer = 1.0f;
	
	// This is the actual timer.
	protected float processingTimer = 0.0f;
	
	// Track whether this Gizmo has been hit by a ball object and should begin counting
	// down it processingTimer.
	protected bool hasBeenHit = false;
	
	// Use this for initialization
	protected void Start () 
	{		
		// Grab the ball game object ref from the "BallStart" component
		GameObject gameManagerObject = GameObject.Find("GameManager");
		GameManager gameManager = gameManagerObject.GetComponent<GameManager>();
		ballObject = gameManager.ballObject;
		
		if( ballObject == null )
		{
			print("[ERROR] A gizmo was unable to get the BallObject reference");	
		}
		
		// Do some one time calculations
		
		// Get ball size
		Collider ballCollider = ballObject.GetComponent<Collider>();
		ballSize = ballCollider.bounds.size;
			
		// Get gizmo size
		Collider gizmoCollider = gameObject.GetComponent<Collider>();
		gizmoSize = gizmoCollider.bounds.size;
					
		// Play the idle animation.	
		
		UpdateBallCountText();
	}
	
	
	public void ResetGizmo()
	{
		ballCounter = 0;
		processing = false;
		processingTimer = 0.0f;		
		hasBeenHit = false;
		
		// Attempt to access the ball count and update it's value		
		foreach( TextMesh textMesh in gameObject.GetComponentsInChildren<TextMesh>() )
		{
			if( textMesh.tag == "BallCountText" )
			{
				textMesh.text = "0";	
			}
		}		
	}
	
	
	public virtual void OnCollisionEnter( Collision collision )
	{
		// Check that the collision is with a ball.
		if( collision.gameObject.tag == "Ball" )			
		{										
			// Keep an ball object game ref so it can be spawned later.
			// This could break down if we start handling multiple ball types.
			//if( ballObject == null )
			//	ballObject = collision.gameObject;
			
			// Increment the ball counter
			ballCounter++;
			
			// Destroy the ball that entered
			Destroy( collision.gameObject );
			
			UpdateBallCountText();
			
			// Reset the processing timer.
			processingTimer = autoProcessingTimer;
			
			hasBeenHit = true;
		}		
	}	
		
	public virtual void Update()
	{
		if( hasBeenHit )
		{
			if( processingTimer > 0.0f )
			{
				processingTimer -= Time.deltaTime;
			}
			else 
			{
				processingTimer = 0.0f;	
				
				// Kick off the processing if it hasn't already been started.
				if( !processing )
				{
					StartCoroutine(BeginProcessing());
				}				
			}
		}
	}
	
	protected void UpdateBallCountText()
	{		
		// Attempt to access the ball count and update it's value		
		foreach( TextMesh textMesh in gameObject.GetComponentsInChildren<TextMesh>() )
		{
			if( textMesh.tag == "BallCountText" )
			{
				textMesh.text = ballCounter.ToString();	
			}
		}		
	}
	
	
	// Each Gizmo has to define this function.
	public virtual void DoMathematicalOperation()
	{		
	}
		
	
	public virtual IEnumerator BeginProcessing()
    {		
		// If we're already processing don't do anything.
		if( processing )
		{			
			yield break;
		}
		
		// Don't do anything if the gizmo is empty
		if( ballCounter == 0 )
		{
			// Play failure sound effect
			
			// Play failure particle effect
			
			// Play failure animation
			
			yield break;
		}
						
		// Mark that we've begun processing.
		processing = true;		
						
		// Play processing animation
		
		// Play particles
		
		// Play sounds
		
		
		// Do mathematical operation
		DoMathematicalOperation();		
		UpdateBallCountText();		
		
		
		// Wait for a little bit
        yield return new WaitForSeconds(0.5f);
		
		// Calculate where the balls will be spit out from		
		ballSpawnPoint = gameObject.transform.position;					
		ballSpawnPoint.y = ballSpawnPoint.y - 0.5f*ballSize.y - 0.5f*gizmoSize.y - 0.1f;			
						
		int ballsAtStart = ballCounter;
		
		// Begin spitting out the balls and decrement the ball counter until all the balls are gone.
		for( int i = 0; i < ballsAtStart; i++ )
		{				
			// Instantiate ball at the bottom of the gizmo			
			Instantiate( ballObject, ballSpawnPoint, Quaternion.identity );
			
			ballCounter--;
			
			UpdateBallCountText();
			
			// Play sound
			
			// Play particle
			
			// Wait for little bit before spitting out the next ball.
			yield return new WaitForSeconds(0.5f);
		}
				
		ballCounter = 0;
		processing = false;
		hasBeenHit = false;
    }	
}
