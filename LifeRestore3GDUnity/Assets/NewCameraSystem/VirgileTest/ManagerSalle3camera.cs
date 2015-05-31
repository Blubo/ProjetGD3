using UnityEngine;
using System.Collections;

public class ManagerSalle3camera : MonoBehaviour {

  public GameObject Camera;
  private DynamicCamera Dynamicbehavior;
  private NotSoDynamicCamera noDynamicCamera;

	// Use this for initialization
	void Start () {
   Dynamicbehavior =  Camera.GetComponent<DynamicCamera>();
   noDynamicCamera = Camera.GetComponent<NotSoDynamicCamera>();

	}
  void Activated()
  {
    Dynamicbehavior.enabled = false;
    noDynamicCamera.enabled = true;
  }
  void Deactivated()
  {
    Dynamicbehavior.enabled = true;
    noDynamicCamera.enabled = false;
  }
}
