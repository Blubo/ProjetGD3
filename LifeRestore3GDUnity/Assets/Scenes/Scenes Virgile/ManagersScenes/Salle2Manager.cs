using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Salle2Manager : MonoBehaviour {

  public GameObject _Caserne1, _Caserne2;
  public List<GameObject> _Doors;

	private int Casernes;

  public Block_SpawnCollectible _RainingBonus;

	void Update () {

		if (_Caserne1 == null || _Caserne1.tag == "CaserneKO")
    {
			if(_Caserne2 == null || _Caserne2.tag == "CaserneKO" ){
				EndSalle();
			}
    }

		if ( _Caserne2 == null || _Caserne2.tag == "CaserneKO")
		{
			if( _Caserne1 == null || _Caserne1.tag == "CaserneKO" ){
				EndSalle();
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

  void OnTriggerEnter(Collider Col) {
    if (Col.gameObject.tag == "Player")
    {
      _Caserne1.GetComponent<Spawner>().enabled = true;
      _Caserne2.GetComponent<Spawner>().enabled = true;
    }
  }

  void Activated()
  {
    _RainingBonus.SpawnCollectible();
  }
}
