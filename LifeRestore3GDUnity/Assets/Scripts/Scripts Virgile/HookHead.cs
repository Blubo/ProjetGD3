using UnityEngine;
using System.Collections;

public class HookHead : MonoBehaviour {

	public GameObject GrappedTo;
	private bool _HaveChild;

	void Start(){
		_HaveChild = false;
	} 

	void OnTriggerEnter(Collider _Collided){
		//Si un objet est touché alors on fait un truc selon sont type et ses propriétés
	//	if (_Collided.gameObject.tag != "Player") { // faudrait pas que ce soit le joueur
		if (_HaveChild == false){
			GrappedTo = _Collided.gameObject;
			if(_Collided.gameObject.tag == "Player"){
				LinkStrenght _Linkcommited = _Collided.gameObject.GetComponent<LinkStrenght>();
				_Linkcommited._LinkCommited += 1;
			}
			_HaveChild = true;
		}
		//}
	}

	void Update (){
		// Si la tete de grappin à choper quelque chose alors cette chose 
		if (GrappedTo != null) {
			ComeWithMe();
		}
	}

	void ComeWithMe(){
		//By physique
		/*FixedJoint _joint = GetComponent<FixedJoint>();
		_joint.connectedBody = GrappedTo.rigidbody;*/

		//By script 
		GrappedTo.transform.parent = transform;
	}
}
