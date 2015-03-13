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

			float vFinal = _collision.rigidbody.mass * _collision.relativeVelocity.magnitude / (GetComponent<Rigidbody>().mass + _collision.rigidbody.mass);
			//interchangez les deux lignes suivantes si on veut effectivement casser les blocks selon sa propre énergie cinétique et non selon le poids du block
			float impulse = vFinal * GetComponent<Rigidbody>().mass;
//			float impulse = vFinal * _collision.rigidbody.mass;

			Debug.Log("impulse is now "+ impulse);
//			Debug.Log("impulse is "+ impulse);
			if (impulse >= MinForceToBreak){

//			if (CalculateForce() >= MinForceToBreak){
				if(_collision.gameObject.GetComponent<ShootF>()._myHook!=null){
					if(_collision.gameObject.GetComponent<ShootF>()._myHook.GetComponent<HookHeadF>().GrappedTo!=null){
						if(_collision.gameObject.GetComponent<ShootF>()._myHook.GetComponent<HookHeadF>().GrappedTo==gameObject){
							Destroy(_collision.gameObject.GetComponent<ShootF>()._myHook);
						}
					}
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
