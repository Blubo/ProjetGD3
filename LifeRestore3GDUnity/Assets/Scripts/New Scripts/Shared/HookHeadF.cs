using UnityEngine;
using System.Collections;

public class HookHeadF : MonoBehaviour {
	//ce script est mis sur la tete du grappin
	//grappedTo=L'objet sur lequel s'est fixée la tete
	//myShooter=celui qui a tiré ce lien
	//how i was shot=quelle gachette a crée ce lien

	//L'objet auquel est attachée la tete
	[HideInInspector]
	public GameObject GrappedTo;
	private Vector3 _PlayerPosition, _diffPosition;

	[HideInInspector]
	public GameObject _myShooter;
	[HideInInspector]
	public int howWasIShot;

	[HideInInspector]
	public Vector3 _anchorPoint;
	private Vector3 _myShooterInitPos, _myShooterPos;
	public float v_returnDistance, v_returnSpeedConst, v_allowedProximity, v_BreakDistance;
	private bool shouldIReturn=false;

	[HideInInspector]
	public Color myColor;

	public AudioClip v_linkedToAnObject, v_linkedToAPlayer;

	void Start(){
		if(_myShooter!=null){
			_myShooterInitPos=_myShooter.transform.position;
		}

		if(_myShooter.GetComponent<ShootF>().playerIndex==XInputDotNetPure.PlayerIndex.One){
			gameObject.transform.Find("B 5Janv").renderer.material.color=Color.yellow;
			gameObject.renderer.material.color=Color.yellow;
		}
		if(_myShooter.GetComponent<ShootF>().playerIndex==XInputDotNetPure.PlayerIndex.Two){
			gameObject.transform.Find("B 5Janv").renderer.material.color=Color.red;
			gameObject.renderer.material.color=Color.red;
		}		
		if(_myShooter.GetComponent<ShootF>().playerIndex==XInputDotNetPure.PlayerIndex.Three){
//			gameObject.transform.Find("B 5Janv").renderer.material.color=Color.green;
			gameObject.transform.Find("B 5Janv").renderer.material.color=new Color32(107,142,35,255);
			//			gameObject.renderer.material.color=Color.green;
			gameObject.renderer.material.color=new Color32(107,142,35,255);

		}		
		if(_myShooter.GetComponent<ShootF>().playerIndex==XInputDotNetPure.PlayerIndex.Four){
			gameObject.transform.Find("B 5Janv").renderer.material.color=Color.blue;
			gameObject.renderer.material.color=Color.blue;
		}
		
	} 

	//ce que touche la tete:
	void OnTriggerEnter(Collider _Collided){
		//si on touche qqch qui n'est:
		//ni mon shooter
		//ni le graphisme de mon shooter
		//ni une autre tete de tir
		//ni un lien
		if(_Collided.gameObject != _myShooter && _Collided.gameObject!= _myShooter.transform.Find("ApparenceAvatar").gameObject && _Collided.gameObject.name!="NewHookhead(Clone)" && _Collided.gameObject.name!="B 5Janv"){
//			Debug.Log("son nom est "+_Collided.name);

			if (GrappedTo == null){

				GrappedTo = _Collided.gameObject;
				if(GrappedTo.GetComponent<Sticky>()!=null){
					GrappedTo.GetComponent<Sticky>().v_numberOfLinks+=1;
				}

//				RaycastHit _hit;
//				Debug.Log("le collided est   "+_Collided.name);
//				Debug.Log("my shooter pos "+_myShooter.transform.position);
//
////				if (Physics.Raycast (transform.TransformPoint(_Collided.transform.position),transform.TransformPoint(gameObject.transform.position), out _hit, 50f)) {
//				if (Physics.Raycast (_myShooter.transform.position, gameObject.transform.position, out _hit, 50f)) {
//
//					Debug.DrawRay(_myShooter.transform.position, gameObject.transform.position, Color.red, 50f);
//					Debug.Log("position d hit   "+_hit.point);
//					_PlayerPosition = _hit.point;
//
//					//On va garder la position exact de l'impact par rapport au centre de la structure
//					//_diffPosition = _Collided.transform.position - _PlayerPosition;
//				}

				//gameObject.rigidbody.velocity = Vector3.zero;
				if(howWasIShot==1)_myShooter.GetComponent<ShootF>()._target=_Collided.gameObject;
				if(howWasIShot==2)_myShooter.GetComponent<ShootF>()._target1=_Collided.gameObject;

				//si on touche un autre joueur
				if(_Collided.gameObject.transform.Find("ApparenceAvatar")!=null){
					if(_Collided.gameObject.transform.Find("ApparenceAvatar").gameObject.tag == "Player"){
						//ET SI CE N EST PAS MOI
//						Debug.Log("suce");
						LinkStrenght _Linkcommited = _Collided.gameObject.GetComponent<LinkStrenght>();
						_Linkcommited._LinkCommited += 1;

						LinkStrenght _LinkcommitedToMe = _myShooter.GetComponent<LinkStrenght>();
						_LinkcommitedToMe._LinkCommited += 1;

						//audio joué quand on connect un joueur
						audio.PlayOneShot(v_linkedToAPlayer);
					}	
				}else{
					//audio joué quand on connect un block
					audio.PlayOneShot(v_linkedToAnObject);
				}
			}
		}

		//si la tete revient sur son tireur
		if(_Collided.gameObject==_myShooter){
			if(shouldIReturn==true){
//				Debug.Log("contact?");
				if(howWasIShot==1) Destroy(_myShooter.GetComponent<ShootF>()._myHook);
				if(howWasIShot==2) Destroy(_myShooter.GetComponent<ShootF>()._myHook1);
			}
		}
	}
	
	void Update (){
		//si le grappin est attaché à un objet, il suit ses mvts
		if(GrappedTo != null){
//			if(_diffPosition!=null){
			//**
			_PlayerPosition = GrappedTo.transform.position;
//			transform.position = _PlayerPosition;

			Vector3 temp = new Vector3(_PlayerPosition.x, _myShooter.transform.position.y, _PlayerPosition.z);
			gameObject.transform.position = temp;

			//ceci brise un lien lorsqu'il est trop grand
			//aucun feedback
			//aussi, vu que pour l'instant la tete du lien se met au centre (et donc bouge!) alors si on se connecte à la distance max, le lien se détruit automatiquement!
			if(Vector3.Distance(gameObject.transform.position, _myShooterPos)>=v_BreakDistance){
				_myShooter.GetComponent<ShootF>().DetachLink(howWasIShot - 1);
			}
		}
	}

//	void OnCollisionEnter(Collision _Collided){
//		//si on touche qqch qui n'est:
//		//ni mon shooter
//		//ni le graphisme de mon shooter
//		//ni une autre tete de tir
//		//ni un lien
//		if(_Collided.gameObject != _myShooter && _Collided.gameObject!= _myShooter.transform.Find("ApparenceAvatar").gameObject && _Collided.gameObject.name!="NewHookhead(Clone)" && _Collided.gameObject.name!="B 5Janv"){
//			Debug.Log("son nom est "+_Collided.gameObject.name);
//			gameObject.rigidbody.freezeRotation=true;
//
//			if (GrappedTo == null){
//				GrappedTo = _Collided.gameObject;
//				if(GrappedTo.GetComponent<Sticky>()!=null){
//					GrappedTo.GetComponent<Sticky>().v_numberOfLinks+=1;
//				}
//				
//				//gameObject.rigidbody.velocity = Vector3.zero;
//				if(howWasIShot==1)_myShooter.GetComponent<ShootF>()._target=_Collided.gameObject;
//				if(howWasIShot==2)_myShooter.GetComponent<ShootF>()._target1=_Collided.gameObject;
//				
//				//si on touche un autre joueur
//				if(_Collided.gameObject.transform.Find("ApparenceAvatar")!=null){
//					if(_Collided.gameObject.transform.Find("ApparenceAvatar").gameObject.tag == "Player"){
//						//ET SI CE N EST PAS MOI
//						LinkStrenght _Linkcommited = _Collided.gameObject.GetComponent<LinkStrenght>();
//						_Linkcommited._LinkCommited += 1;	
//					}	
//				}
//			}
//		}
//		
//		//si la tete revient sur son tireur
//		if(_Collided.gameObject==_myShooter){
//			if(shouldIReturn==true){
////				Debug.Log("contact?");
//				if(howWasIShot==1) Destroy(_myShooter.GetComponent<ShootF>()._myHook);
//				if(howWasIShot==2) Destroy(_myShooter.GetComponent<ShootF>()._myHook1);
//			}
//		}
//	}

//	void OnCollisionStay(Collision _Collided){
//		//si on touche qqch qui n'est:
//		//ni mon shooter
//		//ni le graphisme de mon shooter
//		//ni une autre tete de tir
//		//ni un lien
//		if(_Collided.gameObject != _myShooter && _Collided.gameObject!= _myShooter.transform.Find("ApparenceAvatar").gameObject && _Collided.gameObject.name!="NewHookhead(Clone)" && _Collided.gameObject.name!="B 5Janv"){
//			//for (int i = 0; i < _Collided.contacts.Length; i++) {
//				//if(_Collided.contacts[i]!=null){
//					//_anchorPoint=_Collided.contacts[i].point;
//					//break;
//				//}
//			//}
//			Debug.Log("contact "+ _Collided.contacts[0].point);
//			ContactPoint contact = _Collided.contacts[0];
//			gameObject.transform.position = contact.point;
//			_anchorPoint=contact.point;
//		}
//	}

	void FixedUpdate (){
		if(_myShooter!=null){
			_myShooterPos=_myShooter.transform.position;
			
			//15
			//on n'enclenche/ne permet le retour que si la tete du grappin n'est pas posée
			if (GrappedTo == null) {
				//si j'ai rien choppé
				//				if(Vector3.Distance(gameObject.transform.position, _myShooterInitPos)>=v_returnDistance){
				if(Vector3.Distance(gameObject.transform.position, _myShooterPos)>=v_returnDistance){
					
					shouldIReturn=true;
				}
				
				if(shouldIReturn==true){
					if(Vector3.Distance(gameObject.transform.position, gameObject.transform.Find("B 5Janv").GetComponent<InTheMiddle5Janv>()._whereIsItShot)<=v_allowedProximity){
						if(howWasIShot==1) Destroy(_myShooter.GetComponent<ShootF>()._myHook);
						if(howWasIShot==2) Destroy(_myShooter.GetComponent<ShootF>()._myHook1);
					}
					//shouldIReturn=false;
					//gameObject.rigidbody.velocity = Vector3.zero;
					Vector3 whereShouldIGo = _myShooterPos-gameObject.transform.position;
					whereShouldIGo.Normalize();
					gameObject.rigidbody.AddForce(whereShouldIGo*_myShooter.GetComponent<ShootF>().v_SpeedBullet*v_returnSpeedConst);
				}
			}
		}
	}

}
