using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BombBehavior : MonoBehaviour
{
  public float RangeExplosion, _KnockBack, _Fuse, _DamageValue;
  private bool _ReadyToBlow;
  public bool _IsSolo;
  private Collider[] tab;

  private Sticky _MySticky;

  public GameObject explosionVisuel, Launcher;

  void Start()
  {

    if (_IsSolo)
    {
      _MySticky = gameObject.GetComponent<Sticky>();
    }

    RangeExplosion = 15.0f;
    _KnockBack = 300f;
    _ReadyToBlow = false;

    _DamageValue = 2.0f;
    _Fuse = 4.0f;
  }

  // Update is called once per frame
  void Update()
  {
    if (Launcher == null && transform.parent == null)
    {
      Destroy(gameObject);
    }
  }

  void OnCollisionEnter(Collision col)
  {
    if (_IsSolo)
    {
      if (_MySticky.v_numberOfLinks > 0)
      {
        StartCoroutine("Setup");
      }
      if(col.gameObject.tag == "Ennemy"){
				_Fuse = 2.0f;
        StartCoroutine("Setup");
      }
    }
    else
    {
      StartCoroutine("Setup");
    }

  }

  public IEnumerator Setup()
  {
    //La bombe attend X pour exploser
    yield return new WaitForSeconds(_Fuse);
    
    //Effet FX 

    //La bombe provoque une explosion
    tab = Physics.OverlapSphere(transform.position, RangeExplosion);
    foreach (Collider c in tab)
    {
			if (c.gameObject.tag == "Player" || c.gameObject.tag == "Idole" || c.gameObject.tag == "Arbre" || c.gameObject.tag == "UnlinkableDestructible"||c.gameObject.tag == "Ennemy")
      {
        DealDamages(c.gameObject);
      }

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

  private void DealDamages(GameObject Hit)
  {
    Hit.SendMessage("TakeDamage", _DamageValue);
  }

  public void TakeDamage(){
    StartCoroutine("Setup");
  }
}
