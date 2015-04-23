using UnityEngine;
using System.Collections;
using FMOD.Studio;

public class MusicManagerFMOD : MonoBehaviour {

	[SerializeField]
	private FMOD_StudioEventEmitter music;

	// Use this for initialization
	void Start () {
		music.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
