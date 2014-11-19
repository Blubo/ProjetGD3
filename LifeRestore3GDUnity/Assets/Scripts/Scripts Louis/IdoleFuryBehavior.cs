using UnityEngine;
using System.Collections;

public class IdoleFuryBehavior : MonoBehaviour {

	private float _furyTimer, _calmingTimer;

	// Use this for initialization
	void Start () {
		_furyTimer=10f;
		_calmingTimer=2f;
		gameObject.renderer.material.color = Color.blue;
	}
	
	// Update is called once per frame
	void Update () {
		if(_furyTimer>=0f){
			_furyTimer -= Time.deltaTime;
		}

		if(_furyTimer<0){
			for (int i = 0; i < gameObject.GetComponent<BasicIdoleSript>().v_playersList.Count; i++) {
				gameObject.GetComponent<BasicIdoleSript>().v_playersList[i].GetComponent<PlayerState>().v_isLinked = false;

			}

			gameObject.renderer.material.color = Color.red;
			_calmingTimer -= Time.deltaTime;

			if(_calmingTimer<=0f){
				_calmingTimer=2f;
				_furyTimer=10f;
				gameObject.renderer.material.color = Color.blue;
			}
		}
	}
}