using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DynamicCamera : MonoBehaviour {

  private Camera MainCamera;

  //Players
  private GameObject[] _Players; 
  //Idole
  private GameObject _Idole;
  //Target 
  public Transform _Target, _Mini, _Medium, _High;
  //Distance entre un joueur le plus loin de l'idole
  private float _Distance;

	void Start () {
    MainCamera = Camera.main;
    _Idole = GameObject.FindGameObjectWithTag("Idole");
    _Players = GameObject.FindGameObjectsWithTag("Player");
	}


	void Update () {
    CalculateCenter(_Idole.transform, _Players[0].transform, _Players[1].transform, _Players[2].transform);
    FieldOfVision();
    CameraPosition();
	}

  void CameraPosition()
  {
   /* Transform tempPosition = null;
    if(GetFarthestElementFrom() <= 8){
      tempPosition = _Mini;
      MainCamera.fieldOfView = 75;
    }
    else if (GetFarthestElementFrom() <= 25)
    {
      tempPosition = _Medium;
      MainCamera.fieldOfView = 85;
    }
    else 
    {
      tempPosition = _High;
      MainCamera.fieldOfView = 90;
    }*/

    transform.position =  Vector3.Lerp(transform.position, new Vector3( _Target.transform.position.x,(_Mini.position.y+ GetFarthestElementFrom())* 1.3f, _Target.transform.position.z-10.0f),0.9f *Time.deltaTime);
  }

  void CalculateCenter(Transform A, Transform B, Transform C, Transform D)
  {
    Vector3 MidPoint = (A.position + B.position + C.position + D.position+A.position + A.position);
    MidPoint = MidPoint / 6;
    _Target.position = MidPoint;
  }

  void FieldOfVision()
  {
    if (MainCamera.transform.position.y <= 30)
    {
     // MainCamera.fieldOfView = Mathf.Lerp(MainCamera.fieldOfView, 60, 1.0f * Time.deltaTime) ;
    }
    else if (MainCamera.transform.position.y <= 50)
    {
     /// MainCamera.fieldOfView = Mathf.Lerp(MainCamera.fieldOfView, 60, 1.0f * Time.deltaTime);
    }
    else if (MainCamera.transform.position.y <= 60)
    {
      //MainCamera.fieldOfView = Mathf.Lerp(MainCamera.fieldOfView, 60, 1.0f * Time.deltaTime);
    }
  }

  float GetFarthestElementFrom()
  {
    float TempDistance = 0;
    GameObject _TheFarthest = null;

    for (int i = 0; i < _Players.Length; i++)
    {
      if (Vector3.Distance(_Idole.transform.position, _Players[i].transform.position)>TempDistance)
      {
        TempDistance = Vector3.Distance(_Idole.transform.position, _Players[i].transform.position);
        _Distance = Vector3.Distance(_Idole.transform.position, _Players[i].transform.position);
        _TheFarthest = _Players[i].transform.Find("Instantiateur").gameObject;
      }
    }
    return _Distance;
  }
}
