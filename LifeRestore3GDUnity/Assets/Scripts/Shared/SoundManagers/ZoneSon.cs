using UnityEngine;
using System.Collections;

public class ZoneSon : MonoBehaviour {

	[Tooltip("Mettre l'asset qui controle les changements de son: idole, joueur1, barycentre des joueurs...")]
	[SerializeField]
	private GameObject player1;

	[Tooltip("Mettre le numéro de la zone de son, attention, UN SEUL")]
	[SerializeField]
	private int whichZoneAmI;

	private MusicManagerFMOD theMusicManager;

	void Start(){
		theMusicManager = Camera.main.GetComponent<MusicManagerFMOD>();
	}


	void OnTriggerEnter(Collider col){
		if(col.gameObject == player1){
			switch (whichZoneAmI) {
			case 1:
				//0>0.49
				//calme
//				Debug.Log("get in 1");
				theMusicManager.ChangeParam(theMusicManager.musicDemonstration, 0.3f);

				break;
			case 2:
				//0.5>0.9
				//rythmé
//				Debug.Log("get in 2");

				theMusicManager.ChangeParam(theMusicManager.musicDemonstration, 0.7f);

				break;
			default:
				break;
			}
		}
	}
}
