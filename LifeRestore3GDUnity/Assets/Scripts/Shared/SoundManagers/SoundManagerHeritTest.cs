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
      case "Impact Pierre":
        FMOD_StudioSystem.instance.PlayOneShot("event:/FMODBlocs/Bloc pierre impact", gameObject.transform.position);
        break;
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
    case "Tourelle tir":
      FMOD_StudioSystem.instance.PlayOneShot("event:/FMODTourelle et Canon/Tourelle tir", gameObject.transform.position);
      break;
    case "Bloc bois dommage":
      FMOD_StudioSystem.instance.PlayOneShot("event:/FMODBlocs/Bloc bois dommage", gameObject.transform.position);
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
    case "Barriere impact":
      FMOD_StudioSystem.instance.PlayOneShot("event:/FMODDécors et interrupteur/Barriere impact", gameObject.transform.position);
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
    case "Marmite":
      FMOD_StudioSystem.instance.PlayOneShot("event:/FMODDécors et interrupteur/Marmite", gameObject.transform.position);
      break;
    case "Cadavre impact":
      FMOD_StudioSystem.instance.PlayOneShot("event:/FMODEnnemis/Cadavre impact", gameObject.transform.position);
      break;
    case "Clip canon":
      FMOD_StudioSystem.instance.PlayOneShot("event:/FMODTourelle et Canon/Clip canon", gameObject.transform.position);
      break;
    case "Bombe explosion":
      FMOD_StudioSystem.instance.PlayOneShot("event:/FMODTourelle et Canon/Bombe explosion", gameObject.transform.position);
      break;
        //Ennemis
    case "Ennemi standart mort":
      FMOD_StudioSystem.instance.PlayOneShot("event:/FMODEnnemis/Ennemi standart mort", gameObject.transform.position);
      break;
    case "Ennemi barak mort":
      FMOD_StudioSystem.instance.PlayOneShot("event:/FMODEnnemis/Ennemi barak mort", gameObject.transform.position);
      break;
    case "Ennemi ingenieur mort":
      FMOD_StudioSystem.instance.PlayOneShot("event:/FMODEnnemis/Ennemi ingenieur mort", gameObject.transform.position);
      break;
        //
    case "Ennemi standard spawn":
      FMOD_StudioSystem.instance.PlayOneShot("event:/FMODEnnemis/Ennemi standard spawn", gameObject.transform.position);
      break;
    case "Ennemi barak spawn":
      FMOD_StudioSystem.instance.PlayOneShot("event:/FMODEnnemis/Ennemi barak spawn", gameObject.transform.position);
      break;
    case "Ennemi ingenieur spawn":
      FMOD_StudioSystem.instance.PlayOneShot("event:/FMODEnnemis/Ennemi ingenieur spawn", gameObject.transform.position);
      break;
        //
    case "Ennemi ingenieur tir":
      FMOD_StudioSystem.instance.PlayOneShot("event:/FMODEnnemis/Ennemi ingenieur tir", gameObject.transform.position);
      break;
    case "Ennemi standard chasse":
      FMOD_StudioSystem.instance.PlayOneShot("event:/FMODEnnemis/Ennemi standard chasse", gameObject.transform.position);
      break;
    case "Ennemi barak chasse":
      FMOD_StudioSystem.instance.PlayOneShot("event:/FMODEnnemis/Ennemi barak chasse", gameObject.transform.position);
      break;
        //
    case "Ennemi barak blessure":
      FMOD_StudioSystem.instance.PlayOneShot("event:/FMODEnnemis/Ennemi barak blessure", gameObject.transform.position);
      break;


				default:
						break;
			//Camera.main.GetComponent<SoundManagerHeritTest>().PlaySoundOneShot("Ouglou tir");

		}
	}
}
