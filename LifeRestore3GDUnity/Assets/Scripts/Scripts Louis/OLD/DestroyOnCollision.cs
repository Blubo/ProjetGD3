using UnityEngine;
using System.Collections;

public class DestroyOnCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter(Collision _coll) {
		if (_coll.gameObject.layer == 18 || _coll.gameObject.layer == 19) {
			Debug.Log("at");
			Destroy(gameObject);
		}
	}

}
