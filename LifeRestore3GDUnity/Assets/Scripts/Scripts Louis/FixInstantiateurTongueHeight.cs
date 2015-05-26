using UnityEngine;
using System.Collections;

public class FixInstantiateurTongueHeight : MonoBehaviour {

	private float initHeight;

	// Use this for initialization
	void Start () {
		initHeight = gameObject.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = new Vector3(gameObject.transform.position.x ,initHeight ,gameObject.transform.position.z);
	}
}
