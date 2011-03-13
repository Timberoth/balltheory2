using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	// Set all the GameObject references through the GUI since Unity is stupidly set up this way.
	public GameObject ballObject = null;
	public GameObject repeaterOutputObject = null;
	
	// Number of balls that will spawn at the BallStart position
	public int startingBalls = 1;
	
	// Number of balls to complete this level.
	public int requiredBalls = 1;

	// Number of ball collected at the ball finish.
	private int numberCollectedBalls = 0;
		
	// Track whether the game has started
	private bool gameStarted = false;
	
	public void BallCollected()
	{
		numberCollectedBalls++;
	}
		
	// Use this for initialization
	void Start () 
	{		
			
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public void StartLevel()
	{
		if( !gameStarted )
		{
			gameStarted = true;
			StartCoroutine(SpawnStartingBalls());	
		}
	}
	
	public void RestartLevel()
	{		
		if( gameStarted )
		{
			// Destroy all the existing balls
			GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
			foreach( GameObject ball in balls )
			{
				Destroy( ball );			
			}
			
			// Reset all the Gizmos
			GameObject[] gizmos = GameObject.FindGameObjectsWithTag("Gizmo");			
			foreach( GameObject gizmo in gizmos )
			{				
				Gizmo gizmoComponent = gizmo.GetComponent<Gizmo>();
				if( gizmoComponent != null )
				{
					gizmoComponent.ResetGizmo();
				}
			}
			
			
			gameStarted = false;
		}
	}
	
	private IEnumerator SpawnStartingBalls()
    {	
		// Grab the BallStart object.
		GameObject ballStart = GameObject.Find("BallStart");		
		
		if( ballObject != null )
		{
			// Create all the starting ball objects
			for( int i = 0; i < startingBalls; i++ )
			{
				// Wait for a little before ball spawns
				yield return new WaitForSeconds(0.5f);
				
				Instantiate( ballObject, ballStart.transform.position, Quaternion.identity );
			}
		}
		else
		{
			// This means that the BallStart object has not been added to the scene.
			print("[ERROR] Ball Object has not been set in the BallStart object.");
		}			
	}
}
