using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//ce script est posé sur les colliders entre les salles
//et sert à envoyer l'ordre de téléport et de moveCamera??

public class TitleScreenTrigger0to2 : MonoBehaviour {

	private int playerCount;
	private int whereWereThey;
//	[SerializeField]
//	private int targetIndex1, targetIndex2;
	private List<GameObject> PlayersInTrigger;


	void Start () {
		PlayersInTrigger = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {


	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag.Equals("Player")){
			PlayersInTrigger.Add(col.gameObject);
			playerCount+=1;
			if(playerCount>=3){
				Debug.Log("well");
				for (int i = 0; i < PlayersInTrigger.Count; i++) {
					if(PlayersInTrigger[i].GetComponent<TitleScreenPlayerPosition>().whichRoomImIn == 0){
						PlayersInTrigger[i].GetComponent<TitleScreenPlayerPosition>().TeleportPlayer(new Vector3(-5,0,0));
						Camera.main.GetComponent<TitleScreenCameraManager>().currentWayPointNumber = 2;


					}else if(PlayersInTrigger[i].GetComponent<TitleScreenPlayerPosition>().whichRoomImIn == 2){
						PlayersInTrigger[i].GetComponent<TitleScreenPlayerPosition>().TeleportPlayer(new Vector3(5,0,0));
						Camera.main.GetComponent<TitleScreenCameraManager>().currentWayPointNumber = 0;

					}
				}
			}
		}
	}

	void OnCollisionExit(Collision col){
		if(col.gameObject.tag.Equals("Player")){
			playerCount-=1;
			PlayersInTrigger.Remove(col.gameObject);
		}
	}
}
