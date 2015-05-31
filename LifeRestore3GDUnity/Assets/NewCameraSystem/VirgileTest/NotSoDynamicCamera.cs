using UnityEngine;
using System.Collections;

public class NotSoDynamicCamera : MonoBehaviour {

  private Camera MainCamera;
  public Transform _LockTransform;


	void Start () {
    transform.position = _LockTransform.position;
	}
}
