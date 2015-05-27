using UnityEngine;
using System.Collections;

public class SoundCollisions : MonoBehaviour {

  public string _Name;
  public int _Priority;

  private SoundManagerHeritTest _Sounds;

  void Awake()
  {
    _Sounds = Camera.main.GetComponent<SoundManagerHeritTest>();
  }

  private void OnCollisionEnter(Collision col)
  {
    switch (_Name)
    {
      case "Impact Pierre":
        _Sounds.PlaySoundOneShot("Impact Pierre");
        break;

      case "Barriere impact":
        _Sounds.PlaySoundOneShot("Barriere impact");
        break;

      case "Marmite":
        _Sounds.PlaySoundOneShot("Marmite");
        break;

      case "Cadavre impact":
        _Sounds.PlaySoundOneShot("Cadavre impact");
      break;

      case "Bloc bois dommage":
      _Sounds.PlaySoundOneShot("Bloc bois dommage");
      break;
    }
  }
}
