using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TitleScreenTrigger : MonoBehaviour {

	private int playerCount;
	[SerializeField]
	private int TriggerCount;
	[SerializeField]
	private GameObject associatedCollider;
	private int whereWereThey;
	// Use this for initialization

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {


	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag.Equals("Player")){
			playerCount+=1;
			if(playerCount>=3){
				associatedCollider.GetComponent<Collider>().isTrigger = true;
			}
		}
	}

	void OnTriggerExit(Collider col){
		if(col.gameObject.tag.Equals("Player")){
			playerCount-=1;
			if(playerCount<=0){
//				GetComponent<Collider>().isTrigger = false;
			}
		}
	}
}
