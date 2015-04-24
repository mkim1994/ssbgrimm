using UnityEngine;
using System.Collections;

public class AppleScale : MonoBehaviour {

	public Vector3 baseScale = new Vector3( 1.0f, 1.0f, 1.0f );
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
		transform.localScale = new Vector3( baseScale.x * (size / 100.0f), baseScale.y * (size / 100.0f), baseScale.z );
	}

	public Vector3 GetScale()
	{
		return baseScale;
	}
}
