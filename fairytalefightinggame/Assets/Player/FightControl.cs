using UnityEngine;
using System.Collections;

public class FightControl : MonoBehaviour {

	public string AttackButton;
	public string BlockButton;
	public string SpecialButton;
	public string UltimateButton;
	public Sprite bar;
	public GameObject apple;
	private Vector3 baseScale;
	public float sheild = 100.0f;

	private GameObject barrier;

	private Animator anim;

	private float ultcharge = 0.0000001f;

	void Start() 
	{
		anim = GetComponent<Animator>();
		ultcharge = 0.0000001f;
	}
	
	// Each frame - read input, trigger animation and states
	void Update () 
	{
		if (anim.GetBool ("Block")) {
			sheild -= 0.5f;
			barrier.transform.localScale = new Vector3(sheild/150 + 0.2f, sheild/150 + 0.2f, 1f); //resize
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

	}

	void BeginAttack()
	{
		anim.SetTrigger( "Attack" );
	}

	void EndAttack()
	{
		//anim.SetTrigger("Cancel");
	}

	void BeginBlock()
	{
		if (sheild > 30.0f) {
			gameObject.GetComponent<SoundController> ().Play_Block ();
			anim.SetBool ("Block", true);
			barrier = new GameObject();
			barrier.transform.position = transform.position;
			SpriteRenderer renderer = barrier.AddComponent<SpriteRenderer>();
			renderer.sprite = bar;
			renderer.sortingLayerName = "Players"; //put on player layer
			renderer.sortingOrder = 2; // on top of player
			barrier.transform.localScale = new Vector3(sheild/150 + 0.2f, sheild/150 + 0.2f, 1f);
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
		if (anim.name == "Fairy(Clone)") { //if fairy is casting it
			//gameObject.GetComponent<Projectiles> ().Invoke ("Fire", 0.5f);
		}
	}

	void EndSpecial()
	{
		//anim.SetTrigger("Cancel");
	}

	void BeginUltimate()
	{
		if ( ultcharge >= 100.0f )
		{
			anim.SetBool ("isUlting", true);
			anim.SetTrigger( "Ultimate");
			ultcharge = 0.0f;
			apple.GetComponent<Animator>().SetFloat("Size", ultcharge);
			if (anim.name == "Fairy(Clone)"){ //if fairy is casting it
				gameObject.GetComponent<Projectiles>().Invoke("FGUlt",1f); //create polymorph projectile
			}
			else if (anim.name == "BambooCutter(Clone)"){ //if bamboocutter is casting it
				gameObject.GetComponent<Projectiles>().Invoke("BCUlt",1f); //create bamboo projectiles
			}
			else if (anim.name == "Grandma(Clone)"){ //if grandma is casting it
				gameObject.GetComponent<WolfUlt>().Invoke("BBGUlt",1f); //cast ult
			}

			Invoke("ResetUltimate", 1);
		}		
	}

	public void InitUltimate()
	{
		baseScale = apple.transform.localScale;
		apple.transform.localScale = new Vector3( 0.0f, 0.0f, 1.0f );
	}

	public void ResetUltimate()
	{
		apple.transform.localScale = new Vector3( baseScale.x * ultcharge / 100.0f, baseScale.y * ultcharge / 100.0f, 1.0f );
		apple.transform.position = new Vector3( apple.transform.position.x, Mathf.Lerp(1.13f, 1.032f, ultcharge / 100.0f), apple.transform.position.z );
	}

	public void EndUltimate()
	{
		anim.SetBool ("isUlting", false);
	}

	public void ChargeUltimate( float charge )
	{
		ultcharge += charge;
		ultcharge = Mathf.Min( ultcharge, 100.0f );
		apple.GetComponent<Animator>().SetFloat("Size", ultcharge);
		ResetUltimate();
	}
}
