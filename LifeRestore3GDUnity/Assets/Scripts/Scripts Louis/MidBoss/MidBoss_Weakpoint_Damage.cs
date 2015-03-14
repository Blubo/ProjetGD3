using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MidBoss_Weakpoint_Damage : MonoBehaviour {
	
	[SerializeField]
	private int _WillBreakIn;
	
	[SerializeField]
	private bool _DestroyOnlyBySpecial;
	
	// Update is called once per frame
	void Update () {
		if (_WillBreakIn<=0 ){
			gameObject.transform.parent.GetComponent<MidBoss_Death>().Death();
		}
	}
	
	void OnCollisionEnter(Collision _collision)
	{
		if(_collision.gameObject.layer == LayerMask.NameToLayer("CanBreak")){
			if(_DestroyOnlyBySpecial){
				if (_collision.gameObject.tag == "Special"){
					_WillBreakIn -= 1;
				}
			}
			else if (!_DestroyOnlyBySpecial) { _WillBreakIn -= 1; }
		}
	}
}
