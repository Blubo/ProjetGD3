using UnityEngine;
using System.Collections;

public class KeyholeSystem : MonoBehaviour {

	private Vector3 initRot;
	[HideInInspector]
	public bool allowedToRotate;

	// Use this for initialization
	void Start () {
		initRot = gameObject.transform.eulerAngles;
//		Debug.Log ("init rot is "+ initRot);
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.transform.eulerAngles.y > initRot.y+10f){
			gameObject.transform.eulerAngles = new Vector3 (gameObject.transform.eulerAngles.x,  initRot.y+10f, gameObject.transform.eulerAngles.z);
			allowedToRotate = false;
//			Debug.Log("superior to 10");
		}

	}
}
