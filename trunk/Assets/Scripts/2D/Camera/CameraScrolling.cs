using UnityEngine;
using System.Collections;

public class CameraScrolling : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		// If the mouse is at the edge of the screen, move the camera over.
		float mPosX = Input.mousePosition.x;
		float mPosY = Input.mousePosition.y;
		float scrollArea = 10.0f;
		float scrollSpeed = 10.0f;
		float dragSpeed = 10.0f;
				
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
	}
}
