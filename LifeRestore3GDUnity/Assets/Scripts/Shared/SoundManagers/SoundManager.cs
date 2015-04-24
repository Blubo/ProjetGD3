using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;

public class SoundManager : MonoBehaviour {

	[Tooltip("Foutre les StudioEventEmitters ici")]
	[SerializeField]
	private List<FMOD_StudioEventEmitter> _emitters;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	public virtual void PlaySound(int i){
		if(_emitters[i].getPlaybackState() == PLAYBACK_STATE.STOPPED){
			_emitters[i].Play();
		}
	}

	public virtual void PlaySoundOneShotCustom(int i){
		if(_emitters[i].getPlaybackState() == PLAYBACK_STATE.PLAYING){
			_emitters[i].Stop();
			_emitters[i].Play();
		}
	}

	public virtual void PlaySoundOneShot(string text){
		
	}

}
