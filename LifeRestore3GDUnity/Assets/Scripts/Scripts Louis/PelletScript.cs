using UnityEngine;
using System.Collections;

public class PelletScript : MonoBehaviour {

	private float _lifeTime=3f;
	private bool _collidedSomething = false;

	//Qui a tiré cette bullet, modifié dans LifeRestore
	[HideInInspector]
	public GameObject v_whoShotMe;
	public GameObject v_whoIShot;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		_lifeTime -= 1 *Time.deltaTime;

		if(_lifeTime<=0f){
			Destroy(gameObject);
		}

		v_whoShotMe.GetComponent<RuntimeRopeLouis>().v_create=true;
		v_whoShotMe.GetComponent<RuntimeRopeLouis>().pointA=v_whoShotMe.transform;
		v_whoShotMe.GetComponent<RuntimeRopeLouis>().pointB=gameObject.transform;

		Debug.Log("point A est "+ v_whoShotMe.transform.position);
		Debug.Log("point B est "+ gameObject.transform.position);


	}

//	void OnTriggerEnter(Collider collided){
//		if(v_whoShotMe != collided.gameObject && collided.gameObject.name!=("GroundPlane")){
//			if(_collidedSomething==false){
//				_collidedSomething=true;
//				v_whoIShot = collided.gameObject;
//				v_whoShotMe.GetComponent<RuntimeRopeLouis>().v_create=true;
//
//				v_whoShotMe.GetComponent<RuntimeRopeLouis>().pointA=v_whoShotMe.transform;
//				v_whoShotMe.GetComponent<RuntimeRopeLouis>().pointB=v_whoIShot.transform;
//
//
//			}
//		}
//	}
}
