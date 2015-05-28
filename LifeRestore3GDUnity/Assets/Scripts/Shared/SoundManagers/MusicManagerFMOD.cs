using UnityEngine;
using System.Collections;
using FMOD.Studio;

public class MusicManagerFMOD : MonoBehaviour
{

  public FMOD_StudioEventEmitter mainMusic, titleMusic, victoryMusic;
  [HideInInspector]
  public bool playing;
  [HideInInspector]
  public int step;

  private float Timer, Timertemp;

  [HideInInspector]
  [Range(0, 1)]
  private float beginParamMusic;

  // Use this for initialization
  void Start()
  {
    Timer = 7.0f;
    Timertemp = 0.0f;

    beginParamMusic = 0.01f;
   //   ChangeParamMainMusic(beginParamMusic);

    step = 0;
    mainMusic.Play();
    playing = true;

  }

  // Update is called once per frame
  void Update()
  {
    Timertemp += 1.0f *Time.deltaTime;
    if (Timertemp > Timer)
    {
      beginParamMusic += 0.10f;
      ChangeParamMainMusic(beginParamMusic);
      Timertemp = 0.0f;
    }
    if (beginParamMusic > 1)
    {
      beginParamMusic = 0.1f;
    }
    //  if(playing == true && musicDemonstration.getPlaybackState()==PLAYBACK_STATE.STOPPED){
    //   musicDemonstration.Stop();
    //
    //  }else if(playing == false && musicDemonstration.getPlaybackState()==PLAYBACK_STATE.PLAYING){
    //   musicDemonstration.Play();
    //
    //  }

    if (playing == false)
    {
      mainMusic.Stop();
    }
    else if (playing == true && mainMusic.getPlaybackState() == PLAYBACK_STATE.STOPPED)
    {
      mainMusic.Play();
    }
  }

  public void PlayMyMusic()
  {
    playing = !playing;

    //  music.GetComponent<FMOD_StudioEventEmitter>().Play();
  }

  public void PauseMyMusic()
  {
    //  music.GetComponent<FMOD_StudioEventEmitter>().Stop();
  }

  public void ChangeParamMainMusic(float valeur)
  {
    mainMusic.getParameter("Transition").setValue(valeur);
  }

  public void ChangeParamTitleMusic(float valeur)
  {
    //  music.getParameter("Transition").setValue(valeur);
  }


}