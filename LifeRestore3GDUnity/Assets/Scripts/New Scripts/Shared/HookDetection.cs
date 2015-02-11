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
			if(_collider.GetComponent<ShootF>()._alternateDash==false){
				if(_collider.GetComponent<ShootF>()._Dashing == true){
					testing = transform.parent.gameObject.GetComponent<HookHeadF>()._myShooter;
					Test();
				}
			}else{
				if(_collider.GetComponent<ShootF>()._DashingTest == true){
					testing = transform.parent.gameObject.GetComponent<HookHeadF>()._myShooter;
					Test();
				}
			}
		}
	}
	void Test(){
		testing.GetComponent<ShootF>().DetachLink(_LinkToDetach);
	}
}
