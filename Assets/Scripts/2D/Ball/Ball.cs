using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider))]
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (AudioSource))]

public class Ball : MonoBehaviour {	
	
	public float baseFootAudioVolume = 1.0f;
	public float soundEffectPitchRandomness = 0.05f;
	
	// This is the class reference to the specific drop object type like Beam, Wheel, Microchip, etc.
	public GameObject dropObject = null;
	
	// This type is used to track which Drop Objects are actually being processed in each of the Gizmos.
	public DropObject dropType;
	
	void OnDeath() 
	{		
		if( gameObject != null )
			Destroy(gameObject);
	}
		
	
	void OnCollisionEnter ( Collision collision )
	{
		// TODO Different effects and sounds based on the object type.
		
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
