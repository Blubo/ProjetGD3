using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//ce script est posé sur les colliders entre les salles
//et sert à envoyer l'ordre de téléport et de moveCamera??

public class TitleScreenTrigger0to3 : MonoBehaviour {

	private int playerCount;
	private int whereWereThey;
//	[SerializeField]
//	private int targetIndex1, targetIndex2;
	private List<GameObject> PlayersInTrigger;

	[SerializeField]
	private GameObject directionFleche, doublage;

	void Start () {
		PlayersInTrigger = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		if(playerCount!=0){
			directionFleche.SetActive(true);
			doublage.SetActive(true);
		}else{
			directionFleche.SetActive(false);
			doublage.SetActive(false);
		}
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag.Equals("Player")){
			PlayersInTrigger.Add(col.gameObject);
			playerCount+=1;
			if(playerCount>=3){
				for (int i = 0; i < PlayersInTrigger.Count; i++) {
					if(PlayersInTrigger[i].GetComponent<TitleScreenPlayerPosition>().whichRoomImIn == 0){
						PlayersInTrigger[i].GetComponent<TitleScreenPlayerPosition>().TeleportPlayer(new Vector3(5,0,0));
						Camera.main.GetComponent<TitleScreenCameraManager>().currentWayPointNumber = 3;

					}else if(PlayersInTrigger[i].GetComponent<TitleScreenPlayerPosition>().whichRoomImIn == 3){
						PlayersInTrigger[i].GetComponent<TitleScreenPlayerPosition>().TeleportPlayer(new Vector3(-5,0,0));
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
