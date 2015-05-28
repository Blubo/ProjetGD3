using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//ce script est posé sur les colliders entre les salles
//et sert à envoyer l'ordre de téléport et de moveCamera??

public class TitleScreenTrigger0to1 : MonoBehaviour {

	[HideInInspector]
	public int playerCount;
	private int whereWereThey;
//	[SerializeField]
//	private int targetIndex1, targetIndex2;
	private List<GameObject> PlayersInTrigger;
	[SerializeField]
	private GameObject playersGroupe;
	[SerializeField]
	private Transform cameraTarget;
	void Start () {
		PlayersInTrigger = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {


	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag.Equals("Player")){
//			Debug.Log("fdp");
			PlayersInTrigger.Add(col.gameObject);
			playerCount+=1;
			if(playerCount>=3){
				for (int i = 0; i < PlayersInTrigger.Count; i++) {
//					Debug.Log("whyyy");
					if(PlayersInTrigger[i].GetComponent<TitleScreenPlayerPosition>().whichRoomImIn == 0){
						PlayersInTrigger[i].GetComponent<TitleScreenPlayerPosition>().TeleportPlayer(new Vector3(0,0,1));
						PlayersInTrigger[i].GetComponent<TitleScreenPlayerPosition>().ChangePlayerState(false);
//						Camera.main.GetComponent<TitleScreenCameraManager>().MoveCamera(1);
						Camera.main.GetComponent<TitleScreenCameraManager>().currentWayPointNumber = 1;
						playersGroupe.GetComponent<SmallCouloirMove>().CouloirMove(0);
//						Camera.main.GetComponent<TitleScreenCameraManager>().simpleLook = false;
//						Camera.main.GetComponent<TitleScreenCameraManager>().PleaseCameraAngle(cameraTarget);

						playersGroupe.GetComponent<MoveLevelSelect>().playersInSight = true;
						Camera.main.transform.Find("GeneralScoreUI").gameObject.SetActive(true);


					}else if(PlayersInTrigger[i].GetComponent<TitleScreenPlayerPosition>().whichRoomImIn == 1){
//						PlayersInTrigger[i].GetComponent<TitleScreenPlayerPosition>().TeleportPlayer(new Vector3(0,0,-3));
////						Camera.main.GetComponent<TitleScreenCameraManager>().MoveCamera(0);
//						Camera.main.GetComponent<TitleScreenCameraManager>().currentWayPointNumber = 0;
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
