using UnityEngine;
using System.Collections;

public class RepeaterOutput : Gizmo 
{	
	// Add this number of balls to the RepeaterOutput
	public void AddBalls( int num )
	{
		ballCounter += num;
		UpdateBallCountText();
	}
	
	public override void OnCollisionEnter( Collision collision )
	{
		// Don't handle collisions for the RepeaterOutput because
		// all input is coming from the Repeater.
	}	
	
	
	void OnTriggerEnter (Collider other) 
	{
		// Place some animation to show the ball passing through the object.
	}
	
	public override void Update()
	{
		// Don't do anything because the BeginProcessing function will be called from the Repeater.
	}
	
	// Need specialized processing function.
	public override IEnumerator BeginProcessing()
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
								
		
		// Wait for a little bit
        yield return new WaitForSeconds(0.5f);
		
		
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
