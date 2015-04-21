using UnityEngine;
using System.Collections;

public class DistanceIs : MonoBehaviour {
	public GameObject obj1, obj2;

	// Use this for initialization
	void Start () {
		Debug.Log(Vector3.Distance(obj1.transform.position, obj2.transform.position));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
