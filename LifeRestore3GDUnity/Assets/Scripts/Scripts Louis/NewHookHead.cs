using UnityEngine;
using System.Collections;

public class NewHookHead : MonoBehaviour {

	public GameObject GrappedTo;
	private bool _HaveChild;

	[HideInInspector]
	public GameObject _myShooter;
	[HideInInspector]
	public int howWasIShot;
	private Vector3 _myShooterInitPos, _myShooterPos;
	public float v_returnDistance, v_returnSpeedConst;
	private bool shouldIReturn=false;

	void Start(){
		_HaveChild = false;
		if(_myShooter!=null){
			_myShooterInitPos=_myShooter.transform.position;
		}
	} 

	void OnTriggerEnter(Collider _Collided){
		//????
		//PKOI LINK COMMITED AUGMENTE??
		Debug.Log("i touched "+_Collided.gameObject.name);

		//Si un objet est touché alors on fait un truc selon sont type et ses propriétés
	//	if (_Collided.gameObject.tag != "Player") { // faudrait pas que ce soit le joueur
		//pas le joueur; pas le grappin d'un autre
		if(_Collided.gameObject != _myShooter && _Collided.gameObject.name!="NewHookhead" && _Collided.gameObject.name!="B 5Janv"){

			if (_HaveChild == false){
				gameObject.rigidbody.velocity = Vector3.zero;
				//_myShooter.GetComponent<NewShoot>()._Hookheadplaced=true;
//				if(howWasIShot==1)_myShooter.GetComponent<NewShoot>()._target=_Collided.gameObject;
//				if(howWasIShot==2)_myShooter.GetComponent<NewShoot>()._target1=_Collided.gameObject;

				_myShooter.GetComponent<NewShoot>()._target=_Collided.gameObject;
				GrappedTo = _Collided.gameObject;
				if(_Collided.gameObject.tag == "Player"){
					LinkStrenght _Linkcommited = _Collided.gameObject.GetComponent<LinkStrenght>();
					_Linkcommited._LinkCommited += 1;

				}
				_HaveChild = true;
			}
		}
		//}

		if(_Collided.gameObject==_myShooter){
			if(shouldIReturn==true){
//				Destroy(gameObject.transform.Find("B 5Janv"));
				_myShooter.GetComponent<NewShoot>().Invoke("DetachChildren",0);
				Destroy(_myShooter.GetComponent<NewShoot>()._myHook);
				Destroy(gameObject);
			}
		}
	}

//	void OnCollisionEnter(Collision collision){
//		if(collision.gameObject==_myShooter){
//			if(shouldIReturn==true){
//				Destroy(gameObject.transform.Find("B 5Janv"));
//				Destroy(gameObject);
//				Debug.Log("kill that motherfucker");
//			}
//		}
//	}

	void Update (){
		// Si la tete de grappin a choppé quelque chose alors cette chose 
		if (GrappedTo != null) {
			ComeWithMe();
		}

		if(_myShooter!=null){
			
			_myShooterPos=_myShooter.transform.position;
			
			//15
			//on n'enclenche/ne permet le retour que si la tete du grappin n'est pas posée
			if (GrappedTo == null) {
				if(Vector3.Distance(gameObject.transform.position, _myShooterInitPos)>=v_returnDistance){
					shouldIReturn=true;
				}
				
				if(shouldIReturn==true){
					//shouldIReturn=false;
					gameObject.rigidbody.velocity = Vector3.zero;
//					gameObject.rigidbody.AddForce((_myShooterPos-gameObject.transform.position)*_myShooter.GetComponent<ShootHookLouis5Janv>()._SpeedBullet*v_returnSpeedConst);
					gameObject.rigidbody.AddForce((_myShooterPos-gameObject.transform.position)*_myShooter.GetComponent<NewShoot>()._SpeedBullet*v_returnSpeedConst);

				}
				
				//ce 4 est rentré en dur, et correspond au rayon de l'avatar, soit quand détruire le lien quand il revient.
				//SWITCH THE COMMENTS ON THE TWO NEXT LINES FOR MANAGING THE WIDTH OF THE AVATAR
				if(Vector3.Distance(gameObject.transform.position, _myShooterPos)<=0.1f && shouldIReturn==true){
//				if(Vector3.Distance(gameObject.transform.position, _myShooterPos)<=4f && shouldIReturn==true){
					_myShooter.GetComponent<NewShoot>().Invoke("DetachChildren",0);
					Destroy(gameObject);
				}
			}
		}
	}

	void ComeWithMe(){
		GrappedTo.transform.parent = transform;
	}
}
