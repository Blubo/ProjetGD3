using UnityEngine;
using System.Collections;

public class TestEnerjCinetique : MonoBehaviour {

  private float Masse;
  private float Vitesse;
  private Rigidbody rb;

  private float Ej;

	// Use this for initialization
	void Start () {
    rb = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
    Masse = rb.mass;
    Vitesse = rb.velocity.magnitude;

    Ej = 0.5f * Masse * (Vitesse * Vitesse);
    Debug.Log("Ej      "+  Ej);
	}
}
