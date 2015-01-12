using UnityEngine;
using System.Collections;

public class HookDetection : MonoBehaviour {
	
	private int _LinkToDetach;

	void Start () {
		_LinkToDetach = transform.parent.GetComponent<HookHeadF> ().howWasIShot;
		_LinkToDetach -= 1;	//Petit problème de continuité, pas trop grave
	}
	void Update () {
	
	}

	// On peut utiliser un layer pour pouvoir éviter les triggers non physiques
	void OnTriggerEnter(Collider _collider){
		Debug.Log ("Ce qui coupe le fil est" + _collider.name);		//vérification de l'objet qui cut le lien

		if(_collider.gameObject.tag == "Player" && _collider.GetComponent<ShootF>()._Dashing == true){			//Si c'est le joueur qui dash 
			collider.GetComponent<ShootF>().DetachLink(_LinkToDetach);
		}
	}
}
