using UnityEngine;
using System.Collections;

public class ItemOnRails : MonoBehaviour {

	[HideInInspector]
	public bool verticalPassport, horizontalPassport;
	public bool verticalOnly, horizontalOnly;
	private Rigidbody myRB;
	// Use this for initialization
	void Start () {
		myRB = gameObject.GetComponent<Rigidbody>();
		verticalPassport = false;
		horizontalPassport = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(verticalOnly==true){
			myRB.constraints = RigidbodyConstraints.None;

			myRB.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
		}

		if(horizontalOnly==true){
			myRB.constraints = RigidbodyConstraints.None;

			myRB.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;

		}

//
//		if(Input.GetKeyUp(KeyCode.Space)){
//			Debug.Log("vertical passport is "+verticalPassport);
//			Debug.Log("horizontal passport is "+horizontalPassport);
//			Debug.Log("verticalonly is "+ verticalOnly);
//			Debug.Log("horizontalonly is "+horizontalOnly);
//		}

	}

	public void RemoveConstraints(){
//		Debug.Log("well");
		myRB.constraints = RigidbodyConstraints.None;

		myRB.constraints = RigidbodyConstraints.FreezeRotation;
		verticalOnly = false;
		horizontalOnly = false;
	}

}
