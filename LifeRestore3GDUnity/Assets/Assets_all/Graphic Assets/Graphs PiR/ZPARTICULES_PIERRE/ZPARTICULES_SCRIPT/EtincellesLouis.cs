using UnityEngine;
using System.Collections;

public class EtincellesLouis : MonoBehaviour {

	public GameObject ParticlePrefab;

	private Rigidbody myRB;

	void Start(){
		myRB = GetComponent<Rigidbody>();
	}

	void OnCollisionEnter(Collision coll)
	{
//		if(myRB.velocity.magnitude >= 5) 
			StartCoroutine (SpawnParticle (coll.contacts [0]));
	}

	IEnumerator SpawnParticle (ContactPoint contact) 
	{
		Vector3 position = contact.point;
		Quaternion rotation = Quaternion.LookRotation (contact.normal);
		GameObject ParticleObject = GameObject.Instantiate (ParticlePrefab, position, rotation)as GameObject;
		ParticleSystem ps = ParticleObject.GetComponent<ParticleSystem> ();
		yield return new WaitForSeconds (ps.duration);
		GameObject.Destroy (ParticleObject);


	}
}
