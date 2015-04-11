using UnityEngine;
using System.Collections;

public class ElevateFormGround : MonoBehaviour {

  public Transform _GetToTpoint;
  private bool _moved;

	// Use this for initialization
	void Start () {
    _moved = false;
    if (gameObject.GetComponent<BoxCollider>() != null)
    {
      gameObject.GetComponent<BoxCollider>().enabled = false;

    }
	}
	
	// Update is called once per frame
	void Update () {
    if (_moved == true)
    {
      transform.position = Vector3.Lerp(transform.position, _GetToTpoint.position, 1 * Time.deltaTime);
      if (gameObject.GetComponent<BoxCollider>()!=null)
      {
        gameObject.GetComponent<BoxCollider>().enabled = false;

      }
      if (Vector3.Distance(transform.position, _GetToTpoint.position )< 0.5f)
      {
        if (gameObject.GetComponent<BoxCollider>() != null)
        {
          gameObject.GetComponent<BoxCollider>().enabled = true;
          gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
        
        _moved = false;
      }
    }
	}

  void Activated()
  {
    _moved = true;
  }
}
