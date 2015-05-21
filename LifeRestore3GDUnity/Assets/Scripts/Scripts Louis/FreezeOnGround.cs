using UnityEngine;
using System.Collections;

public class FreezeOnGround : MonoBehaviour {
	
	// Use this for initialization
	void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag.Equals("Ground") || col.gameObject.tag.Equals("Static")){
			if(gameObject.GetComponent<Rigidbody>()!=null) Destroy(gameObject.GetComponent<Rigidbody>());
		}
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag.Equals("Ground") || col.gameObject.tag.Equals("Static")){
			if(gameObject.GetComponent<Rigidbody>()!=null) Destroy(gameObject.GetComponent<Rigidbody>());
		}
	}
}
