using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActivatedDevientVisible : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void Activated(){

		gameObject.GetComponent<Text>().enabled = true;

	}

	void Deactivated(){}
}
