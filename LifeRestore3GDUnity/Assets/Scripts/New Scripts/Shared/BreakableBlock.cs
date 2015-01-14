using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BreakableBlock : MonoBehaviour {
	
	public float MinForceToBreak;
	
	private float _MassCollided, _veloMagCollided;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionEnter(Collision _collision){
		// On a besoin que ce soit un joueur UNIQUEMENT qui soit détecter
		if(_collision.gameObject.tag == "Player"){
			_MassCollided = _collision.rigidbody.mass;
			_veloMagCollided = _collision.rigidbody.velocity.magnitude;

			float vFinal = _collision.rigidbody.mass * _collision.relativeVelocity.magnitude / (rigidbody.mass + _collision.rigidbody.mass);
			float impulse = vFinal * rigidbody.mass;

//			Debug.Log("impulse is "+ impulse);
			if (impulse >= MinForceToBreak){

//			if (CalculateForce() >= MinForceToBreak){
				if(_collision.gameObject.GetComponent<ShootF>()._myHook!=null){
					Destroy(_collision.gameObject.GetComponent<ShootF>()._myHook);
				}
				if(_collision.gameObject.GetComponent<ShootF>()._myHook1!=null){
					Destroy(_collision.gameObject.GetComponent<ShootF>()._myHook1);
				}
				Destroy(gameObject);
			}
		}
	}
	
	float CalculateForce(){
		float _Force = _MassCollided * _veloMagCollided;
//		Debug.Log( _veloMagCollided);

//		Debug.Log (_Force);
		return _Force;
	}
}
