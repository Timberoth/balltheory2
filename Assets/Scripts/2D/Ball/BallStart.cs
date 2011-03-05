using UnityEngine;
using System.Collections;

public class BallStart : MonoBehaviour {
			
	void OnDrawGizmos() 
	{
		Gizmos.DrawIcon(transform.position, "Player Icon.tif");		
	}
}
