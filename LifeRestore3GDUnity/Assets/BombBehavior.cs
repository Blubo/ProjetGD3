using UnityEngine;
using System.Collections;

public class BombBehavior : MonoBehaviour
{
  public float RangeExplosion, _KnockBack;
  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  void OnCollisionEnter(Collision col)
  {
    Collider[] tab = Physics.OverlapSphere(transform.position, RangeExplosion);
    foreach (Collider c in tab)
    {
      Rigidbody r = c.GetComponent<Rigidbody>();
      if (r != null)
      {
        r.AddExplosionForce(_KnockBack, transform.position, RangeExplosion);
      }
      //Si c'est un joueur
      //Si c'est un ennemi
      //Si c'est l'Idole
    }

    Destroy(gameObject);
  }
}
