﻿using UnityEngine;
using System.Collections;

public class RéceptacleBlocNormal : MonoBehaviour {

	//this for exterior consequence
	[SerializeField]
	[Tooltip("Insert gameObjects affected by this réceptacle")]
	private GameObject activatedItem;

	[Tooltip("Infinite uses or just one")]
	[SerializeField]
	private bool limitedUses;

	[Tooltip("Number of uses if limited")]
	[SerializeField]
	private int numberLimitedUses;

	//the number of times this has been activated
	private int activatedCounter;

	// Use this for initialization
	void Start () {
		activatedCounter=0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		//on a besoin d'activer l'objet dès qu'un bloc normal rentre dans le collider
		//pour commencer, on teste simplement si l'objet est de layer CanBreak
		//il faut parer à toute eventualité: qu'arrive-t-il aux objets posés dedans?
		//que ce soit les blocs normaux, avant et après une première activation, l'idole, joueur, ennemis, etc

		//de la meme manière, doit-on permetre un usage infini ou limité de ce réceptacle standard?
		//si limité, tuer le code ET fermer l'accès ?
//		if(col.gameObject.layer.Equals("CanBreak")){
		if(col.gameObject.layer == 17){

			if((limitedUses==true && activatedCounter<numberLimitedUses)||limitedUses==false){
//				Debug.Log("activated");
				activatedCounter+=1;
				activatedItem.SendMessage("Activated");
			}
		}
	}
}
