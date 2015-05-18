using UnityEngine;
using System.Collections;

public class ActivatedBecomeRed : MonoBehaviour {

	void Start(){
	}

	void Activated(){
		gameObject.GetComponent<Renderer>().material.color = Color.red;

	}

	void Deactivated(){
		
	}
}
