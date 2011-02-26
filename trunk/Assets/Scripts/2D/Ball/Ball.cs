using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider))]
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (AudioSource))]

public class Ball : MonoBehaviour {
	
	public float baseFootAudioVolume = 1.0f;
	public float soundEffectPitchRandomness = 0.05f;
	
	void OnDeath() 
	{		
		if( gameObject != null )
			Destroy(gameObject);
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
