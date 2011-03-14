using UnityEngine;
using System.Collections;

public class Repeater : Gizmo {
				
	// Keep a reference to the ModifierBox
	public ModifierBox modifierBox;
	
	// Keep a reference to the RepeaterOutput object created.
	private RepeaterOutput repeaterOutput;
	
	// Track the loop count to restore it after the iteration is done.
	private int loopCount = 0;
		
	
	new void Start()
	{
		// Call Gizmo's start function first.
		base.Start();
				
		// Create the Repeater output object.
		GameObject gameManagerObject = GameObject.Find("GameManager");
		GameManager gameManager = gameManagerObject.GetComponent<GameManager>();		
		if( gameManager.repeaterOutputObject == null )
		{
			print("[ERROR] A Repeater was unable to get the RepeaterOutput reference from the GameManager.");	
		}
		
		Vector3 spawnPoint = gameObject.transform.position;
		spawnPoint.y -= 1.5f;		
		// Spawn the RepeaterOutput object and place it next to the Repeater.			
		GameObject repeaterOutputObject = (GameObject)Instantiate( gameManager.repeaterOutputObject, spawnPoint, Quaternion.identity );
		
		// Grab the RepeaterOutput component.
		repeaterOutput = repeaterOutputObject.GetComponent<RepeaterOutput>();
		if( repeaterOutput == null )
		{
			print("[ERROR] A Repeater was unable to get the RepeaterOutput reference");	
		}
		
		
		// Get the modifier box reference
		modifierBox = GetComponentInChildren<ModifierBox>();
		if( modifierBox == null )
		{
			print("[ERROR] This Multiplier does not have an child ModifierBox.");	
		}			
	}			
	
	// Need specialized processing function.
	public override IEnumerator BeginProcessing()
    {				
		// If we're already processing don't do anything.
		if( processing )
		{			
			yield break;
		}
				
		// Mark that we've begun processing.
		processing = true;
						
		// If we've finished the iterations spit the ball out from the Repeater instead
		// of the RepeaterOutput.
		
		// Still more iterations left to process.
		if( modifierBox.modifier > 0 )
		{
			// Increment the ball counter for RepeaterOutput.
			repeaterOutput.AddBalls(ballCounter);
			
			// All the balls have been "transfered" to the RepeaterOutput object
			ballCounter = 0;
			UpdateBallCountText();
							
			// Play processing animation
			
			// Play particles
			
			// Play sounds
									
			
			// Wait for a little bit
	        yield return new WaitForSeconds(0.5f);
			
			
			// Call the BeginProcessing function for the RepeaterOutput.
			StartCoroutine(repeaterOutput.BeginProcessing());
			
			// Decrement the Loop Count in the ModifierBox
			modifierBox.DecrementLoopCount();
			
			// Increment our loop counter to track how many iterations have occured.
			loopCount++;
		}
		
		// No more iterations left, spit balls out from repeater.
		else
		{
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
			
			// Reset the ModifierBox value after all the iterations are done.
			// This is required to properly created nested loops.
			modifierBox.SetModifier( loopCount );
			loopCount = 0;
		}							
		
		processing = false;
		hasBeenHit = false;
    }	
}
