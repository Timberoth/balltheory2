using UnityEngine;
using System.Collections;

public class CreateButtonData
{
    public float x = 0.0f;
    public float y = 0.0f;
    public string gizmoName = "";
}

// Different drop object types
public enum DropObject {Beam, Wheel, Microchip, Energy, Oil};


public class GameManager : MonoBehaviour {
				
	// Spawn Point Object Names
	private string[] spawnpointNames = { "BeamSpawn", "WheelSpawn", "MicrochipSpawn", "EnergySpawn", "OilSpawn" };
	
	// Drop Object Text Names
	private string[] dropObjectTextNames = { "Beam_Text", "Wheel_Text", "Microchip_Text", "Energy_Text", "Oil_Text" };
	
	
	// Num Required Objects Per Level
	public int[] numObjectsRequired = { 1, 0, 0, 0, 0 };
	
	// Num Objects Collected Per Level
	private int[] numObjectsCollected = { 0, 0, 0, 0, 0 };
	
	
	// Gizmos Object References
	public GameObject adderObject = null;
	public GameObject doublerObject = null;
	public GameObject halferObject = null;
	public GameObject repeaterObject = null;
	public GameObject repeaterOutputObject = null;
	public GameObject storageObject = null;
	public GameObject subtractorObject = null;	
	public GameObject slopeObject = null;
	
	// Maximum number of gizmos, 0 means no limit
	private static int maxGizmos = 1000;
	public int maxAdders = maxGizmos;
	public int maxSubtractors = maxGizmos;
	public int maxDoublers = maxGizmos;
	public int maxHalfers = maxGizmos;
	public int maxRepeaters = maxGizmos;
	public int maxStorages = maxGizmos;
	public int maxSlopes = maxGizmos;
	
	// Number of each gizmo
	private int numAdders = 0;
	private int numSubtractors = 0;
	private int numDoublers = 0;
	private int numHalfers = 0;
	private int numRepeaters = 0;
	private int numStorages = 0;
	private int numSlopes = 0;
	
	
	// Keep a reference to the current object being dragged.
	public GameObject dragObject = null;
	
	
	// Track whether the game has started
	private bool gameStarted = false;
	
	
	// Check for finish
	private bool checkForFinish = false;
	
	
	// This is the amount of time to wait before finishing the level.	
	private float FINISH_WAIT_TIME = 1.0f;
		
	// This is the actual finish timer.
	private float finishTimer = 0.0f;
		
	// Balls Collected Text
	GUIText ballsCollectedText;
	
	
	
	// Constructor
	public void BallCollected( GameObject dropObject )
	{
		Ball component = dropObject.GetComponent<Ball>();
		int type = (int)component.dropType;
		numObjectsCollected[type]++;
		
		// Update drop object text
		GameObject textObject = GameObject.Find( dropObjectTextNames[type] );
		GUIText text = textObject.GetComponent<GUIText>();
		text.text = numObjectsCollected[type].ToString()+"/"+numObjectsRequired[type].ToString();
									
		CheckForFinish();				
		
		finishTimer = FINISH_WAIT_TIME;
	}
	
	
	// Check if all the object requirements have been met.
	void CheckForFinish()
	{
		bool done = true;
		
		// If we have the exact number of required objects, wait for a little to see if the level is complete.
		for( int i = 0; i < numObjectsCollected.Length; i++ )
		{			
			if( numObjectsCollected[i] != numObjectsRequired[i] )
				done = false;			
		}
		
		if( done )
			checkForFinish = true;
		else
			checkForFinish = false;
	}
		
	
	// Use this for initialization
	void Start () 
	{	
		GUIText text;
		GameObject textObject;		
		
		if( PlayerPrefs.HasKey("levelnumber") )
		{		
			int level = PlayerPrefs.GetInt("levelnumber");
			print("Starting Level: "+level);
			
			// Level and score text
			textObject = GameObject.Find( "Level_Text" );
			text = textObject.GetComponent<GUIText>();
			text.text = "Level "+level.ToString();		
		}
		
		
		// Set up the Drop Object Text
		for( int i = 0; i < dropObjectTextNames.Length; i++ )
		{
			textObject = GameObject.Find( dropObjectTextNames[i] );
			if( textObject != null )
			{
				text = textObject.GetComponent<GUIText>();
				text.text = "0/"+numObjectsRequired[i].ToString();
			}
		}
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
		// Play particle effect
		
		// Play sound effect
		
		// Play animation
		
		yield return new WaitForSeconds(0.5f);
		
		// Go to the score screen, from there go to Next Level or Level Select.
		
		
		// HACK TEST Load up the next level.		
		string levelName = Application.loadedLevelName;
		string levelNumber = levelName.TrimStart("Level".ToCharArray());
		
		int level;
		int.TryParse(levelNumber, out level);
		level++;
		
		// Check for final level to do something different.
		
		string nextLevel = "Level"+level.ToString();
		
		PlayerPrefs.SetInt("levelnumber", level );
		
		Application.LoadLevel( nextLevel );
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
			// Stop all coroutintes			
			StopAllCoroutines();
			
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
			
			for( int i = 0; i < numObjectsCollected.Length; i++ )
				numObjectsCollected[i] = 0;
			
			
			// Reset spawn points
			foreach( string name in spawnpointNames )
			{
				ResetStartingObject( name );
			}
			
			
			gameStarted = false;
			checkForFinish = false;
			finishTimer = 0.0f;
			
			GameObject textObject;
			GUIText text;
			
			// Set up the Drop Object Text
			for( int i = 0; i < dropObjectTextNames.Length; i++ )
			{
				textObject = GameObject.Find( dropObjectTextNames[i] );
				if( textObject != null )
				{
					text = textObject.GetComponent<GUIText>();
					text.text = "0/"+numObjectsRequired[i].ToString();
				}
			}
			
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
					yield return new WaitForSeconds(0.3f);
					
					// Create the new drop object					
					GameObject dropObject = Instantiate( spawnpoint.ballObject, spawnpointObject.transform.position, Quaternion.identity ) as GameObject;
					
					// Make sure that the new Drop object has a reference to the object it is like Beam, Wheel, Microchip, etc.
					Ball ballComponent = dropObject.GetComponent<Ball>();
					ballComponent.dropObject = spawnpoint.ballObject;
					
					spawnpoint.DecrementCounter();					
				}
			}
			else
			{
				// This means that the BallStart object has not been added to the scene.
				print("[ERROR] Ball Object has not been set in the BallStart object.");
			}				
		}			
	}
	
	private void ResetStartingObject( string name )
	{
		// Grab the DropObject spawn point.
		GameObject spawnpointObject = GameObject.Find(name);
		
		if( spawnpointObject != null )
		{
			BallStart spawnpoint = spawnpointObject.GetComponent<BallStart>();
		
			if( spawnpoint != null )
			{							
				spawnpoint.ResetSpawn();
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
			if( numAdders < maxAdders )
			{
				Instantiate( adderObject, position, Quaternion.identity);
				numAdders++;
			}
			break;
			
		case "Doubler":
			if( numDoublers < maxDoublers )
			{
			Instantiate( doublerObject, position, Quaternion.identity);
			numDoublers++;
			}
			break;
			
		case "Halfer":
			if( numHalfers < maxHalfers )
			{
				Instantiate( halferObject, position, Quaternion.identity);
				numHalfers++;
			}
			break;
			
		case "Repeater":
			if( numRepeaters < maxRepeaters )
			{
				Instantiate( repeaterObject, position, Quaternion.identity);
				numRepeaters++;
			}
			break;
			
		case "Storage":
			if( numStorages < maxStorages )
			{
				Instantiate( storageObject, position, Quaternion.identity);
				numStorages++;
			}
			break;
			
		case "Subtractor":
			if( numSubtractors < maxSubtractors )
			{
				Instantiate( subtractorObject, position, Quaternion.identity);
				numSubtractors++;
			}
			break;

		case "Slope":			
			if( numSlopes < maxSlopes )
			{
				Instantiate( slopeObject, position, slopeObject.gameObject.transform.rotation );
				numSlopes++;
			}
			break;
		}
	}	
	
	public void DeleteGizmo()
	{
		// Check which gizmo is being dragged and delete it.
		if( dragObject != null )
		{
			// Don't delete any spawn points or the level could be un-winnable.
			if( !( dragObject.name.ToLower().Contains("spawn") ) )
			{				
				string clone = "(Clone)";
				string gizmoName = dragObject.name.TrimEnd(clone.ToCharArray());
				
				// Update counter
				switch( gizmoName )
				{
				case "Adder":
					numAdders = System.Math.Max( 0, --numAdders );				
					break;
					
				case "Doubler":
					numDoublers = System.Math.Max( 0, --numDoublers );
					break;
					
				case "Halfer":
					numHalfers = System.Math.Max( 0, --numHalfers );
					break;
					
				case "Repeater":
					numRepeaters = System.Math.Max( 0, --numRepeaters );								
					break;
					
				case "RepeaterOutput":
					numRepeaters = System.Math.Max( 0, --numRepeaters );
					break;
					
				case "Storage":
					numStorages = System.Math.Max( 0, --numStorages );
					break;
					
				case "Subtractor":
					numStorages = System.Math.Max( 0, --numSubtractors );
					break;
		
				case "Slope":
					numSlopes = System.Math.Max( 0, --numSlopes );
					break;
				}
			
				Destroy( dragObject );							
				dragObject = null;	
			}
		}				
	}
}
