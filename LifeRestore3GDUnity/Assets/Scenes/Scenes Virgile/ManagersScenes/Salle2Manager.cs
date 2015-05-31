using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Salle2Manager : MonoBehaviour {

  public GameObject _Caserne1, _Caserne2;
  public List<GameObject> _Doors;

  public Transform _Highview;
  public Camera _CameraMain;

  private bool ItsMyFirstTime = false;
  private int CountPlayers = 0;

	private int Casernes;

  public Block_SpawnCollectible _RainingBonus;

  void Start()
  {
    _CameraMain = Camera.main;
  }

	void Update () {

		if (_Caserne1 == null || _Caserne1.tag == "CaserneKO")
    {
			if(_Caserne2 == null || _Caserne2.tag == "CaserneKO" ){
        if (ItsMyFirstTime == false)
        {
          EndSalle();
        }
			}
    }

		if ( _Caserne2 == null || _Caserne2.tag == "CaserneKO")
		{
			if( _Caserne1 == null || _Caserne1.tag == "CaserneKO" ){
        if (ItsMyFirstTime == false)
        {
          EndSalle();
        }
			}
		}
	}

  void EndSalle()
  {
    for (int i = 0; i < _Doors.Count; i++)
    {
      _Doors[i].SendMessage("Activated");
    }
    ItsMyFirstTime = true;
  }

  void OnTriggerEnter(Collider Col) {
    if (Col.gameObject.tag == "Player")
    {
      CountPlayers += 1;
      _Caserne1.GetComponent<Spawner>().enabled = true;
      _Caserne2.GetComponent<Spawner>().enabled = true;
    }
    if (CountPlayers == 3)
    {
      _CameraMain.GetComponent<DynamicCamera>()._LockedCamera = true;
      _CameraMain.GetComponent<DynamicCamera>()._LockTransform = _Highview;
      //_CameraMain.transform.position = _Highview.transform.position;
    }
  }

  void Activated()
  {
    _RainingBonus.SpawnCollectible();
  }
}
