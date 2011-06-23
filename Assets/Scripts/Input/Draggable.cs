using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider))]

public class Draggable : MonoBehaviour {
	
	private bool mouseHeld;
	
	// Keep track of the offset from the object's pivot to the where the mouse is clicked.
	// This will prevent the object's pivot from jumping to the mouse's position when dragging.
	private Vector2 mouseOffset;
	
	// Keep a reference to the GameManager so it can be used for tracking purposes.
	private GameManager gameManager;
	
	// Use this for initialization
	void Start ()
	{
		mouseHeld = false;
		
		GameObject gameManagerObject = GameObject.Find("GameManager");
		gameManager = gameManagerObject.GetComponent<GameManager>();
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
					
			// Do some bounds checking.
			LevelAttributes attributes = LevelAttributes.GetInstance();
			
			// Left bound
			if( worldPosition.x < attributes.bounds.x )
			{
				worldPosition.x = attributes.bounds.x;
			}
			
			// Right bound
			if( worldPosition.x > (attributes.bounds.x + attributes.bounds.width) )
			{
				worldPosition.x = attributes.bounds.x + attributes.bounds.width;
			}
			
			// Bottom bound
			if( worldPosition.y < attributes.bounds.y )
			{
				worldPosition.y = attributes.bounds.y;
			}
			
			// Top bound
			if( worldPosition.y > (attributes.bounds.y + attributes.bounds.height) )
			{
				worldPosition.y = attributes.bounds.y + attributes.bounds.height;
			}
						
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
			// Have GameManager track this object possibly being dragged.
			gameManager.dragObject = gameObject;
			
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
			
			// Use the camera to convert from screen to world space
			Camera mainCamera = UnityEngine.Camera.mainCamera;
			Vector3 worldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
			
			// Round the numbers to use Snap Positioning.				
			worldPosition.x = worldPosition.x + mouseOffset.x;
			worldPosition.y = worldPosition.y + mouseOffset.y;
			
			// Round to the nearest 0.5
			float floorX = (float)System.Math.Floor( worldPosition.x );
			float decimalX = worldPosition.x - floorX;
			if( decimalX < 0.25 )
			{
				worldPosition.x = floorX;
			}
			else if( decimalX > 0.75 )
			{
				worldPosition.x = floorX+1.0f;
			}
			else
			{
				worldPosition.x = floorX+0.5f;
			}
			
			float floorY = (float)System.Math.Floor( worldPosition.y );
			float decimalY = worldPosition.y - floorY;
			if( decimalY < 0.25 )
			{
				worldPosition.y = floorY;
			}
			else if( decimalY > 0.75 )
			{
				worldPosition.y = floorY+1.0f;
			}
			else
			{
				worldPosition.y = floorY+0.5f;
			}
			
			worldPosition.z = 0.0f;
			transform.position = worldPosition;
			
			gameManager.dragObject = null;
		}
	}		
	
	void OnMouseDrag()
	{
		
	}
}
