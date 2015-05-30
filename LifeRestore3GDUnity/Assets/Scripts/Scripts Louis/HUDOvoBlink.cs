using UnityEngine;
using System.Collections;

public class HUDOvoBlink : MonoBehaviour {
	
	[SerializeField]
	private float timer;
	private float internalTimer, randomTimer;
	private bool blinked = false;
	public GameObject target;

	// Use this for initialization
	void Start () {
		randomTimer = timer+Random.Range(-timer*0.5f, timer*0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		if(blinked == false){
			internalTimer+=Time.deltaTime;
			
			if(internalTimer>=timer){
				blinked = true;
				randomTimer = timer+Random.Range(-0.5f*timer, 0.5f*timer);
				internalTimer = 0f;
				target.SetActive(true);
			}
		}else{
			internalTimer+=Time.deltaTime;
			if(internalTimer>=0.10f){
//				Debug.Log("fdp");
				target.SetActive(false);
				internalTimer=0f;
				blinked = false;
			}
		}	
	}
}
