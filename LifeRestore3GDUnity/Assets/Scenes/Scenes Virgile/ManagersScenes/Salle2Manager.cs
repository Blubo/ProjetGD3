using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Salle2Manager : MonoBehaviour {

  public GameObject _Caserne1, _Caserne2;
  public List<GameObject> _Doors;

  public Block_SpawnCollectible _RainingBonus;

	void Update () {
    if (_Caserne1.tag == "CaserneKO" && _Caserne2.tag == "CaserneKO")
    {
      EndSalle();
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

      _RainingBonus.SpawnCollectible();
    }
  }
}
