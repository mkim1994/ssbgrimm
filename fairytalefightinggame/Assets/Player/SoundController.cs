using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

	public AudioClip attack_Idle;
	public AudioClip getHit;
	public AudioClip special;
	public AudioClip ultimate;
	public AudioClip block;
	public AudioClip win;
	public AudioClip enter;
	private AudioSource player;

	void Start(){
		player = gameObject.AddComponent<AudioSource> ();
	}
	public void  Play_Attack_Idle() {
		player.PlayOneShot (attack_Idle);
	}
	public void  Play_Attack_Crouch() {
		player.PlayOneShot (attack_Idle);
	}
	public void  Play_Attack_Jump() {
		player.PlayOneShot (attack_Idle);
	}	
	public void  Play_GetHit() {
		player.PlayOneShot (getHit);
	}
	public void  Play_Jump() {
		//player.PlayOneShot (jump);
	}
	public void  Play_Special() {
		player.PlayOneShot (special);
	}
	public void  Play_Ultimate() {
		player.PlayOneShot (ultimate);
	}
	public void Play_Block(){
		player.PlayOneShot (block);
	}
	public void Play_Win(){
		player.PlayOneShot (win);
	}
	public void Play_Enter(){
		player.PlayOneShot (enter);
	}

}
