using UnityEngine;
using System.Collections;

public class FightControl : MonoBehaviour {

	public string AttackButton;
	public string BlockButton;
	public string SpecialButton;
	public string UltimateButton;

	private Animator anim;

	void Start() 
	{
		anim = GetComponent<Animator>();
	}
	
	// Each frame - read input, trigger animation and states
	void Update () 
	{
	
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
		anim.SetBool( "Attack", false );
	}

	void BeginBlock()
	{
		anim.SetBool( "Block", true );
	}

	void EndBlock()
	{
		anim.SetBool( "Block", false );
	}

	void BeginSpecial()
	{
		anim.SetBool( "Special", true );
	}

	void EndSpecial()
	{
		anim.SetBool( "Special", false );
	}

	void BeginUltimate()
	{
		anim.SetBool( "Ultimate", true );
	}

	void EndUltimate()
	{
		anim.SetBool( "Ultimate", false );
	}
}
