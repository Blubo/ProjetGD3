using UnityEngine;
using System.Collections;

public class FreezeOnGround : MonoBehaviour {
	[SerializeField]
	private float timerBeforeFreezeMin, timerBeforeFreezeMax;
	private float internalTimer, timerBeforeFreezeRandom;
	private bool touchedGround, destroyedRB = false;
	// Use this for initialization
	void Start () {
		timerBeforeFreezeRandom = Random.Range(timerBeforeFreezeMin, timerBeforeFreezeMax);
	}
	
	// Update is called once per frame
	void Update () {
		if(touchedGround == true){
			internalTimer+=Time.deltaTime;
			if(internalTimer>timerBeforeFreezeRandom){
				if(destroyedRB==false){
//					Destroy(gameObject.GetComponent<Rigidbody>());
					gameObject.GetComponent<Rigidbody>().isKinematic = true;
					gameObject.GetComponent<Collider>().isTrigger = true;
					destroyedRB = true;
				}
			}
		}
	}
	
	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag.Equals("Ground") || col.gameObject.tag.Equals("Static")){
			if(gameObject.GetComponent<Rigidbody>()!=null){
				touchedGround = true;
//				Debug.Log("collision ground");
//				Destroy(gameObject.GetComponent<Rigidbody>());
			}
		}
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag.Equals("Ground") || col.gameObject.tag.Equals("Static")){
			if(gameObject.GetComponent<Rigidbody>()!=null){
				touchedGround = true;
//				Debug.Log("trigger ground");

//				Destroy(gameObject.GetComponent<Rigidbody>());
			}
		}
	}
}
