using UnityEngine;
using System.Collections;
using FMOD.Studio;

public class MusicManagerFMOD : MonoBehaviour {

	public FMOD_StudioEventEmitter musicTest, musicDemonstration;
	[HideInInspector]
	public bool playing;

	// Use this for initialization
	void Start () {
		musicDemonstration.Play();
		playing=true;

	}
	
	// Update is called once per frame
	void Update () {
//		if(playing == true && musicDemonstration.getPlaybackState()==PLAYBACK_STATE.STOPPED){
//			musicDemonstration.Stop();
//
//		}else if(playing == false && musicDemonstration.getPlaybackState()==PLAYBACK_STATE.PLAYING){
//			musicDemonstration.Play();
//
//		}

		if(playing==false){
			musicDemonstration.Stop();	
		}else if(playing == true && musicDemonstration.getPlaybackState()==PLAYBACK_STATE.STOPPED){
			musicDemonstration.Play();
		}

	}

	public void PlayMyMusic(){
		playing =! playing;

//		music.GetComponent<FMOD_StudioEventEmitter>().Play();
	}

	public void PauseMyMusic(){
//		music.GetComponent<FMOD_StudioEventEmitter>().Stop();
	}

	public void ChangeParam(FMOD_StudioEventEmitter music, float valeur){
		music.getParameter("Transition").setValue(valeur);
	}


}
