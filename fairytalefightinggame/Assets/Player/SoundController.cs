using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

	public AudioClip attack_Idle;
	public AudioClip attack_Crouch;
	public AudioClip attack_Jump;
	public AudioClip getHit;
	public AudioClip jump;
	public AudioClip special;
	public AudioClip ultimate;
	private AudioSource player;

	void Start(){
		player = gameObject.AddComponent<AudioSource> ();
	}
	public void  Play_Attack_Idle() {
		player.PlayOneShot (attack_Idle);
	}
	public void  Play_Attack_Crouch() {
		player.PlayOneShot (attack_Crouch);
	}
	public void  Play_Attack_Jump() {
		player.PlayOneShot (attack_Jump);
	}	
	public void  Play_GetHit() {
		player.PlayOneShot (getHit);
	}
	public void  Play_Jump() {
		player.PlayOneShot (jump);
	}
	public void  Play_Special() {
		player.PlayOneShot (special);
	}
	public void  Play_Ultimate() {
		player.PlayOneShot (ultimate);
	}

}
