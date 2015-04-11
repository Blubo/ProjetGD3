using UnityEngine;
using System.Collections;

public class ActivatedIdoleRiseFromGround : MonoBehaviour {
	
	[Tooltip("This is the end position of this appearing cube")]
	[SerializeField]
	private GameObject v_target;
	
	private bool receivedOrder = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(receivedOrder==true){
			gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
			gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, v_target.transform.position, 0.1f);
		}
		
		if(gameObject.transform.position == v_target.transform.position){
			gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
			gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
			receivedOrder=false;
			Destroy (GetComponent<ActivatedIdoleRiseFromGround>());
		}
	}
	
	void Activated(){
		receivedOrder=true;
	}
}
