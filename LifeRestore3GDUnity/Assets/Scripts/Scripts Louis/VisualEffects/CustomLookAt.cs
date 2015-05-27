using UnityEngine;
using System.Collections;

public class CustomLookAt : MonoBehaviour {

	[SerializeField]
	private GameObject montagne;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.LookAt(montagne.transform);
	}
}
