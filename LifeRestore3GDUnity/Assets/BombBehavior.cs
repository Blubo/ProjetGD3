using UnityEngine;
using System.Collections;

public class BombBehavior : MonoBehaviour
{

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
    Collider[] tab = Physics.OverlapSphere(transform.position, 100f);
    foreach (Collider c in tab)
    {
      Rigidbody r = c.GetComponent<Rigidbody>();
      if (r != null)
      {
        r.AddExplosionForce(1000f, transform.position, 1000f);
      }
    }

    Destroy(gameObject);
  }
}
