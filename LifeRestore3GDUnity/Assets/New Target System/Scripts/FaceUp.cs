using UnityEngine;
using System.Collections;

public class FaceUp : MonoBehaviour {
	[SerializeField]
	private float lookSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.rotation = Camera.main.transform.rotation;
//
//		Vector3 targetPostition = new Vector3(Camera.main.transform.position.x, this.transform.position.y, Camera.main.transform.position.z);
//		this.transform.LookAt( targetPostition ) ;

//		Vector3 targetDir = Camera.main.transform.position - transform.position;
//		float step = lookSpeed * Time.deltaTime;
//		Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
//		transform.rotation = Quaternion.LookRotation(newDir);

	}
}
