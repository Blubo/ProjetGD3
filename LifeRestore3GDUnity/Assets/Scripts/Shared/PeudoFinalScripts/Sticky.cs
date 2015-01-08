using UnityEngine;
using System.Collections;

public class Sticky : MonoBehaviour {

	//soit le mettre dans l'inspecteur de chaque objet auquel on souhaite pouvoir se connecter
	//soit le rajouter procéduralement depuis une collision avec HookHead (ou dans shoot), à chaque collision avec chaque item de tel tag
	//(et si le script existe déjà, alors juste v_number+1)
	[HideInInspector]
	public int v_numberOfLinks;

	// Use this for initialization
	void Start () {
		v_numberOfLinks=0;
	}
	
	// Update is called once per frame
	void Update () {
		//gérer le rigidoby des items auxquels on se connecte ici: si lien=! 0, alors rigidbody, si lien = 0 alors destroy/disable rigidbody
	}
}
