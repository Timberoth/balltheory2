using UnityEngine;
using System.Collections;

// Small script to hold a reference to a prefab used for when the ball hits me.

// This script is attached to game object making up your level. 
// The "Ball" script (which is attached to the player) looks for this script on whatever it touches.
// If it finds it, then it will spawn one of these
public class CollisionParticleEffect : MonoBehaviour
{
	public GameObject effect;	
}

