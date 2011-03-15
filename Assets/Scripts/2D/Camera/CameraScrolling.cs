using UnityEngine;
using System.Collections;

public class CameraScrolling : MonoBehaviour {
	
	public float scrollArea = 5.0f;
	public float scrollSpeed = 15.0f;
	public float dragSpeed = 15.0f;
	public float cameraBuffer = 10.0f;
	
	public bool scrollingEnabled = false;
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( !scrollingEnabled )
			return;
		
		// If the mouse is at the edge of the screen, move the camera over.
		float mPosX = Input.mousePosition.x;
		float mPosY = Input.mousePosition.y;		
				
		// Do camera movement by mouse position
		if( mPosX < scrollArea ) 
		{
			gameObject.transform.Translate(Vector3.right * -scrollSpeed * Time.deltaTime);			
		}
		
		if( mPosX >= Screen.width-scrollArea )
		{
			gameObject.transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime);
		}
		
		if( mPosY < scrollArea )
		{
			gameObject.transform.Translate(Vector3.up * -scrollSpeed * Time.deltaTime);
		}
		
		if( mPosY >= Screen.height-scrollArea )
		{
			gameObject.transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);
		}			
		
		/*
		// Do camera movement by keyboard
		gameObject.transform.Translate( new Vector3(Input.GetAxis("EditorHorizontal") * scrollSpeed * Time.deltaTime,
		                                       Input.GetAxis("EditorVertical") * scrollSpeed * Time.deltaTime, 0) );
		*/
		
		// Do camera movement by holding down option                 or middle mouse button and then moving mouse
		if( (Input.GetKey("left alt") || Input.GetKey("right alt")) || Input.GetMouseButton(2) ) {
		    gameObject.transform.Translate( -new Vector3(Input.GetAxis("Mouse X")*dragSpeed, Input.GetAxis("Mouse Y")*dragSpeed, 0) );
		}
		
		// Do some bounds checking.
		LevelAttributes attributes = LevelAttributes.GetInstance();
				
		Vector3 newPosition = gameObject.transform.position;
		
		// Left bound
		float leftBound = attributes.bounds.x + cameraBuffer;
		if( newPosition.x < leftBound )
		{
			newPosition.x = leftBound;
		}
		
		// Right bound
		float rightBound = attributes.bounds.x + attributes.bounds.width - cameraBuffer;
		if( newPosition.x > rightBound )
		{
			newPosition.x = rightBound;
		}
		
		// Bottom bound
		float bottomBound = attributes.bounds.y + cameraBuffer;
		if( newPosition.y < bottomBound )
		{
			newPosition.y = bottomBound;
		}
		
		// Top bound
		float topBound = attributes.bounds.y + attributes.bounds.height - cameraBuffer;
		if( newPosition.y > topBound )
		{
			newPosition.y = topBound;
		}
		
		gameObject.transform.position = newPosition;		
	}
}
