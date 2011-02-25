using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider))]
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (AudioSource))]

public class Ball : MonoBehaviour {
	
	public float baseFootAudioVolume = 1.0f;
	public float soundEffectPitchRandomness = 0.05f;
	

	// Use this for initialization
	void Start () 
	{
		Spawn();
		
		// Start particle effects
	}
	
	/*
	// Update is called once per frame
	void Update () 
	{
		
	}
	*/
	
	// Spawn the ball at the BallStart position.
	void Spawn()
	{
		// Get a ref to the BallStart object to get it's position and velocity
		GameObject ballStart = GameObject.Find("BallStart");
		
		if( ballStart != null )
		{
			// Reset position
			transform.position = ballStart.transform.position;
			
			// Reset physics
			Rigidbody rb = GetComponent<Rigidbody>();
			Vector3 temp = new Vector3(0,0,0);
			rb.velocity = temp;					
			rb.angularVelocity = temp;
		}
		else
		{
			// This means that the BallStart object has not been added to the scene.
			print("ERROR BallStart object has not been added to this scene.");
		}
	}
	
	void OnDeath() 
	{		
		Destroy(gameObject);
	}
	
	void Respawn()
	{
		Spawn();
	}
	
	void OnCollisionEnter ( Collision collision )
	{
		CollisionParticleEffect collisionParticleEffect = collision.gameObject.GetComponent<CollisionParticleEffect>();
	
		if (collisionParticleEffect) {
			Instantiate(collisionParticleEffect.effect, transform.position, transform.rotation);
		}
		
		CollisionSoundEffect collisionSoundEffect = collision.gameObject.GetComponent<CollisionSoundEffect>();
	
		if (collisionSoundEffect) {
			audio.clip = collisionSoundEffect.audioClip;
			audio.volume = collisionSoundEffect.volumeModifier * baseFootAudioVolume;
			audio.pitch = Random.Range(1.0f - soundEffectPitchRandomness, 1.0f + soundEffectPitchRandomness);
			audio.Play();		
		}
	}
}
