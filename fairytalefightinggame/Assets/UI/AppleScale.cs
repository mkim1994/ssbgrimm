using UnityEngine;
using System.Collections;

public class AppleScale : MonoBehaviour {

	private Animator anim;

	void Start()
	{
	}

	public void Zero()
	{
		transform.localScale = new Vector3( transform.localScale.x * 0.0f, 0.0f, 1.0f );
	}
}
