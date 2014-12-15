using UnityEngine;
using System.Collections;

public class SpecialPelletScript : MonoBehaviour {

	private float _lifeTime=3f;

	//Qui a tiré cette bullet, modifié dans LifeRestore
	//[HideInInspector]
	public GameObject v_whoShotMe;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log("who shot me?? "+ v_whoShotMe.name);
//		_lifeTime -= 1 *Time.deltaTime;
//
//		if(_lifeTime<=0f){
//			Destroy(gameObject);
//		}
	}

//	void OnTriggerEnter(Collider collided){
//		if(collided.gameObject.tag == "SpecialBoss"){
//			collided.gameObject.SendMessage("SpecialTakeDamage");
//		}
//	}


}
