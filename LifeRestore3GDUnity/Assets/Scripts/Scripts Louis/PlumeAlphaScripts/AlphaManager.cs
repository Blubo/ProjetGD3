using UnityEngine;
using System.Collections;

public class AlphaManager : MonoBehaviour {

	[HideInInspector]
	public bool featherIsTaken = false;

	[HideInInspector]
	public GameObject posessor;

	public int scoreBonusFlat;

	private Vector3 initScale, heldScale;

	void Start(){
		initScale = gameObject.transform.localScale;
//		heldScale = initScale * 1f;

//		DontDestroyOnLoad(gameObject);
	}

	// Update is called once per frame
	void Update () {
		if(posessor != null){
			GetComponent<Collider>().isTrigger =true;
			gameObject.transform.position = posessor.transform.Find("Avatar/Body/AlphaHolder").transform.position;
			gameObject.transform.rotation = posessor.transform.Find("Avatar/Body/AlphaHolder").transform.rotation;
//			gameObject.transform.localScale = heldScale;
			gameObject.transform.parent = posessor.transform;

			gameObject.transform.localScale = initScale*4;

			transform.Find("plumeAlpha_rayon").GetComponent<ParticleSystem>().Stop();

		}else{
//			gameObject.transform.localScale = initScale;
			if(transform.Find("plumeAlpha_rayon").GetComponent<ParticleSystem>().isStopped == true){
				transform.Find("plumeAlpha_rayon").GetComponent<ParticleSystem>().Play();
			}
		}
	}
}
