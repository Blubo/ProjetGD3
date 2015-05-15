using UnityEngine;
using System.Collections;

public class EpineBehaviour : MonoBehaviour {

	private Renderer myRend;
	private Material myMat;
	private FadeObjectInOut myFader;
	// Use this for initialization
	void Start () {
		myFader = gameObject.GetComponent<FadeObjectInOut>();
		myRend = gameObject.GetComponent<Renderer>();
		myMat = gameObject.GetComponent<Material>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag.Equals("Ground")){
			if(gameObject.GetComponent<Rigidbody>()!=null) Destroy(gameObject.GetComponent<Rigidbody>());
			myFader.FadeOut();
		}
	}
}
