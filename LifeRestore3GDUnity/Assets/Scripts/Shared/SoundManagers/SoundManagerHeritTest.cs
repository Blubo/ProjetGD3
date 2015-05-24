using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;

public class SoundManagerHeritTest : SoundManager {
	
	public override void PlaySoundOneShot(string text){

		//		ce que j'ai:
		//done
		// tir tourelle/canon
		//tir ouglou (à tester)
		//collecte ouglou
		//musique IG
		// snap idole/interrupteur
		//activation mécanisme interrupteur
		// ouverture porte

		//not done
		//blessure ouglou
		// explosion bombe
		//idle ouglou (à tester)
		//musique écran titre
		// musique écran des scores
		// blessure idole
		// Mort ennemi standard
		//impact bloc de bois

		switch (text) {
		case "Ouglou tir":
			FMOD_StudioSystem.instance.PlayOneShot("event:/FMODOvos/Ovo tir", gameObject.transform.position);
			break;
		case "Ovo collecte":
			FMOD_StudioSystem.instance.PlayOneShot("event:/FMODOvos/Ovo collecte", gameObject.transform.position);
			break;
		case "Ovo blessure":
			FMOD_StudioSystem.instance.PlayOneShot("event:/FMODOvos/Ovo blessure", gameObject.transform.position);
			break;
		case "Interrupteur snap":
			FMOD_StudioSystem.instance.PlayOneShot("event:/FMODDécors et interrupteur/Interrupteur snap", gameObject.transform.position);
			break;
		case "Porte ouverture":
			FMOD_StudioSystem.instance.PlayOneShot("event:/FMODDécors et interrupteur/Porte ouverture", gameObject.transform.position);
			break;
		case "Interrupteur activer":
			FMOD_StudioSystem.instance.PlayOneShot("event:/FMODDécors et interrupteur/Interrupteur activer", gameObject.transform.position);
			break;
		case "Canon tir":
			FMOD_StudioSystem.instance.PlayOneShot("event:/FMODTourelle et Canon/Canon tir", gameObject.transform.position);
			break;
		case "Ennemi standart mort":
			FMOD_StudioSystem.instance.PlayOneShot("event:/FMODEnnemis/Ennemi standart mort", gameObject.transform.position);
			break;
		case "Bloc bois dommage":
			FMOD_StudioSystem.instance.PlayOneShot("event:/FMODBlocs et bombes/Bloc bois dommage", gameObject.transform.position);
			break;
		case "Impact bloc fronde":
			FMOD_StudioSystem.instance.PlayOneShot("event:/FMODBlocs et bombes/Impact bloc fronde", gameObject.transform.position);
			break;
		case "Caserne dommage":
			FMOD_StudioSystem.instance.PlayOneShot("event:/FMODCasernes/Caserne dommage", gameObject.transform.position);
			break;
		case "Arbre destruction":
			FMOD_StudioSystem.instance.PlayOneShot("event:/FMODDécors et interrupteur/Arbre destruction", gameObject.transform.position);
			break;
		case "Barrière impact":
			FMOD_StudioSystem.instance.PlayOneShot("event:/FMODDécors et interrupteur/Barrière impact", gameObject.transform.position);
			break;
		case "Brasero destruction":
			FMOD_StudioSystem.instance.PlayOneShot("event:/FMODDécors et interrupteur/Brasero destruction", gameObject.transform.position);
			break;
		case "Idole blessure":
			FMOD_StudioSystem.instance.PlayOneShot("event:/FMODIdole/Idole blessure", gameObject.transform.position);
			break;
		case "Idole danger":
			FMOD_StudioSystem.instance.PlayOneShot("event:/FMODIdole/Idole danger", gameObject.transform.position);
			break;



				default:
						break;
			//Camera.main.GetComponent<SoundManagerHeritTest>().PlaySoundOneShot("Ouglou tir");

		}
	}
}
