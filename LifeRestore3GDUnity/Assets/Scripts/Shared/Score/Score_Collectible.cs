﻿using UnityEngine;
using System.Collections;

public class Score_Collectible : MonoBehaviour {

    [SerializeField]
	private int _value, _Multiplicator;
	[SerializeField]
	[Tooltip("Very slightly above 1f")]
	private float _playerSizeMultiplicator;
    private ScoreManager _ScoreManager;
	[SerializeField]
	private GameObject destructionParticle, linkedDestructionParticle;
	private GameObject idole;

    void Awake()
    {
		idole = GameObject.Find("Idole");
        _ScoreManager = Camera.main.GetComponent<ScoreManager>();
//        gameObject.transform.localScale = gameObject.transform.localScale+new Vector3(_Multiplicator, _Multiplicator, _Multiplicator);

    }

	void OnTriggerEnter(Collider _collision){
		//SI CE COLLECTIBLE EST TOUCHE PAR UN JOUEUR
		if (_collision.gameObject.tag == "Player"){
			Player_Status hisPlayerStatus = _collision.gameObject.GetComponent<Player_Status>();
			if (!hisPlayerStatus._IsInvincible){
				if(hisPlayerStatus.linkedObject!=null && hisPlayerStatus.linkedObject.tag.Equals("Idole") == true){
					_Multiplicator = 2;
					Instantiate(linkedDestructionParticle, transform.position, Quaternion.identity);
					
				}else{
					Instantiate(destructionParticle, transform.position, Quaternion.identity);
				}
				
				_collision.gameObject.GetComponent<FatPlayerScript>().ChangeSize(_playerSizeMultiplicator);
				
				//ALORS ON AUGMENTE LE SCORE DU JOUEUR DE "VALUE"
				string _name = _collision.gameObject.name;
				
				_ScoreManager.Increase_score(_name, _value * _Multiplicator);
				//Destruction du collectible après le calcul 
				//ET ON JOUE LE SON DE COLLECTE AVANT DE DETRUIRE LE COLLECTIBLE
				Camera.main.GetComponent<SoundManagerHeritTest>().PlaySoundOneShot("Ovo collecte");
				//ON DETRUIT MAINTENANT LE COLLECTIBLE
				Destroy(gameObject);
			}
		}
	}

//	void OnCollisionEnter(Collision _collision){
//		//SI CE COLLECTIBLE EST TOUCHE PAR UN JOUEUR
//		if (_collision.gameObject.tag == "Player"){
//			Player_Status hisPlayerStatus = _collision.gameObject.GetComponent<Player_Status>();
//			if (!hisPlayerStatus._IsInvincible){
//				if(hisPlayerStatus.linkedObject!=null && hisPlayerStatus.linkedObject.tag.Equals("Idole") == true){
//					_Multiplicator = 2;
//					Instantiate(linkedDestructionParticle, transform.position, Quaternion.identity);
//
//				}else{
//					Instantiate(destructionParticle, transform.position, Quaternion.identity);
//				}
//				
//				_collision.gameObject.GetComponent<FatPlayerScript>().ChangeSize(_playerSizeMultiplicator);
//				
//				//ALORS ON AUGMENTE LE SCORE DU JOUEUR DE "VALUE"
//				string _name = _collision.gameObject.name;
//				
//				_ScoreManager.Increase_score(_name, _value * _Multiplicator);
//				//Destruction du collectible après le calcul 
//				//ET ON JOUE LE SON DE COLLECTE AVANT DE DETRUIRE LE COLLECTIBLE
//				Camera.main.GetComponent<SoundManagerHeritTest>().PlaySoundOneShot("Ovo collecte");
//				//ON DETRUIT MAINTENANT LE COLLECTIBLE
//				Destroy(gameObject);
//			}
//		}
//	}
}
