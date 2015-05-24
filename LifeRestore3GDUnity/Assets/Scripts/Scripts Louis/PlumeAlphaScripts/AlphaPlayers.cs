using UnityEngine;
using System.Collections;

public class AlphaPlayers : MonoBehaviour {

	private GameObject plumeAlpha;
	private AlphaManager _alphaManager;
	private bool imTheAlpha;
	[SerializeField]
	private float ejectionForce;
	private Player_Status myPlayerStatus;


	// Use this for initialization
	void Start () {
		myPlayerStatus = gameObject.GetComponent<Player_Status>();
		plumeAlpha = GameObject.Find("PlumeAlpha");
		if(plumeAlpha != null){
			_alphaManager = plumeAlpha.GetComponent<AlphaManager>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		Physics.IgnoreCollision(plumeAlpha.GetComponent<Collider>(), GetComponent<Collider>(), myPlayerStatus._IsInvincible);
	}

	void OnCollisionEnter(Collision col){
		if(myPlayerStatus._IsInvincible == false){
			if(col.gameObject == plumeAlpha){
				if(_alphaManager.featherIsTaken == false){

					_alphaManager.posessor = gameObject;
					_alphaManager.featherIsTaken = true;
					imTheAlpha = true;
				}
			}
		}
	}

	public void DropTheFeather(Vector3 direction){
		if(imTheAlpha == true){
			plumeAlpha.GetComponent<Collider>().isTrigger = false;
			plumeAlpha.GetComponent<Rigidbody>().AddForce(ejectionForce*direction.normalized);
			plumeAlpha.transform.parent = null;

			_alphaManager.featherIsTaken = false;
			_alphaManager.posessor = null;
		}
		imTheAlpha = false;
	}
}
