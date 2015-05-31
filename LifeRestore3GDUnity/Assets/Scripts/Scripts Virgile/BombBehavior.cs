using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BombBehavior : MonoBehaviour
{
	public float RangeExplosion, _KnockBack, _Fuse, _DamageValue;
	public int damagePlayer;
	private bool _ReadyToBlow;
	public bool _IsSolo;
	private Collider[] tab;

	private Sticky _MySticky;

  public GameObject explosionVisuel, Launcher;
	private GameObject myPlayerWhoDetonatedMe;
	private bool playerDetonatorAssigned = false;
	[SerializeField]
	private GameObject fuseEffect;
	private Animator myAnimator;

  private SoundManagerHeritTest _Sound;

	void Start()
	{
    _Sound = Camera.main.GetComponent<SoundManagerHeritTest>();
		myAnimator = GetComponent<Animator>();

//		if (_IsSolo)
//		{
			_MySticky = gameObject.GetComponent<Sticky>();
//		}

//		RangeExplosion = 15.0f;
//		_KnockBack = 300f;
//		_ReadyToBlow = false;
//		_Fuse = 4.0f;
//		_DamageValue = 2.0f;
	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnCollisionEnter(Collision col)
	{
		if (_IsSolo)
		{
			if (_MySticky.v_numberOfLinks > 0)
//			if (_MySticky.v_numberOfLinks > 0 || _MySticky.wasLinkedNotLongAgo == true)
			{
				if(playerDetonatorAssigned == false){
					playerDetonatorAssigned = true;
					myPlayerWhoDetonatedMe = _MySticky.myHolderPlayer;
				}
				StartCoroutine("Setup", false);
			}

			if(col.gameObject.tag == "Ennemy"){
				_Fuse = 0.75f;
				StartCoroutine("Setup", true);
			}

			if(col.gameObject.tag == "CaserneKO"
			   || col.gameObject.tag == "Fronde"
			   || col.gameObject.tag == "FrondeFriable"
			   || col.gameObject.tag == "Bombe"
			   || col.gameObject.tag == "Weapon"
			   || col.gameObject.tag == "Ragdoll"){
				StartCoroutine("Setup", false);
			}
		}

		else
		{
			StartCoroutine("Setup", false);
		}

	}

	public IEnumerator Setup(bool now)
	{
		fuseEffect.GetComponent<ParticleSystem>().Play();
		if(now == false){
			myAnimator.SetBool("Exploding", true);
		}else{
			myAnimator.SetBool("ExplodingNOW", true);
		}

		//La bombe attend X pour exploser
		yield return new WaitForSeconds(_Fuse);

		//Effet FX 
		_Sound.PlaySoundOneShot("Bombe explosion");


		//La bombe provoque une explosion
		tab = Physics.OverlapSphere(transform.position, RangeExplosion);
		foreach (Collider c in tab)
		{
			/* idole
			* joueur
			* fronde (friable)
			* interrupteur (friable)
			* decor
			* enemy petit vivant
			* enemy ingenieur (=bombe)
			* enemiKO (décor/fronde)
			* caserneKO (décor/fronde)
			* enemyVivants
			* bombe
			*/
			if (c.gameObject.tag == "Idole") DealDamages(c.gameObject, "Idole");
			else if(c.gameObject.tag == "Player") DealDamages(c.gameObject, "Player");
			else if(c.gameObject.tag == "FrondeFriable") DealDamages(c.gameObject, "FrondeFriable");
			else if(c.gameObject.tag == "WoodBlock") DealDamages(c.gameObject, "WoodBlock");
			else if(c.gameObject.tag == "Arbre") DealDamages(c.gameObject, "Arbre");
			else if(c.gameObject.tag == "UnlinkableDestructible") DealDamages(c.gameObject, "UnlinkableDestructible");
			else if(c.gameObject.tag == "Ennemy") DealDamages(c.gameObject, "Ennemy");
			else if(c.gameObject.tag == "Ragdoll") DealDamages(c.gameObject, "Ragdoll");
			else if(c.gameObject.tag == "Bombe") DealDamages(c.gameObject, "Bombe");
			else if(c.gameObject.tag == "Unlinkable") DealDamages(c.gameObject, "Unlinkable");
			else if(c.gameObject.tag == "CaserneKO") DealDamages(c.gameObject, "CaserneKO");

			Rigidbody r = c.GetComponent<Rigidbody>();

			if (r != null)
			{
				r.AddExplosionForce(_KnockBack, transform.position, RangeExplosion);
			}
		}

		if (explosionVisuel != null)
		{
			GameObject explosion = Instantiate(explosionVisuel, gameObject.transform.position, Quaternion.AngleAxis(-90, Vector3.right)) as GameObject;
		}

		//La bombe est détruite
		Destroy(gameObject);
	}

	private void DealDamages(GameObject Hit, string type)
	{
		if(Hit!=null){
			if(type == "Idole") Hit.SendMessage("TakeDamage", _DamageValue);
			else if(type == "Player"){
				if(_MySticky.myHolderPlayer != null){
					if(Hit != _MySticky.myHolderPlayer){
						Hit.GetComponent<Player_Status>().TakeDamage(new Vector3(Hit.gameObject.transform.position.x, Hit.gameObject.transform.position.y + 10f, Hit.gameObject.transform.position.z) - gameObject.transform.position, damagePlayer);
//						Hit.SendMessage("TakeDamage", new Vector3(Hit.gameObject.transform.position.x, Hit.gameObject.transform.position.y + 10f, Hit.gameObject.transform.position.z) - gameObject.transform.position);
						Hit.SendMessage("SeverLinkToIdole");
					}
				}else{
					Hit.GetComponent<Player_Status>().TakeDamage(new Vector3(Hit.gameObject.transform.position.x, Hit.gameObject.transform.position.y + 10f, Hit.gameObject.transform.position.z) - gameObject.transform.position, damagePlayer);

//					Hit.SendMessage("TakeDamage", new Vector3(Hit.gameObject.transform.position.x, Hit.gameObject.transform.position.y + 10f, Hit.gameObject.transform.position.z) - gameObject.transform.position);
					Hit.SendMessage("SeverLinkToIdole");
				}
			}
			else if(type == "FrondeFriable") Hit.SendMessage("TakeDamage", _DamageValue);
			else if(type == "WoodBlock") Hit.SendMessage("TakeDamage", _DamageValue);
			else if(type == "Arbre") Hit.SendMessage("TakeDamage", _DamageValue);
			else if(type == "UnlinkableDestructible") Hit.SendMessage("TakeDamage", _DamageValue);
			else if(type == "Ennemy") Hit.SendMessage("TakeDamage", _DamageValue);
			else if(type == "Ragdoll") Hit.SendMessage("TakeDamage", _DamageValue);
			else if(type == "Bombe"){
				Hit.GetComponent<BombBehavior>()._Fuse = 0.75f;
				Hit.SendMessage("TakeDamage", true);
			}
			else if(type == "Unlinkable") Hit.SendMessage("TakeDamage", _DamageValue);
			else if(type == "CaserneKO") Hit.SendMessage("TakeDamage", _DamageValue);
		}

//		Hit.SendMessage("TakeDamage", _DamageValue);
	}

	public void TakeDamage(bool now){
		StartCoroutine("Setup", now);
	}

	void OnDrawGizmos(){
		Gizmos.color = new Color32(255,0,0,50);
		Gizmos.DrawSphere(transform.position, RangeExplosion);
	}
}
