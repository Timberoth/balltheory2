using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider))]

public class Draggable : MonoBehaviour {
	
	private bool mouseHeld;
	
	// Use this for initialization
	void Start ()
	{
		mouseHeld = false;	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if( mouseHeld )
		{					
			// Use the camera to convert from screen to world space
			Camera mainCamera = UnityEngine.Camera.mainCamera;
			Vector3 worldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
			
			// Make sure to keep everything in 2D
			worldPosition.z = 0.0f;
			transform.position = worldPosition;
			
			// Move particle Effect
		}
	}
	
	void OnCollisionEnter ()
	{		
		print("CollisionEnter");			
	}	
	
	void OnCollisionExit ()
	{		
		print("CollisionExit");			
	}		

	void Reset() 
	{		
		mouseHeld = false;
	}
	
	void OnMouseDown()
	{		
		// Begin moving the object around
		bool mouseDown = Input.GetMouseButtonDown(0);
		if( mouseDown && !mouseHeld )
		{
			mouseHeld = true;
			print("On mouse down");
			
			// Start particle effect
		}
	}
	
	void OnMouseUp()
	{
		bool mouseUp = Input.GetMouseButtonUp(0);
		if( mouseUp && mouseHeld )
		{
			mouseHeld = false;
			print("On mouse up");
			
			// End particle effect
		}
	}		
}
