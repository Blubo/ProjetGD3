using UnityEngine;
using System.Collections;

public class Etincelles : MonoBehaviour {

	public GameObject ParticlePrefab;

	void OnCollisionEnter(Collision coll)
	{
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
