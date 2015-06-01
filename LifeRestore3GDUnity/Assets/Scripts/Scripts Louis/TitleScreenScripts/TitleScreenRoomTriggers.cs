using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// on pose ce script sur les triggers géant des salles
//il sert à indiquer aux joueurs dans quelle salle ils se trouvent

public class TitleScreenRoomTriggers : MonoBehaviour {
	
	[SerializeField]
	private int TriggerCount;

	private int playerCount;
  public bool Slides;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerStay(Collider col){
		if(col.gameObject.tag.Equals("Player")){
			col.GetComponent<TitleScreenPlayerPosition>().whichRoomImIn = TriggerCount;
			if(TriggerCount == 1 && Slides == false){
				col.GetComponent<TitleScreenPlayerPosition>().ChangePlayerState(false);
			}
		}
	}
}