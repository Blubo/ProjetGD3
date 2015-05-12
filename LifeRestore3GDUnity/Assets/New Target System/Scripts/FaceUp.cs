using UnityEngine;
using System.Collections;

public class FaceUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.rotation = Camera.main.transform.rotation;
//		gameObject.transform.up = Vector3.forward;
	}
}
