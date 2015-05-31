using UnityEngine;
using System.Collections;

public class BombCanonColManager : MonoBehaviour {

	[HideInInspector]
	public GameObject v_CanonWhoShotMe;

	// Use this for initialization
	void Start () {
		Physics.IgnoreCollision(GetComponent<Collider>(), v_CanonWhoShotMe.GetComponent<Collider>());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag.Equals("Ground") == false && col.gameObject.tag.Equals("Canon") == false){
			GetComponent<BombBehavior>().TakeDamage(true);
			GetComponent<BombBehavior>()._Fuse = 0.75f;
		}
	}
}
