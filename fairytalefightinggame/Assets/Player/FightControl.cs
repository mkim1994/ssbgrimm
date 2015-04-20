using UnityEngine;
using System.Collections;

public class FightControl : MonoBehaviour {

	public string AttackButton;
	public string BlockButton;
	public string SpecialButton;
	public string UltimateButton;
	public Sprite bar;
	public Animator apple;
	public float sheild = 100.0f;

	private GameObject barrier;

	private Animator anim;

	private float ultcharge = 0.0f;

	void Start() 
	{
		anim = GetComponent<Animator>();

		ultcharge = 0.0f;
	}
	
	// Each frame - read input, trigger animation and states
	void Update () 
	{
		if (anim.GetBool ("Block")) {
			sheild -= 0.5f;
			barrier.transform.localScale = new Vector3(sheild/200 + 0.2f, sheild/200 + 0.2f, 1f); //resize
			barrier.transform.position = transform.position;
		}
		else if (sheild < 100) {
			sheild += 0.2f; //regen sheild
		}
		if (sheild < 1.0f){ //outa sheild
			anim.SetBool("Block",false);
			Destroy (barrier);
		}

		if ( Input.GetButtonDown( AttackButton ) )
		{
			BeginAttack();
		}
		else if ( Input.GetButtonUp( AttackButton ) )
		{
			EndAttack();
		}
		else if ( Input.GetButtonDown( BlockButton ) )
		{
			BeginBlock();
		}
		else if ( Input.GetButtonUp( BlockButton ) )
		{
			EndBlock();
		}
		else if ( Input.GetButtonDown( SpecialButton ) )
		{
			BeginSpecial();
		}
		else if ( Input.GetButtonUp( SpecialButton ) )
		{
			EndSpecial();
		}
		else if ( Input.GetButtonDown( UltimateButton ) )
		{
			BeginUltimate();
		}
		else if ( Input.GetButtonUp( UltimateButton ) )
		{
			EndUltimate();
		}

	}

	void BeginAttack()
	{
		anim.SetTrigger( "Attack" );
	}

	void EndAttack()
	{
		anim.SetTrigger("Cancel");
	}

	void BeginBlock()
	{
		if (sheild > 30.0f) {
			anim.SetBool ("Block", true);
			barrier = new GameObject();
			barrier.transform.position = transform.position;
			SpriteRenderer renderer = barrier.AddComponent<SpriteRenderer>();
			renderer.sprite = bar;
			renderer.sortingLayerName = "Players"; //put on player layer
			renderer.sortingOrder = 2; // on top of player
			barrier.transform.localScale = new Vector3(sheild/200 + 0.2f, sheild/200 + 0.2f, 1f);
		}
	}

	void EndBlock()
	{
		anim.SetBool( "Block", false );
		Destroy (barrier);
	}

	void BeginSpecial()
	{
		anim.SetTrigger("Special");
	}

	void EndSpecial()
	{
		anim.SetTrigger("Cancel");
	}

	void BeginUltimate()
	{
		if ( ultcharge >= 100.0f )
		{
			anim.SetTrigger( "Ultimate");
			ultcharge = 0.0f;
		}		
	}

	void EndUltimate()
	{
	}

	public void ChargeUltimate( float charge )
	{
		ultcharge += charge;
		ultcharge = Mathf.Min( ultcharge, 100.0f );
		apple.SetFloat("Size", ultcharge ;
	}
}
