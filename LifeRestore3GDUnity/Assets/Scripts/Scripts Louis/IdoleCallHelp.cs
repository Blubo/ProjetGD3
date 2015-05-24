using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IdoleCallHelp : MonoBehaviour {

	private Sticky mySticky;
	[SerializeField]
	private float timeBeforeCall, dangerCallDuration, enemyRangeDetection;
	private float timer, dangerTimer;
	private Animator myLightAnimator;
	[SerializeField]
	private Light myLight;
	

	// Use this for initialization
	void Start () {
		myLightAnimator = myLight.GetComponent<Animator>();
		timer = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		timer+=Time.deltaTime;

//		if(mySticky.v_numberOfLinks == 0){
//
//		}
//
//		if(timer>timeBeforeCall){
//			CallHelp();
//		}

		Collider[] allCollidersInRange = Physics.OverlapSphere(transform.position, enemyRangeDetection);
		List<Collider> enemiesInRange = new List<Collider>();
		List<Collider> playersInRange = new List<Collider>();

		foreach (Collider col in allCollidersInRange) {
			if(col.gameObject.tag.Equals("Ennemy")){
				enemiesInRange.Add(col);
			}

			if(col.gameObject.tag.Equals("Player")){
				playersInRange.Add(col);
			}

		}

		if(enemiesInRange.Count != 0){
			Camera.main.GetComponent<SoundManagerHeritTest>().PlaySoundOneShot("Idole danger");
			if(myLightAnimator.GetBool("Danger") == false ){
				CallState("Danger", true);
				Debug.Log("once?");
			}
		}else{
			if(myLightAnimator.GetBool("Danger") == true ){
				CallState("Danger", false);
			}
			if(playersInRange.Count == 0){
				if(myLightAnimator.GetBool("Seul") == false ){
					CallState("Seul", true);
				}
			}else{
				if(myLightAnimator.GetBool("Seul") == true ){
					CallState("Seul", false);
				}
			}

		}

		//quand danger est vrai, on lance le timer de danger
//		if(myLightAnimator.GetBool("Danger") == true){
//			dangerTimer+=Time.deltaTime;
//		}
		//quand timer de danger est plus grand que le temps choisi, on met danger faux, et on reset timer de danger
//		if(dangerTimer>dangerCallDuration){
//			CallHelp();
//			dangerTimer=0;
//		}
	}

	public void CallState(string myString, bool myBool){
		myLightAnimator.SetBool(myString, myBool);
	}

	void OnDrawGizmos(){
		Gizmos.color = new Color32(232, 40, 40, 30);
		Gizmos.DrawSphere(transform.position, enemyRangeDetection);
	}
	
}
