using UnityEngine;
using System.Collections;

public class AoEScript : MonoBehaviour {

	//le GO collidé est stocké là-dedans
	private GameObject _collided;
	private float _variation = 0.01f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//CECI SERT A CE QUE LE FUCKING TRIGGER MARCHE O_O j'avais envie de faire comme ca (surtout trop fatigué pr réfléchir à autre solution)
		//et le trigger ne sera detecté que si l'objet sur lequel on teste bouge. super.
		_variation*=-1;
		gameObject.transform.position+=new Vector3(0, _variation,0);


	}

	void OnTriggerStay(Collider collided){

		if(collided.gameObject.layer==8){
			if(collided.GetComponent<PlayerState>()!=null){
				collided.GetComponent<PlayerState>().v_myHP-=1;
			}
		}

	}
}
