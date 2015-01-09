using UnityEngine;
using System.Collections;

public class InTheMiddle5Janv : MonoBehaviour {

	public GameObject v_A, v_C;
	//v_C=mon parent
	private Vector3 _scale;

	// Use this for initialization
	void Start () {
//		v_A=v_C.GetComponent<GrappleComebackLouis5Janv>()._myShooter;
//		v_A=v_C.GetComponent<NewHookHead>()._myShooter;
		v_A=v_C.GetComponent<HookHeadF>()._myShooter;


	}
	
	// Update is called once per frame
	void Update () {
		//anchorOnCircle est l'intersection du cercle de l'avatar (le rayon 5) et du vecteur v_A;v_C
		Vector3 anchorOnCircle = v_C.transform.position-v_A.transform.position;
		anchorOnCircle.Normalize();
		anchorOnCircle = v_A.transform.position + anchorOnCircle*1.75f;

		Debug.DrawRay(anchorOnCircle, Vector3.up);

		//SWITCH THE COMMENTS ON THE TWO NEXT LINES FOR MANAGING THE WIDTH OF THE AVATAR
//		gameObject.transform.position = (v_A.transform.position+ v_C.transform.position)/2;
		gameObject.transform.position = (anchorOnCircle+ v_C.transform.position)/2;

		gameObject.transform.LookAt(v_C.transform);

		//SWITCH THE COMMENTS ON THE TWO NEXT LINES FOR MANAGING THE WIDTH OF THE AVATAR
//		_scale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, Vector3.Distance(v_A.transform.position, v_C.transform.position)/2);
		_scale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, Vector3.Distance(anchorOnCircle, v_C.transform.position)/2);

		gameObject.transform.localScale=_scale;
	}
}
