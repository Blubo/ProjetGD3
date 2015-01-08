using UnityEngine;
using System.Collections;

public class HookHead : MonoBehaviour {
	
	//L'objet auquel est attachée la tete
	public GameObject GrappedTo;
	private Vector3 _PlayerPosition;
	
	void Start(){
		
	} 
	
	void OnTriggerEnter(Collider _Collided){
		//Si un objet est touché alors le grappin en prend connaissance
		if(GrappedTo == null && _Collided.gameObject.tag != "HookHead"){
			GrappedTo = _Collided.gameObject;
			if(GrappedTo.rigidbody == null){
				GrappedTo.AddComponent<Rigidbody>();
				GrappedTo.rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
			}
			if(GrappedTo.gameObject.tag == "Player"){
				//link commited +1
			}
		}
	}
	
	void Update (){
		//si l'objet attaché est un joueur alors le grappin va "suivre" ses mouvements
		if(GrappedTo != null){
			//if(GrappedTo.gameObject.tag == "Player"){
			_PlayerPosition = GrappedTo.transform.position;
			transform.position = _PlayerPosition;
			//}
		}
	}
}
