using UnityEngine;
using System.Collections;

// Small script to hold a reference to an audioclip to play when the ball hits me.
// This script is attached to game object making up your level. 
// The "Ball" script (which is attached to the player) looks for this script on whatever it touches.
public class CollisionSoundEffect : MonoBehaviour
{	
	public AudioClip audioClip;
	public float volumeModifier = 1.0f;
}

