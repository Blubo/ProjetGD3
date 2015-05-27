using UnityEngine;
using System.Collections;

public class DestroyOnActivated : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
  void Activated()
  {
    Destroy(gameObject);
  }
}
