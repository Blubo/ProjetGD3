using UnityEngine;
using System.Collections;

public class SoundCollisionFrondeFMOD : MonoBehaviour {

	private float _silenceTimer;

	// Use this for initialization
	void Start () {
		_silenceTimer=0f;
	
	}
	
	// Update is called once per frame
	void Update () {
		_silenceTimer+=Time.deltaTime;
	
	}

	void OnCollisionEnter(Collision collision){
		if(_silenceTimer>0.5f){
//			if(collision.gameObject.layer.Equals("CanBreak")){
//			if(collision.gameObject.layer == 17){

			Camera.main.GetComponent<SoundManagerHeritTest>().PlaySoundOneShot("Impact bloc fronde");

//			}
		}
	}
}
