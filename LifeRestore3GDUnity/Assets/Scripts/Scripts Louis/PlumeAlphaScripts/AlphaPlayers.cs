using UnityEngine;
using System.Collections;

public class AlphaPlayers : MonoBehaviour {

	public GameObject plumeAlpha;
	private AlphaManager _alphaManager;
	private bool imTheAlpha;
	[SerializeField]
	private float ejectionForce;
	private Player_Status myPlayerStatus;
	private GameObject featherToHide;

	// Use this for initialization
	void Start () {
		featherToHide = transform.Find("Avatar/Body/Plume004").gameObject;
//		featherToHide = transform.Find("Avatar/Box964").gameObject;

		myPlayerStatus = gameObject.GetComponent<Player_Status>();
    FindPlume();
	}

  public void FindPlume()
  {
    plumeAlpha = GameObject.Find("PlumeAlpha");
    if (plumeAlpha != null)
    {
      _alphaManager = plumeAlpha.GetComponent<AlphaManager>();
    }
  }

	// Update is called once per frame
	void Update () {
		if(plumeAlpha != null){
			Physics.IgnoreCollision(plumeAlpha.GetComponent<Collider>(), GetComponent<Collider>(), myPlayerStatus._IsInvincible);
		}
		featherToHide.GetComponent<MeshRenderer>().enabled = !imTheAlpha;


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
