using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManagerSlide : MonoBehaviour {

  private GameObject _MainCamera;

  public List<Transform> _EmplacementsCam, _Salle;

  void Start()
  {
    _MainCamera = Camera.main.gameObject;
    _MainCamera.transform.position = _EmplacementsCam[0].position;
    _MainCamera.transform.rotation = _EmplacementsCam[0].rotation;
  }
}
