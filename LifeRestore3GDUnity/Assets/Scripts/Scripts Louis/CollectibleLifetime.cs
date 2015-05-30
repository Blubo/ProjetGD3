using UnityEngine;
using System.Collections;

public class CollectibleLifetime : MonoBehaviour {

	[SerializeField]
	private float lifeTime;
	private float internalTimer;
	public FadeObjectInOut myFader;
	private bool fading;


	// Use this for initialization
	void Start () {
		internalTimer = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		internalTimer+=Time.deltaTime;

		if(fading == false){
			if(internalTimer >= lifeTime){
				Debug.Log("zrgt");
				internalTimer = 0f;
				myFader.FadeOut();
				fading = true;
			}
		}else{
			if(internalTimer >= myFader.fadeTime){
				Destroy(gameObject);
			}
		}
	}
}
