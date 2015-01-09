using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerState : MonoBehaviour {

	public bool v_isLinked, v_isPlayerLinked;
	private float _linkingTimer;
	public float v_myHP, v_myPoints, v_myVisibleHP;

	[HideInInspector]
	//public GameObject[] _whoILinked, _whoLinkedMe;
	public List<GameObject> _whoILinked, _whoLinkedMe;

	// Use this for initialization
	void Start () {
		v_myHP=500f;
		_linkingTimer=5f;
		v_isLinked=true;
		v_isPlayerLinked=false;
	}
	
	// Update is called once per frame
	void Update () {

		//Debug.Log("My HP is:" + v_myHP);
		if(v_myHP<=0){
			Debug.Log("I'm dead!");
		}

		if(v_myHP>=500){
			v_myHP=500;
		}

		if(v_myHP<=0){
			v_myHP=0;
		}

		if(v_isLinked==false){
			_linkingTimer -= Time.deltaTime;
		}

		if(_linkingTimer<=0){
			_linkingTimer=5;
			v_isLinked=true;
		}

		if(v_isPlayerLinked==true){
			v_myHP+=5*Time.deltaTime;
		}

		v_myVisibleHP=Mathf.CeilToInt(v_myHP);
	}
}
