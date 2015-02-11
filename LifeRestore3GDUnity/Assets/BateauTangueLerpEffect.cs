using UnityEngine;
using System.Collections;

public class BateauTangueLerpEffect : MonoBehaviour {

	public GameObject v_target1, v_target2;
	public float v_tangueSpeed;
	private float _timer;

	// Use this for initialization
	void Start () {
		_timer=0f;

	}
	
	// Update is called once per frame
	void Update () {
		_timer+=Time.deltaTime;


		if(gameObject.transform.rotation == v_target1.transform.rotation){
//			gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, gameObject.transform.rotation.eulerAngles + new Vector3(0,0,30), Time.deltaTime);
//			gameObject.rigidbody.addTorque
		}

		if(gameObject.transform.rotation == v_target2.transform.rotation){
//			gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, gameObject.transform.rotation.eulerAngles + new Vector3(0,0,-30), Time.deltaTime);

		}
	
	}
}
