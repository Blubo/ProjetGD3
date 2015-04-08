using UnityEngine;
using System.Collections;

public class GachetteCanon : MonoBehaviour {

	[HideInInspector]
	public bool grapped;
	[SerializeField]
	private GameObject canonBody;

	// Use this for initialization
	void Start () {
		grapped = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.GetComponent<Sticky>().v_numberOfLinks!=0){
			grapped=true;
			gameObject.GetComponent<Rigidbody>().isKinematic=false;
			canonBody.GetComponent<Rigidbody>().isKinematic=true;
		}else{
			grapped=false;
//			gameObject.GetComponent<Rigidbody>().isKinematic=true;
			canonBody.GetComponent<Rigidbody>().isKinematic=false;
		}
	}

//	void OnCollisionEnter(Collision col){
//		Debug.Log("collided "+ col.gameObject.name);
//		if(col.gameObject == canonBody){
//			Debug.Log("shoot canon");
//		}
//	}

//	void OnTriggerEnter(Collider col){
//		if(grapped == false){
//			if(col.gameObject.name.Equals("NewHookhead(Clone)")){
//				grapped = true;
//			}
//		}
//	}
//
//	void OnTriggerExit(Collider col){
//		if(grapped == true){
//			if(col.gameObject.name.Equals("NewHookhead(Clone)")){
//				grapped = false;
//			}
//		}
//	}
}
