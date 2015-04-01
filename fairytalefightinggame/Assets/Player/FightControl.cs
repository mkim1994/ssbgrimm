using UnityEngine;
using System.Collections;

public class FightControl : MonoBehaviour {

	public string AttackButton;
	public string BlockButton;
	public string SpecialButton;
	public string UltimateButton;
	
	// Each frame - read input, trigger animation and states
	void Update () {
	
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

	void BeginAttack(){}

	void EndAttack(){}

	void BeginBlock(){}

	void EndBlock(){}

	void BeginSpecial(){}

	void EndSpecial(){}

	void BeginUltimate(){}

	void EndUltimate(){}
}
