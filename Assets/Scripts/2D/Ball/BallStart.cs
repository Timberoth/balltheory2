using UnityEngine;
using System.Collections;

public class BallStart : MonoBehaviour 
{	
	public GameObject ballObject = null;
	public DropObject type = DropObject.Wheel;
	public int startingBalls = 1;
	public int ballCounter = 1;
	
	void Start()
	{
		ballCounter = startingBalls;
		UpdateBallCountText();
	}
	
	private void UpdateBallCountText()
	{		
		// Attempt to access the ball count and update it's value		
		foreach( TextMesh textMesh in gameObject.GetComponentsInChildren<TextMesh>() )
		{
			if( textMesh.tag == "BallCountText" )
			{
				textMesh.text = ballCounter.ToString();	
			}
		}		
	}
	
	public void DecrementCounter()
	{
		ballCounter--;
		UpdateBallCountText();
	}
	
	public void ResetSpawn()
	{
		ballCounter = startingBalls;
		UpdateBallCountText();
	}
}
