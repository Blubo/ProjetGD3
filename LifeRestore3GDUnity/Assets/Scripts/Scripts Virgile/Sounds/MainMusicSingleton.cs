using UnityEngine;
using System.Collections;
using FMOD.Studio;

public class MainMusicSingleton : MonoBehaviour {

  private FMOD.Studio.EventInstance _MainMusic;
  private FMOD.Studio.ParameterInstance _MainMusicPara;

  public float TransitionFloat = 0.0f;

	void Start () {
    _MainMusic = FMOD_StudioSystem.instance.GetEvent("event:/FMODMusiques et ambiances/Musique finale");


     _MainMusic.start();

     _MainMusic.getParameter("Transition", out _MainMusicPara);
     _MainMusicPara.setValue(TransitionFloat);
	}
	
	void Update () {
    
	}
}
