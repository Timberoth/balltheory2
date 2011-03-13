using UnityEngine;
using System.Collections;

public class Repeater : Gizmo {
				
	// Keep a reference to the ModifierBox
	public ModifierBox modifierBox;
	
	// Keep a reference to the RepeaterOutput object created.
	private RepeaterOutput repeaterOutput;
	
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
		
		// Pass the input object reference to the RepeaterOutput.
		//repeaterOutput.SetBallObject( ballObject );
		
		
		// Get the modifier box reference
		modifierBox = GetComponentInChildren<ModifierBox>();
		if( modifierBox == null )
		{
			print("[ERROR] This Multiplier does not have an child ModifierBox.");	
		}			
	}		
	
	public override void OnCollisionEnter( Collision collision )
	{
		// Check that the collision is with a ball.
		if( collision.gameObject.tag == "Ball" )			
		{													
			// Increment the ball counter for RepeaterOutput.
			repeaterOutput.AddBalls(1);
			
			// Destroy the ball that entered
			Destroy( collision.gameObject );
						
			// Reset the processing timer.
			processingTimer = autoProcessingTimer;
			
			hasBeenHit = true;
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
						
		// Play processing animation
		
		// Play particles
		
		// Play sounds
								
		
		// Wait for a little bit
        yield return new WaitForSeconds(0.5f);
		
		
		// Call the BeginProcessing function for the RepeaterOutput.		
		StartCoroutine(repeaterOutput.BeginProcessing());
		
				
		ballCounter = 0;
		processing = false;
		hasBeenHit = false;
    }	
}
