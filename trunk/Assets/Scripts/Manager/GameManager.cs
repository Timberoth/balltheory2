using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	// Set all the GameObject references through the GUI since Unity is stupidly set up this way.
	
	// Dropping objects
	public GameObject ballObject = null;
	
	// Gizmos
	public GameObject adderObject = null;
	public GameObject doublerObject = null;
	public GameObject halferObject = null;
	public GameObject repeaterObject = null;
	public GameObject repeaterOutputObject = null;
	public GameObject storageObject = null;
	public GameObject subtractorObject = null;	
	
	
	// Number of balls that will spawn at the BallStart position
	public int startingBalls = 1;
	
	// Number of balls to complete this level.
	public int requiredBalls = 1;

	// Number of ball collected at the ball finish.
	private int numberCollectedBalls = 0;
		
	// Track whether the game has started
	private bool gameStarted = false;
	
	private bool checkForFinish = false;
	
	// This is the amount of time to wait before finishing the level.	
	private float FINISH_WAIT_TIME = 1.0f;
	
	// This is the actual finish timer.
	private float finishTimer = 0.0f;
	
	
	// Balls Collected Text
	GUIText ballsCollectedText;
	
	public void BallCollected()
	{
		numberCollectedBalls++;
		
		// Set the required ball text.		
		ballsCollectedText.text = "Balls Collected: "+numberCollectedBalls.ToString();
		
		// If we have the exact number of balls, wait for a little to see if the level is complete.
		if( numberCollectedBalls == requiredBalls )
		{
			checkForFinish = true;			
		}
		else
		{
			checkForFinish = false;			
		}
		
		finishTimer = FINISH_WAIT_TIME;
	}
		
	// Use this for initialization
	void Start () 
	{		
		// Set the required ball text.
		GameObject textObject = GameObject.Find("Balls_Required_Text");
		GUIText text = textObject.GetComponent<GUIText>();
		text.text = "Balls Required: "+requiredBalls.ToString();
		
		GameObject textObject2 = GameObject.Find("Balls_Collected_Text");
		ballsCollectedText = textObject2.GetComponent<GUIText>();
		ballsCollectedText.text = "Balls Collected: "+numberCollectedBalls.ToString();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( checkForFinish )
		{
			if( finishTimer > 0.0f )
			{
				finishTimer -= Time.deltaTime;
			}
			else 
			{
				checkForFinish = false;
				finishTimer = 0.0f;
				StartCoroutine(FinishLevel());
			}
		}
	}
	
	
	private IEnumerator FinishLevel()
	{
		print("Finish Level");
		
		// Play particle effect
		
		// Play sound effect
		
		// Play animation
		
		yield return new WaitForSeconds(0.5f);
		
		// Go to the score screen, from there go to Next Level or Level Select.
		
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
			
			numberCollectedBalls = 0;
			gameStarted = false;
			checkForFinish = false;
			finishTimer = 0.0f;
			
			ballsCollectedText.text = "Balls Collected: "+numberCollectedBalls.ToString();
			
			StartLevel();
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
	
	
	public void CreateGizmo( string gizmo )
	{
		// Need to figure out how to place the gizmo on the screen.
		Vector3 position = new Vector3(10.0f, 10.0f, 0.0f);
		
		switch( gizmo )
		{
		case "Adder":
			Instantiate( adderObject, position, Quaternion.identity);
			break;
			
		case "Doubler":
			Instantiate( doublerObject, position, Quaternion.identity);
			break;
			
		case "Halfer":
			Instantiate( halferObject, position, Quaternion.identity);
			break;
			
		case "Repeater":
			Instantiate( repeaterObject, position, Quaternion.identity);
			break;
			
		case "Storage":
			Instantiate( storageObject, position, Quaternion.identity);
			break;
			
		case "Subtractor":
			Instantiate( subtractorObject, position, Quaternion.identity);
			break;
			
		}
	}	
}
