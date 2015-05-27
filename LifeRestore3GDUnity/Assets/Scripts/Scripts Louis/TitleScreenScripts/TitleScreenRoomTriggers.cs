using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TitleScreenRoomTriggers : MonoBehaviour {

	private List<GameObject> PlayersInTrigger;

	[SerializeField]
	private int TriggerCount;

	private int playerCount;
	private int whereWereThey;

	// Use this for initialization
	void Start () {
		PlayersInTrigger = new List<GameObject>();

	}

	// Update is called once per frame
	void Update () {
		playerCount = PlayersInTrigger.Count;
		
		if(playerCount >= 3){
			Camera.main.GetComponent<TitleScreenCameraManager>().MoveCamera(TriggerCount);
		}
	}

	void OnTriggerStay(Collider col){
		if(col.gameObject.tag.Equals("Player")){
			col.GetComponent<TitleScreenPlayerPosition>().whichRoomImIn = TriggerCount;
		}
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag.Equals("Player")){
			PlayersInTrigger.Add(col.gameObject);
		}
	}
	
	void OnTriggerExit(Collider col){
		if(col.gameObject.tag.Equals("Player")){
//			if(playerCount == 3){
//				Camera.main.GetComponent<TitleScreenCameraManager>().RemoveCamera();
//			}
			PlayersInTrigger.Remove(col.gameObject);
		}
	}

}
