using UnityEngine;
using System.Collections;

public class UnfreezeWhenLinked : MonoBehaviour {


	private bool unfrozen;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(unfrozen == true) return;
		else {
			if(gameObject.GetComponent<Sticky>().v_numberOfLinks != 0) Unfreeze();
		}
	}

	void Unfreeze(){
		unfrozen = true;
		gameObject.GetComponent<Rigidbody>().isKinematic = false;
	}

}
