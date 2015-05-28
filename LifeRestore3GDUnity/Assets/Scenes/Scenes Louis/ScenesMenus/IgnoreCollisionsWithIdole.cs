using UnityEngine;
using System.Collections;

public class IgnoreCollisionsWithIdole : MonoBehaviour {

	[SerializeField]
	private GameObject idole;

	// Use this for initialization
	void Start () {
		Physics.IgnoreCollision(GetComponent<Collider>(), idole.GetComponent<Collider>());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
