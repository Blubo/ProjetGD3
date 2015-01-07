using UnityEngine;
using System.Collections;

public class LinkStrenght : MonoBehaviour {

	public int _LinkCommited;

	// Use this for initialization
	void Start () {
		_LinkCommited = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("link commited "+ _LinkCommited);
	}
}
