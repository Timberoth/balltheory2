using UnityEngine;
using System.Collections;

public class BallFinish : MonoBehaviour {
	/*
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	*/
		
	void OnCollisionEnter ( Collision other )
	{			
		// End the level
		StartCoroutine(EndLevel( other ));		
	}
	
	IEnumerator EndLevel( Collision other )
    {
		// Calculate the score
		
		// Play particles
		
		// Play sounds
		
		// Wait for 1 second
        yield return new WaitForSeconds(0.5f);
		
		// Kill the ball after a half a second
		if( other.gameObject != null )
			other.gameObject.SendMessage ("OnDeath", SendMessageOptions.DontRequireReceiver);		
    }
}
