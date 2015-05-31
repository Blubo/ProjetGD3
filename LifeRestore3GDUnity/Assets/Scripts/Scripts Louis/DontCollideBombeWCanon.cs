using UnityEngine;
using System.Collections;

public class DontCollideBombeWCanon : MonoBehaviour {

	[HideInInspector]
	public GameObject v_CanonWhoShotMe;

	// Use this for initialization
	void Start () {
		Physics.IgnoreCollision(GetComponent<Collider>(), v_CanonWhoShotMe.GetComponent<Collider>());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
