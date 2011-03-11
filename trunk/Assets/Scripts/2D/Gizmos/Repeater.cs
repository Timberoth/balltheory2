using UnityEngine;
using System.Collections;

public class Repeater : Gizmo {
				
	// Keep a reference to the ModifierBox
	public ModifierBox modifierBox;	
	
	new void Start()
	{
		// Call Gizmo's start first.
		base.Start();
		
		modifierBox = GetComponentInChildren<ModifierBox>();
		if( modifierBox == null )
		{
			print("[ERROR] This Multiplier does not have an child ModifierBox.");	
		}
	}
		
	public override void DoMathematicalOperation()
	{		
		// Do nothing
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
		
		// Used the Repeater Output marker to figure out where the balls will be spit out from.
		// TODO IMPLEMENT THIS PROPERLY
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
    }	
}
