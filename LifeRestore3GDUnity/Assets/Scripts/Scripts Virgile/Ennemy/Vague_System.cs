using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Vague_System : MonoBehaviour {

  private int _CurrentVague;
  public List<Transform> _Emplacement;
  public Transform _BossPlacement;

  public GameObject _Prefab1, _Prefab2, _Prefab3;

  public float _TimerBetween;
  private float _TimerMax;

  public List<GameObject> _CloseThemAll, _OpenThem;

  void Activated()
  {
    this.enabled = true;
  }

	void Awake () {
    _TimerMax = 0.0f;
    _CurrentVague = 0;
	}
	

	void Update () {
    _TimerMax -= 1 * Time.deltaTime;

    if (_CurrentVague >= 4 && GameObject.FindWithTag("Ennemy") == null)
    {
      EndSalle();
    }

    if(_TimerMax <= 0.0f){
      if(_CurrentVague < 4){
        _TimerMax = _TimerBetween;
        PopNextVague();
      }
    }
	}

  void PopNextVague()
  {
    switch (_CurrentVague)
    {
      case 0:
        for (int i = 0; i < _Emplacement.Count; i++)
			{
        Instantiate(_Prefab1, _Emplacement[i].position, Quaternion.identity);
//        Instantiate(_Prefab1, _Emplacement[i].position, Quaternion.identity);
			}
        _CurrentVague += 1;
        break;

      case 1:
        for (int i = 0; i < _Emplacement.Count; i++)
        {
//          Instantiate(_Prefab2, _Emplacement[i].position, Quaternion.identity);
          Instantiate(_Prefab2, _Emplacement[i].position, Quaternion.identity);
        }
        _CurrentVague += 1;
        break;

      case  2 :
        for (int i = 0; i < _Emplacement.Count; i++)
        {
//          Instantiate(_Prefab2, _Emplacement[i].position, Quaternion.identity);
//          Instantiate(_Prefab2, _Emplacement[i].position, Quaternion.identity);

//          Instantiate(_Prefab1, _Emplacement[i].position, Quaternion.identity);
          Instantiate(_Prefab1, _Emplacement[i].position, Quaternion.identity);
        }
//          Instantiate(_Prefab2, _Emplacement[3].position, Quaternion.identity);
          Instantiate(_Prefab2, _Emplacement[5].position, Quaternion.identity);
        _CurrentVague += 1;
        break;

      case 3:
//        Debug.Log("456");
        Instantiate(_Prefab3, _BossPlacement.position, Quaternion.identity);
          _CurrentVague += 1;
        break;
    }
  }

  void EndSalle()
  {
    for (int i = 0; i < _CloseThemAll.Count; i++)
    {
      _CloseThemAll[i].SendMessage("Deactivated");
    }

    for (int i = 0; i < _OpenThem.Count; i++)
    {
      _OpenThem[i].SendMessage("Activated");
    }
  }
}
