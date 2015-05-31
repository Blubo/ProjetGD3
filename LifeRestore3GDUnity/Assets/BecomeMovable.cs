using UnityEngine;
using System.Collections;

public class BecomeMovable : MonoBehaviour {

  public GameObject _NewBloc;
  private Vector3 _Placement;

  void Start()
  {
    _Placement = gameObject.transform.position - Vector3.up * 5; ;
  }

  void Activated()
  {
    gameObject.GetComponent<BoxCollider>().enabled = false;
    _NewBloc.transform.position = gameObject.transform.position;
    _NewBloc.GetComponent<Rigidbody>().useGravity = true;
    //Destroy(gameObject);
    gameObject.transform.position = Vector3.Lerp(transform.position, _Placement, 4.0f * Time.deltaTime);
  }
}
