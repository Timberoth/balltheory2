using UnityEngine;
using System.Collections;

public class BallStart : MonoBehaviour {
	
	/*
	// Use this for initialization
	void Start () 
	{
		
	}
	
	
	// Update is called once per frame
	void Update () {
	
	}
	*/	
	
	void OnDrawGizmos() 
	{
		Gizmos.DrawIcon(transform.position, "Player Icon.tif");		
	}
}
