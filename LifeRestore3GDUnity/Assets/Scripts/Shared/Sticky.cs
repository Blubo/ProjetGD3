using UnityEngine;
using System.Collections;

public class Sticky : MonoBehaviour {

	//soit le mettre dans l'inspecteur de chaque objet auquel on souhaite pouvoir se connecter
	//soit le rajouter procéduralement depuis une collision avec HookHead (ou dans shoot), à chaque collision avec chaque item de tel tag
	//(et si le script existe déjà, alors juste v_number+1)
	[HideInInspector]
	public int v_numberOfLinks;
	private float _myInitMass;

	// Use this for initialization
	void Start () {
		v_numberOfLinks=0;
		_myInitMass=gameObject.GetComponent<Rigidbody>().mass;
	}
	
	// Update is called once per frame
	void Update () {
		//gérer le rigidoby des items auxquels on se connecte ici: si lien=! 0, alors rigidbody, si lien = 0 alors destroy/disable rigidbody
	}

//	void OnCollisionEnter(Collision col){
//		if(col.gameObject.GetComponent<Sticky>()!=null){
//			if(col.gameObject.GetComponent<Sticky>().v_numberOfLinks!=0 ){
//				Debug.Log("hit by a linked object");
//				gameObject.GetComponent<Rigidbody>().mass =0.1f;
//			}
//		}
//	}
//
//	void OnCollisionExit(Collision col){
//		if(col.gameObject.GetComponent<Sticky>()!=null){
//			if(col.gameObject.GetComponent<Sticky>().v_numberOfLinks!=0 ){
//				Debug.Log("hit by a linked object");
//				gameObject.GetComponent<Rigidbody>().mass = _myInitMass;
//			}
//		}
//	}
}
