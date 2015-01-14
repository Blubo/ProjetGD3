using UnityEngine;
using System.Collections;

public class SoundCollisionBlock : MonoBehaviour {

	public AudioClip v_blockCollisionWithBlock;
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
			if(collision.gameObject.tag.Equals("Block")){
				audio.PlayOneShot(v_blockCollisionWithBlock);
//				Debug.Log("gameObject name is "+gameObject.name);
			}
		}
	}
}
