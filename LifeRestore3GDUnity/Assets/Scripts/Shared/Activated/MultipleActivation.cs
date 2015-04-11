﻿using UnityEngine;
using System.Collections;

public class MultipleActivation : MonoBehaviour {

	[Tooltip("How many -activated messages- must this receive to activate its target")]
	[SerializeField]
	private int howManyActivationForEffect;

	[Tooltip("The item to activate is : ")]
	[SerializeField]
	private GameObject activatedItem;

	[Tooltip("Infinite uses or just one")]
	[SerializeField]
	private bool limitedUses;
	
	[Tooltip("Number of uses if limited")]
	[SerializeField]
	private int numberLimitedUses;

	//the number of times this has activated its target
	private int activatedCounter;

	//the number of time this has been activated
	private int gotActivatedCounter;

	//has it activated its target already?
	private bool hasActivatedTarget = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(hasActivatedTarget==false && gotActivatedCounter == howManyActivationForEffect){
			activatedCounter+=1;
			hasActivatedTarget=true;
			activatedItem.SendMessage("Activated");
		}
	}

	void Activated(){
		if((limitedUses==true && activatedCounter<numberLimitedUses)||limitedUses==false){
			gotActivatedCounter+=1;
		}
	}
}
