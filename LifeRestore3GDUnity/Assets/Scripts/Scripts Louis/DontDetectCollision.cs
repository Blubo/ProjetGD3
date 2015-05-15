using UnityEngine;
using System.Collections;

public class DontDetectCollision : MonoBehaviour {

	[SerializeField]
	[Tooltip("GameObject to ignore collisions with")]
	private GameObject ignored;

	// Use this for initialization
	void Start () {
		Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), ignored.GetComponent<Collider>());

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
