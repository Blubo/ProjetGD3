using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Salle5Manager : MonoBehaviour {

  public List<GameObject> _ToDestroy, _Doors;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    if (_ToDestroy.Count == 0)
    {
      EndSalle();
    }
    for (int i = 0; i < _ToDestroy.Count; i++)
    {
      if (_ToDestroy[i].gameObject == null)
      {
        _ToDestroy.Remove(_ToDestroy[i]);
      }
    }
	}

  void EndSalle()
  {
    for (int i = 0; i < _Doors.Count; i++)
    {
      _Doors[i].SendMessage("Activated");
    }
  }
}
