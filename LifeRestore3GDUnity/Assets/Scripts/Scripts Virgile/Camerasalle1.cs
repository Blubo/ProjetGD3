using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Camerasalle1 : MonoBehaviour {

  public GameObject Camera;
  public List<Transform> _Waypoints;

  private int CountPlayers;

	// Update is called once per frame
	void Update () {
    if (CountPlayers > 0 && CountPlayers < 3)
    {
      Camera.transform.position = Vector3.Lerp(Camera.transform.position, _Waypoints[1].position, 1.0f*Time.deltaTime);
    }
    else if (CountPlayers == 3)
    {
      Camera.transform.position = Vector3.Lerp(Camera.transform.position, _Waypoints[2].position, 1.0f * Time.deltaTime);
    }else
    {
      Camera.transform.position = Vector3.Lerp(Camera.transform.position, _Waypoints[0].position, 1.0f * Time.deltaTime);
    }
	}

  void OnTriggerEnter(Collider col)
  {
    if (col.gameObject.tag == "Player")
    {
      CountPlayers += 1;
    }
  }

  void OnTriggerExit(Collider col)
  {
    if (col.gameObject.tag == "Player")
    {
      CountPlayers -= 1;
    }
  }
}
