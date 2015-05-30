using UnityEngine;
using System.Collections;

public class HUDFruitShine : MonoBehaviour {

	[SerializeField]
	private float timer;
	private float internalTimer, randomTimer;
	private bool shined = false;
	private Animator myAnimator;

	// Use this for initialization
	void Start () {
		myAnimator = GetComponent<Animator>();
		randomTimer = timer+Random.Range(-timer*0.5f, timer*0.5f);

	}
	
	// Update is called once per frame
	void Update () {
		if(shined == false){
			internalTimer+=Time.deltaTime;

			if(internalTimer>=timer){
				shined = true;
//				randomTimer = timer+Random.Range(-timer*0.5f, timer*0.5f);
				internalTimer = 0f;
				myAnimator.SetTrigger("DevilTrigger");
			}
		}else{
			internalTimer+=Time.deltaTime;
			if(internalTimer>=0.10f){
//				Debug.Log("stop");
				internalTimer=0f;
				shined = false;
			}
		}	
	}
}
