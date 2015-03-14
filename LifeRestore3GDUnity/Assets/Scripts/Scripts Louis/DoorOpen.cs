using UnityEngine;
using System.Collections;

public class DoorOpen : MonoBehaviour {

	[SerializeField]
	private GameObject _caserne;

	private bool openedAlready = false;

	[SerializeField]
	private GameObject _openedLocation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(_caserne==null && openedAlready == false){
			Destroy(gameObject);
//			Vector3.Lerp(gameObject.transform.position, _openedLocation.transform.position, 1f);
//			if(gameObject.transform.position == _openedLocation.transform.position){
//				openedAlready = true;
//				Debug.Log("fdp2");
//			}
//			Debug.Log("fdp");

		}
	}
}
