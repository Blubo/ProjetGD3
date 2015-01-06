﻿using UnityEngine;
using System.Collections;

public class GrappleComebackLouis5Janv : MonoBehaviour {

	[HideInInspector]
	public GameObject _myShooter;
	private Vector3 _myShooterInitPos, _myShooterPos;
	public float v_returnDistance, v_returnSpeedConst;
	private bool shouldIReturn=false;

	// Use this for initialization
	void Start () {

		if(_myShooter!=null){
			_myShooterInitPos=_myShooter.transform.position;

		}
		//gameObject.transform.Find("Maillon8").GetComponent<SpringJoint>().connectedBody=_myShooter.rigidbody;

	}
	
	// Update is called once per frame
	void Update () {
		if(_myShooter!=null){

			_myShooterPos=_myShooter.transform.position;

			//15
			if(Vector3.Distance(gameObject.transform.position, _myShooterInitPos)>=v_returnDistance){
				shouldIReturn=true;
			}

			if(shouldIReturn==true){
	//			shouldIReturn=false;
				gameObject.rigidbody.velocity = Vector3.zero;
				gameObject.rigidbody.AddForce((_myShooterPos-gameObject.transform.position)*_myShooter.GetComponent<ShootHookLouis5Janv>()._SpeedBullet*v_returnSpeedConst);
			}

	//		if(gameObject.transform.position==_myShooterPos && shouldIReturn==true){
	//			Debug.Log("im here");
	//			Destroy(gameObject);
	//		}

			//ce 5 est rentré en dur, et correspond au rayon de l'avatar, soit quand détruire le lien quand il revient.
			if(Vector3.Distance(gameObject.transform.position, _myShooterPos)<=4f && shouldIReturn==true){
				Destroy(gameObject.transform.Find("B 5Janv"));
				Destroy(gameObject);
			}
		}
	}

//	void OnCollisionEnter(Collision collision){
//		shouldIReturn=true;
////		if(collision.gameObject==_myShooter){
////			Destroy(gameObject);
////		}
//	}

//	void OnTriggerEnter(Collider collision){
//		if(collision.gameObject!=_myShooter){
//			Debug.Log("heyyy");
//			shouldIReturn=true;
//		}
//	}

	void OnCollisionEnter(Collision collision){
		if(collision.gameObject==_myShooter){
			if(shouldIReturn==true){
				Destroy(gameObject.transform.Find("B 5Janv"));
				Destroy(gameObject);
				Debug.Log("kill that motherfucker");
			}
		}
	}
}
