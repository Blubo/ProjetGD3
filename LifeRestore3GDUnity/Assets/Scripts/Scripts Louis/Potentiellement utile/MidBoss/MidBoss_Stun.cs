using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MidBoss_Stun : MonoBehaviour {
	
	[SerializeField]
	private int _WillBeStunnedIn;
	private int _initWillBeStunnedIn;
	[SerializeField]
	private GameObject weakPoint, exposed, hidden;

	[SerializeField]
	private float v_exposedTimer;
	private float _exTimer;

	// Use this for initialization
	void Start () {
		_initWillBeStunnedIn = _WillBeStunnedIn;
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log("will be "+_WillBeStunnedIn);
		if (_WillBeStunnedIn<=0 ){
			//weakpoint
			_exTimer+=Time.deltaTime;
			weakPoint.transform.position = exposed.transform.position;
		}else{
			weakPoint.transform.position = hidden.transform.position;
		}

		if(_exTimer>=v_exposedTimer){
			_exTimer=0f;
			_WillBeStunnedIn = _initWillBeStunnedIn;
		}
	}
	
	void OnCollisionEnter(Collision _collision)
	{
		if(_collision.gameObject.layer == LayerMask.NameToLayer("CanBreak")){
			_WillBeStunnedIn -= 1;
		}
	}
}
