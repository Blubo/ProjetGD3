using UnityEngine;
using System.Collections;

public class InTheMiddle : MonoBehaviour {

	public GameObject v_A, v_C;
	private Vector3 _scale;

	// Use this for initialization
	void Start () {
//		if(v_C.GetComponent<GrappleComebackLouis>()._myShooter!=null){
			v_A=v_C.GetComponent<GrappleComebackLouis>()._myShooter;
//		}
	}
	
	// Update is called once per frame
	void Update () {

		gameObject.transform.position = (v_A.transform.position+ v_C.transform.position)/2;

		gameObject.transform.LookAt(v_C.transform);
		_scale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, Vector3.Distance(v_A.transform.position, v_C.transform.position));
//		_scale = new Vector3(Vector3.Distance(v_A.transform.position, v_C.transform.position), Vector3.Distance(v_A.transform.position, v_C.transform.position), Vector3.Distance(v_A.transform.position, v_C.transform.position));
//		_scale = new Vector3(Vector3.Distance(v_A.transform.position, v_C.transform.position), gameObject.transform.localScale.y, Vector3.Distance(v_A.transform.position, v_C.transform.position));

		gameObject.transform.localScale=_scale;
		//gameObject.transform.localScale.z = Vector3.Distance(v_A.transform.position, v_C.transform.position);
	}
}
