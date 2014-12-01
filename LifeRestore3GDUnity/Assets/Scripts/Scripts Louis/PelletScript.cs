using UnityEngine;
using System.Collections;

public class PelletScript : MonoBehaviour {

	private float _lifeTime=3f;

	[HideInInspector]
	public GameObject v_whoShotMe;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		_lifeTime -= 1 *Time.deltaTime;

		if(_lifeTime<=0f){
			Destroy(gameObject);
		}
	}


}
