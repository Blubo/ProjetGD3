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
			FMOD_StudioSystem.instance.PlayOneShot("event:/Ouglou/Ouglou tir", gameObject.transform.position);
			break;
		case "Interrupteur snap":
			FMOD_StudioSystem.instance.PlayOneShot("event:/Décors et interrupteur/Interrupteur snap", gameObject.transform.position);
			break;
		case "Porte ouverture":
			FMOD_StudioSystem.instance.PlayOneShot("event:/Décors et interrupteur/Porte ouverture", gameObject.transform.position);
			break;
		case "Interrupteur activer":
			FMOD_StudioSystem.instance.PlayOneShot("event:/Décors et interrupteur/Interrupteur activer", gameObject.transform.position);
			break;
		case "Canon tir":
			FMOD_StudioSystem.instance.PlayOneShot("event:/Tourelle et Canon/Canon tir", gameObject.transform.position);
			break;
		case "Ouglou collecte gros":
			FMOD_StudioSystem.instance.PlayOneShot("event:/Ouglou/Ouglou collecte gros", gameObject.transform.position);
			break;
		case "Ennemi standart mort":
			FMOD_StudioSystem.instance.PlayOneShot("event:/Ennemis/Ennemi standart mort", gameObject.transform.position);
			break;
				default:
						break;
			//Camera.main.GetComponent<SoundManagerHeritTest>().PlaySoundOneShot("Ouglou tir");

		}

	}

}
