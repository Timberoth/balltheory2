using UnityEngine;
using System.Collections;

public class BallFinish : MonoBehaviour {
	
	// Keep a reference to the GameManager
	GameManager gameManager;
	
	public GameObject finishObject = null;
	
	void Start()
	{
		GameObject gameManagerObject = GameObject.Find("GameManager");
		gameManager = gameManagerObject.GetComponent<GameManager>();
	}
		
	void OnCollisionEnter ( Collision other )
	{					
		if( other == null || other.gameObject == null )
		{
			return;
		}
		else
		{
			StartCoroutine(DestroyBall( other ));
		}
	}
	
	IEnumerator DestroyBall( Collision other )
    {
		if( other == null || other.gameObject == null )
			yield return new WaitForSeconds(0.0f);
		
		if( other.gameObject.tag == "Ball" )
		{					
			// Play particles
			
			// Play sounds
						
	        yield return new WaitForSeconds(0.0f);
			
			// Kill the ball after a half a second
			if( other != null && other.gameObject != null )
			{
				other.gameObject.SendMessage ("OnDeath", SendMessageOptions.DontRequireReceiver);		
				
				// Only count the object is its colliding with the proper finish object
				if( finishObject != null && other.gameObject.name.Contains(finishObject.name) )
				{
					gameManager.BallCollected( other.gameObject );	
				}				
			}
		}		
    }
}
