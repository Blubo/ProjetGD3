using UnityEngine;
using System.Collections;

public class BecomeMovable : MonoBehaviour {

  public GameObject _NewBloc;

  void Activated()
  {
    gameObject.GetComponent<BoxCollider>().enabled = false;
    _NewBloc.transform.position = gameObject.transform.position;
    Destroy(gameObject);
  }
}
