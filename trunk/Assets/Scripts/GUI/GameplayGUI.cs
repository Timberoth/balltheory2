using UnityEngine;
using System.Collections;

public class GameplayGUI : MonoBehaviour {

	public Texture2D icon;
	
	// Create a reference to the GameManager
	private GameManager gameManager;
	
	void Start()
	{
		GameObject gameManagerObject = GameObject.Find("GameManager");
		gameManager = gameManagerObject.GetComponent<GameManager>();
		if( gameManager == null )
		{
			print("ERROR - Unable to get the GameManager reference in GameplayGUI::Start().");	
		}
	}

	void OnGUI () 
	{		
				
		//GUILayout.BeginArea (new Rect (Screen.width/2, Screen.height/2, 300, 300));
		GUILayout.BeginArea( new Rect(10.0f, 10.0f, 100.0f, 300.0f) );
		
		if( GUILayout.Button ("Start Level") )
		{
			gameManager.StartLevel();
		}
		
		if( GUILayout.Button ("Restart Level") )
		{
			gameManager.RestartLevel();
		}
				
		GUILayout.EndArea ();
	}
}
