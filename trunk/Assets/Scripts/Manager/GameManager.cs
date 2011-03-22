using UnityEngine;
using System.Collections;

public class CreateButtonData
{
    public float x = 0.0f;
    public float y = 0.0f;
    public string gizmoName = "";
}

// Different drop object types
public enum DropObject {Wheel, Beam, Microchip, Energy, Oil};


public class GameManager : MonoBehaviour {
			
	// Set all the GameObject references through the GUI since Unity is stupidly set up this way.
	
	private string[] spawnpointNames = {"WheelSpawn", "BeamSpawn", "MicrochipSpawn", "EnergySpawn", "OilSpawn" };
	
	// Gizmos
	public GameObject adderObject = null;
	public GameObject doublerObject = null;
	public GameObject halferObject = null;
	public GameObject repeaterObject = null;
	public GameObject repeaterOutputObject = null;
	public GameObject storageObject = null;
	public GameObject subtractorObject = null;	
	
	
	// Keep a reference to the current object being dragged.
	public GameObject dragObject = null;
		
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
	
	
	// Constructor
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
		
		
		// Load up the next level.		
	}
	
	
	public void StartLevel()
	{
		if( !gameStarted )
		{
			gameStarted = true;
			// Go through all the possible drop object spawn points and begin spawning.
			foreach( string name in spawnpointNames )
			{
				StartCoroutine( SpawnStartingObjects( name ) );
			}
		}
		
		// If the level has already been started, restart it.
		else
		{
			RestartLevel();
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
	
	private IEnumerator SpawnStartingObjects( string name )
    {			
		// Grab the DropObject spawn point.
		GameObject spawnpointObject = GameObject.Find(name);
		
		if( spawnpointObject != null )
		{
			BallStart spawnpoint = spawnpointObject.GetComponent<BallStart>();
		
			if( spawnpoint.ballObject != null )
			{							
				// Create all the starting ball objects
				for( int i = 0; i < spawnpoint.startingBalls; i++ )
				{
					// Wait for a little before ball spawns
					yield return new WaitForSeconds(0.5f);
					
					// Create the new drop object					
					GameObject dropObject = Instantiate( spawnpoint.ballObject, spawnpointObject.transform.position, Quaternion.identity ) as GameObject;
					
					// Make sure that the new Drop object has a reference to the object it is like Beam, Wheel, Microchip, etc.
					Ball ballComponent = dropObject.GetComponent<Ball>();
					ballComponent.dropObject = spawnpoint.ballObject;
				}
			}
			else
			{
				// This means that the BallStart object has not been added to the scene.
				print("[ERROR] Ball Object has not been set in the BallStart object.");
			}				
		}			
	}
	
	
	public void CreateGizmo( CreateButtonData data )
	{
		// Need to figure out how to place the gizmo on the screen.
		data.x = (float)System.Math.Round(data.x);
		data.y = (float)System.Math.Round(data.y);
		Vector3 position = new Vector3( data.x, data.y, 0.0f);
		
		switch( data.gizmoName )
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
	
	public void DeleteGizmo()
	{
		// Check which gizmo is being dragged and delete it.
		if( dragObject != null )
		{
			print("Delete Gizmo: "+dragObject);
			Destroy( dragObject );
			
			dragObject = null;
		}				
	}
}
