using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider))]

public class Draggable : MonoBehaviour {
	
	private bool mouseHeld;
	
	// Keep track of the offset from the object's pivot to the where the mouse is clicked.
	// This will prevent the object's pivot from jumping to the mouse's position when dragging.
	private Vector2 mouseOffset;
	
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
			worldPosition.x += mouseOffset.x;
			worldPosition.y += mouseOffset.y;
			
			// Round the numbers to use Snap Positioning.
			worldPosition.x = (float)System.Math.Round(worldPosition.x);
			worldPosition.y = (float)System.Math.Round(worldPosition.y);
			
			transform.position = worldPosition;
			
			// Move particle Effect
		}
	}
	
	void OnCollisionEnter ()
	{				
	}	
	
	void OnCollisionExit ()
	{				
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
			// Calculate mouse offset.
			Camera mainCamera = UnityEngine.Camera.mainCamera;
			Vector3 worldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
			
			mouseOffset.x = gameObject.transform.position.x - worldPosition.x;
			mouseOffset.y = gameObject.transform.position.y - worldPosition.y;
			
			mouseHeld = true;			
			
			// Start particle effect
		}
	}
	
	void OnMouseUp()
	{
		bool mouseUp = Input.GetMouseButtonUp(0);
		if( mouseUp && mouseHeld )
		{
			mouseHeld = false;			
			
			// End particle effect
		}
	}		
	
	void OnMouseDrag()
	{
		
	}
}
