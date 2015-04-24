using UnityEngine;
using System.Collections;

public class AppleScale : MonoBehaviour {

	private Vector3 baseScale;
	private Animator anim;

	void Start()
	{
		anim = GetComponent<Animator>();
		baseScale = transform.localScale;
        transform.localScale = new Vector3( 0.0f, 0.0f, baseScale.z );
	}

	public void ApplyScale()
	{
		float size = anim.GetFloat("Size");
		Debug.Log( "AppleSize" + size );
		transform.localScale = new Vector3( baseScale.x * (size / 100.0f), baseScale.y * (size / 100.0f), baseScale.z );
	}
}
