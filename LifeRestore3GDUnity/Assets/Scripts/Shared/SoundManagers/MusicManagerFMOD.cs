using UnityEngine;
using System.Collections;
using FMOD.Studio;

public class MusicManagerFMOD : MonoBehaviour {

	public FMOD_StudioEventEmitter mainMusic, titleMusic, victoryMusic;
	[HideInInspector]
	public bool playing;
	[HideInInspector]
	public int step;

	[HideInInspector]
	[Range(0,1)]
	private float beginParamMusic;

	// Use this for initialization
	void Start () {
		beginParamMusic = 0.41f;
		ChangeParamMainMusic(beginParamMusic);
		step = 0;
		mainMusic.Play();
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
			mainMusic.Stop();	
		}else if(playing == true && mainMusic.getPlaybackState()==PLAYBACK_STATE.STOPPED){
			mainMusic.Play();
		}
	}

	public void PlayMyMusic(){
		playing =! playing;

//		music.GetComponent<FMOD_StudioEventEmitter>().Play();
	}

	public void PauseMyMusic(){
//		music.GetComponent<FMOD_StudioEventEmitter>().Stop();
	}

	public void ChangeParamMainMusic(float valeur){
		mainMusic.getParameter("Transition").setValue(valeur);
	}

	public void ChangeParamTitleMusic(float valeur){
//		music.getParameter("Transition").setValue(valeur);
	}


}
