using UnityEngine;
using System.Collections;

public class SpawnedBlocRiseToGround : MonoBehaviour {

	private bool needToMoveToSpawn;
	private Vector3 target;

	[Tooltip("The height at which our spawned bloc will rise when created")]
	[Range(0,Mathf.Infinity)]
	public float altitude;

	// Use this for initialization
	void Start () {
		needToMoveToSpawn=true;
		target = new Vector3(gameObject.transform.position.x, altitude, gameObject.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		if(needToMoveToSpawn==true){
			gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target, 0.1f);
			gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
			gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
		}
		
		if(gameObject.transform.position == target){
			needToMoveToSpawn=false;
			gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
			gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
		}
	}
}
