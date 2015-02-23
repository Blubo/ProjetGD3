using UnityEngine;
using System.Collections;

public class TurretShooting : MonoBehaviour {

	[SerializeField]
	private float _shootCooldown,_shootForce;
	private float _shootTimer;

	[SerializeField]
	private GameObject _projectile, _instantiateur;

	// Use this for initialization
	void Start () {
		_shootTimer=_shootCooldown;
	}
	
	// Update is called once per frame
	void Update () {
		_shootTimer+=Time.deltaTime;
		if(_shootTimer>=_shootCooldown){
			Shoot();
		}
	}

	void Shoot(){
//		GameObject newProj = Instantiate(_projectile, _instantiateur.transform.position, Quaternion.identity) as GameObject;
		GameObject newProj = Instantiate (_projectile, _instantiateur.transform.position, Quaternion.identity) as GameObject;
		newProj.rigidbody.AddForce(gameObject.transform.up*_shootForce);

		newProj.GetComponent<TurretProjectile>().v_whoShotMe = gameObject;
		_shootTimer=0f;
	}
}
