using UnityEngine;
using System.Collections;

public class NotSoDynamicCamera : MonoBehaviour {

  private Camera MainCamera;
  public Transform _LockTransform;


	void Start () {
    //transform.position = _LockTransform.position;
	}

  void Update()
  {
    if (transform.position != _LockTransform.position)
    {
      transform.position = Vector3.Lerp(transform.position, _LockTransform.position, 1.0f * Time.deltaTime);
    }
  }
}
