using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManagerLvl : MonoBehaviour {

  private GameObject[] _CheckPlumeAlpha;
  [HideInInspector]
  public bool _AlphaFound = false;

  private void Start(){
   _CheckPlumeAlpha =  GameObject.FindGameObjectsWithTag("Player");
  }

  public void CheckForThings()
  {
    if (!_AlphaFound)
    {
      for (int i = 0; i < _CheckPlumeAlpha.Length; i++)
      {
        _CheckPlumeAlpha[i].GetComponent<AlphaPlayers>().FindPlume();

        if (_CheckPlumeAlpha[i].GetComponent<AlphaPlayers>().plumeAlpha != null)
        {
          _AlphaFound = true;
        }
      }   
    }
  }
}
