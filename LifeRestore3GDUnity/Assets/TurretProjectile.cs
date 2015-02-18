using UnityEngine;
using System.Collections;

public class TurretProjectile : MonoBehaviour {

	[SerializeField]
	private float _authorizedLifeTime;
	private float _lifeTime;

	[HideInInspector]
	public GameObject v_whoShotMe;

	// Update is called once per frame
	void Update () {
		_lifeTime+=Time.deltaTime;
		if(_lifeTime>=_authorizedLifeTime){
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter(Collision collision){
		if(collision.gameObject != v_whoShotMe){
			Destroy(gameObject);
			Debug.Log("do something interesting!");
		}
	}
}
