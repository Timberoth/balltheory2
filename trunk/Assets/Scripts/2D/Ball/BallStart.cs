using UnityEngine;
using System.Collections;

public class BallStart : MonoBehaviour {
	
	public GameObject ballObject = null;
	
	// Use this for initialization
	void Start () 
	{		
		if( ballObject != null )
		{
			// Create ball object
			Instantiate( ballObject, transform.position, Quaternion.identity );					
		}
		else
		{
			// This means that the BallStart object has not been added to the scene.
			print("ERROR Ball Object has not been set.");
		}		
	}		
	
	void OnDrawGizmos() 
	{
		Gizmos.DrawIcon(transform.position, "Player Icon.tif");		
	}
}
