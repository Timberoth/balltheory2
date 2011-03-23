using UnityEngine;
using System.Collections;

public class BallFinish : MonoBehaviour {
	
	// Keep a reference to the GameManager
	GameManager gameManager;
	
	void Start()
	{
		GameObject gameManagerObject = GameObject.Find("GameManager");
		gameManager = gameManagerObject.GetComponent<GameManager>();
	}
		
	void OnCollisionEnter ( Collision other )
	{					
		StartCoroutine(DestroyBall( other ));
	}
	
	IEnumerator DestroyBall( Collision other )
    {
		if( other.gameObject.tag == "Ball" )
		{		
			// Increase ball count
			gameManager.BallCollected( other.gameObject );
			
			// Play particles
			
			// Play sounds
			
			// Wait for 1 second
	        yield return new WaitForSeconds(0.25f);
			
			// Kill the ball after a half a second
			if( other.gameObject != null )
				other.gameObject.SendMessage ("OnDeath", SendMessageOptions.DontRequireReceiver);		
		}
    }
}
