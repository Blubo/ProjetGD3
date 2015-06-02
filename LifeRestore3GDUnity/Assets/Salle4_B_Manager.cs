using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Salle4_B_Manager : MonoBehaviour {

  public List<BasicEnnemy> _EnnemiesToKill;
  public List<GameObject> _Doors;

	void Start () {
	
	}

  void EndSalle()
  {
    OpenDoors();
  }

	void Update () {
    for (int i = 0; i < _EnnemiesToKill.Count; i++)
    {
      if (_EnnemiesToKill[i] == null)
      {
        _EnnemiesToKill.Remove(_EnnemiesToKill[i]);
      }
    }

    if (_EnnemiesToKill.Count == 0)
    {
      EndSalle();
    }
	}

  void OpenDoors()
  {
    for (int i = 0; i < _Doors.Count; i++)
    {
      _Doors[i].SendMessage("Activated");
    }
  }
}
