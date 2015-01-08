using UnityEngine;
using System.Collections;

public class HookHeadF : MonoBehaviour {
	
	//L'objet auquel est attachée la tete
	public GameObject GrappedTo;
	private Vector3 _PlayerPosition;

	[HideInInspector]
	public GameObject _myShooter;
	[HideInInspector]
	public int howWasIShot;
	private Vector3 _myShooterInitPos, _myShooterPos;
	public float v_returnDistance, v_returnSpeedConst, v_allowedProximity;
	private bool shouldIReturn=false;

	[HideInInspector]
	public Color myColor;
	
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
	
	void OnTriggerEnter(Collider _Collided){
		if(_Collided.gameObject != _myShooter && _Collided.gameObject.name!="NewHookhead(Clone)" && _Collided.gameObject.name!="B 5Janv"){
//		if(_Collided.gameObject != _myShooter && _Collided.gameObject.name!="Tete(Clone)" && _Collided.gameObject.name!="Lien"){

			if (GrappedTo == null){
				GrappedTo = _Collided.gameObject;
				if(GrappedTo.GetComponent<Sticky>()!=null){
					GrappedTo.GetComponent<Sticky>().v_numberOfLinks+=1;
				}

				//gameObject.rigidbody.velocity = Vector3.zero;
				if(howWasIShot==1)_myShooter.GetComponent<ShootF>()._target=_Collided.gameObject;
				if(howWasIShot==2)_myShooter.GetComponent<ShootF>()._target1=_Collided.gameObject;

				if(GrappedTo.rigidbody == null){
					GrappedTo.AddComponent<Rigidbody>();
					GrappedTo.rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
					GrappedTo.rigidbody.drag=5;
					GrappedTo.rigidbody.angularDrag=5;

					//si on veut que les objets aient des poids différents selon leur taille, il faut le préciser ici
//					if(GrappedTo.tag=="PetitObstacle"){
//						GrappedTo.rigidbody.mass=10f;
//					}else if(GrappedTo.tag=="GrandObstacle"){
//						GrappedTo.rigidbody.mass=50f;
//					}
				}

				if(_Collided.gameObject.tag == "Player"){
					//ET SI CE N EST PAS MOI
					LinkStrenght _Linkcommited = _Collided.gameObject.GetComponent<LinkStrenght>();
					_Linkcommited._LinkCommited += 1;
					
				}
			}
		}

		if(_Collided.gameObject==_myShooter){
			if(shouldIReturn==true){
				if(howWasIShot==1) Destroy(_myShooter.GetComponent<ShootF>()._myHook);
				if(howWasIShot==2) Destroy(_myShooter.GetComponent<ShootF>()._myHook1);
//				Destroy(gameObject);
			}
		}
	}
	
	void Update (){
		//si l'objet attaché est un joueur alors le grappin va "suivre" ses mouvements
		if(GrappedTo != null){
			_PlayerPosition = GrappedTo.transform.position;
			transform.position = _PlayerPosition;
		}

//		if(_myShooter!=null){
//			_myShooterPos=_myShooter.transform.position;
//			
//			//15
//			//on n'enclenche/ne permet le retour que si la tete du grappin n'est pas posée
//			if (GrappedTo == null) {
//				if(Vector3.Distance(gameObject.transform.position, _myShooterInitPos)>=v_returnDistance){
//					shouldIReturn=true;
//				}
//				
//				if(shouldIReturn==true){
//					//shouldIReturn=false;
//					//gameObject.rigidbody.velocity = Vector3.zero;
//					Vector3 whereShouldIGo = _myShooterPos-gameObject.transform.position;
//					whereShouldIGo.Normalize();
////					gameObject.rigidbody.AddForce((_myShooterPos-gameObject.transform.position)*_myShooter.GetComponent<ShootF>()._SpeedBullet*v_returnSpeedConst);
//					gameObject.rigidbody.AddForce(whereShouldIGo*_myShooter.GetComponent<ShootF>()._SpeedBullet*v_returnSpeedConst);
//
//				}
//			}
//		}
	}

	void FixedUpdate (){
		if(_myShooter!=null){
			_myShooterPos=_myShooter.transform.position;
			
			//15
			//on n'enclenche/ne permet le retour que si la tete du grappin n'est pas posée
			if (GrappedTo == null) {
				if(Vector3.Distance(gameObject.transform.position, _myShooterInitPos)>=v_returnDistance){
					shouldIReturn=true;
				}
				
				if(shouldIReturn==true){
					if(Vector3.Distance(gameObject.transform.position, _myShooterPos)<=v_allowedProximity){
						if(howWasIShot==1) Destroy(_myShooter.GetComponent<ShootF>()._myHook);
						if(howWasIShot==2) Destroy(_myShooter.GetComponent<ShootF>()._myHook1);
					}
					//shouldIReturn=false;
					//gameObject.rigidbody.velocity = Vector3.zero;
					Vector3 whereShouldIGo = _myShooterPos-gameObject.transform.position;
					whereShouldIGo.Normalize();
					//					gameObject.rigidbody.AddForce((_myShooterPos-gameObject.transform.position)*_myShooter.GetComponent<ShootF>()._SpeedBullet*v_returnSpeedConst);
					gameObject.rigidbody.AddForce(whereShouldIGo*_myShooter.GetComponent<ShootF>()._SpeedBullet*v_returnSpeedConst);
					
				}
			}
		}
	}
}
