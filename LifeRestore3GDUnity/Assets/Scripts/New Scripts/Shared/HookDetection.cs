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
//			Debug.Log(transform.parent.gameObject.GetComponent<HookHeadF>().howWasIShot);

			_LinkToDetach = transform.parent.GetComponent<HookHeadF>().howWasIShot;
			_LinkToDetach -= 1;	//Petit problème de continuité, pas trop grave
		}
	}

	void Update () {
	
	}

	// On peut utiliser un layer pour pouvoir éviter les triggers non physiques
	void OnTriggerEnter(Collider _collider){
//		Debug.Log ("Ce qui coupe le fil est" + _collider.name);		//vérification de l'objet qui cut le lien

		if(_collider.gameObject.tag == "Player" && _collider.GetComponent<ShootF>()._Dashing == true && _collider!=gameObject.transform.parent.gameObject.GetComponent<HookHeadF>()._myShooter){			//Si c'est le joueur qui dash 
//		if(_collider.gameObject.tag == "Player" && _collider.GetComponent<ShootF>()._DashingTest == true && _collider!=transform.parent.gameObject.GetComponent<HookHeadF>()._myShooter){			//Si c'est le joueur qui dash 
			Debug.Log("collided is "+_collider);
			testing = transform.parent.gameObject.GetComponent<HookHeadF>()._myShooter;

			Test();
//			Debug.Log("collided");
			//collider.GetComponent<ShootF>().DetachLink(_LinkToDetach);
//			Debug.Log("shot");
		}
	}
	void Test(){
		testing.GetComponent<ShootF>().DetachLink(_LinkToDetach);
	}
}
