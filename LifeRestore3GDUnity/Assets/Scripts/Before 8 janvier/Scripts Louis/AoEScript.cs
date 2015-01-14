using UnityEngine;
using System.Collections;

public class AoEScript : MonoBehaviour {

	//le GO collidé est stocké là-dedans
	private GameObject _collided;
	//private float _variation = 0.01f;
	private float _stayingTime=3f;
	private Vector3 _RandomPos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//CECI SERT A CE QUE LE FUCKING TRIGGER MARCHE O_O j'avais envie de faire comme ca (surtout trop fatigué pr réfléchir à autre solution)
		//et le trigger ne sera detecté que si l'objet sur lequel on teste bouge. super.
		//_variation*=-1*Time.deltaTime;

		//bouge au bout de 3s
		_stayingTime-=1*Time.deltaTime;

		if(_stayingTime<=0){
			_stayingTime=3f;
			gameObject.transform.position = new Vector3(Random.Range(-8, 8), gameObject.transform.position.y, Random.Range(-8, 8));
			//gameObject.transform.position+=new Vector3(0, _variation,0);

		}
	}

	//need rajouter des RB sur les joueurs si on veut pas que les collisions déconnent
	void OnTriggerStay(Collider collided){
		if(collided.gameObject.layer==8){
			if(collided.GetComponent<PlayerState>()!=null){
				collided.renderer.material.color=Color.red;
				collided.GetComponent<PlayerState>().v_myHP-=1;
			}
		}
	}
	
	void OnTriggerExit(Collider collided){
		if(collided.gameObject.layer==8){
			if(collided.GetComponent<PlayerState>()!=null){
				collided.renderer.material.color=Color.blue;
			}
		}
	}
}
