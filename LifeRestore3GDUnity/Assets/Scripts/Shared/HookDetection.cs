using UnityEngine;
using System.Collections;

public class HookDetection : MonoBehaviour {

	//ce script est mis sur le lien
	//son parent=la tete du lien que j'ai tiré
	//donc myShooter=bah le joueur m'ayant tiré
	//_collider=un autre joueur que celui m'ayant tiré
	private int _LinkToDetach;
	private GameObject testing;

	void Start () {
		if(transform.parent.GetComponent<HookHeadF>().howWasIShot!=null){
			_LinkToDetach = transform.parent.GetComponent<HookHeadF>().howWasIShot;
			_LinkToDetach -= 1;	//Petit problème de continuité, pas trop grave
		}
	}

	// On peut utiliser un layer pour pouvoir éviter les triggers non physiques
	void OnTriggerEnter(Collider _collider){
		if(_collider.gameObject.tag == "Player" && _collider!=gameObject.transform.parent.gameObject.GetComponent<HookHeadF>()._myShooter){
			/*
			 * si le gars que je collide était en dash
			 * testing = mon shooter
			 * et on execute Brise sur lui
			*/
		}
	}
	void Brise(GameObject targetToUnlink, int linkToDetach){
		if(targetToUnlink.GetComponent<ShootF>()!=null){
			targetToUnlink.GetComponent<ShootF>().DetachLink(linkToDetach);
		}
	}
}
