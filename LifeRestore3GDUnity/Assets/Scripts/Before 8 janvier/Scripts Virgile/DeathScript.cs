using UnityEngine;
using System.Collections;
using System;

public class DeathScript : MonoBehaviour {

	PlayerState _myplayerstate;
	MovementScript _playermovement;
	LifeRestore _playeractions;

	// Use this for initialization
	void Start () {
		_myplayerstate = GetComponent<PlayerState> ();
		//Scripts à activer et désactiver
		_playermovement = GetComponent<MovementScript> ();
		_playeractions = GetComponent<LifeRestore>();

	}
	
	// Update is called once per frame
	void Update () {
		if (_myplayerstate.v_myHP <= 0.0f){
			//Etat de mort
			//Disparition puis respawn de l'entité 
			StartCoroutine(Death());
		}
	}
	IEnumerator Death(){
		gameObject.renderer.enabled = false;
		yield return new WaitForSeconds(3);
		//remise à neuf + arret des scripts
		_playermovement.enabled = false;

		_myplayerstate.v_isLinked=true;
		_myplayerstate.v_isPlayerLinked=false;


		
		//Replacement sur le plateau
		gameObject.transform.position = new Vector3 (0.0f, 1.0f, 0.0f);;
		_myplayerstate.v_myHP = 500.0f;
		gameObject.renderer.enabled = true;
		_playermovement.enabled = true;
	}
}
