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

	//information liées au tir: qui m'a tiré, et comment
	[HideInInspector]
	public GameObject _myShooter;
	[HideInInspector]
	public int howWasIShot;

	private Vector3 _myShooterPos;
	public float v_returnSpeedConst, v_allowedProximity, v_BreakDistance;
	private bool shouldIReturn=false;

	public AudioClip v_linkedToAnObject, v_linkedToAPlayer;
	
	[HideInInspector]
	public Vector3 _localPos;

	//utilisés pour la détection de collision affinée
	[SerializeField]
	private float v_detectionRadius;
	private bool _hitSmth = false;
	private Vector3 where2;

	//24mars
	//attempt at new link
	[Space(10)]
	[Header("New Link System")]
	[TooltipAttribute("Check to use the new link system")]
	public bool newLinkSystem;
	[HideInInspector]
	public float newTensionLessDistance;
	[TooltipAttribute("Break distance = this% de tensionLessD")]
	[SerializeField]
	[Range(0,100)]
	private float breakDistanceRatio;
	[HideInInspector]
	public float newBreakDistance;

	void Start(){
		if(_myShooter.GetComponent<ShootF>().playerIndex==XInputDotNetPure.PlayerIndex.One){
			gameObject.transform.Find("B 5Janv").GetComponent<Renderer>().material.color=Color.yellow;
			gameObject.GetComponent<Renderer>().material.color=Color.yellow;
		}
		if(_myShooter.GetComponent<ShootF>().playerIndex==XInputDotNetPure.PlayerIndex.Two){
			gameObject.transform.Find("B 5Janv").GetComponent<Renderer>().material.color=Color.red;
			gameObject.GetComponent<Renderer>().material.color=Color.red;
		}		
		if(_myShooter.GetComponent<ShootF>().playerIndex==XInputDotNetPure.PlayerIndex.Three){
			gameObject.transform.Find("B 5Janv").GetComponent<Renderer>().material.color=new Color32(107,142,35,255);
			gameObject.GetComponent<Renderer>().material.color=new Color32(107,142,35,255);

		}		
		if(_myShooter.GetComponent<ShootF>().playerIndex==XInputDotNetPure.PlayerIndex.Four){
			gameObject.transform.Find("B 5Janv").GetComponent<Renderer>().material.color=Color.blue;
			gameObject.GetComponent<Renderer>().material.color=Color.blue;
		}
	} 

	//ce que touche la tete:
//	void OnTriggerEnter(Collider _Collided){
////		Debug.Log("i am " + gameObject.name);
//
//		//si on touche qqch qui n'est:
//		//ni mon shooter
//		//ni une autre tete de tir
//		//ni un lien
//		//ni un trigger de changement de slide dans le cadre de la présentation du 14 janvier
//		if(_Collided.gameObject != _myShooter && _Collided.gameObject.name!="NewHookhead(Clone)" && _Collided.gameObject.name!="B 5Janv" && _Collided.gameObject.tag!=("Respawn")){
////			Debug.Log("son nom est "+_Collided.name);
//
//			if (GrappedTo == null){
//				//ON UTILISE PLUS LOCALPOS?
//				_localPos = _Collided.transform.InverseTransformPoint(gameObject.transform.position);
//				GrappedTo = _Collided.gameObject;
//				if(GrappedTo.GetComponent<Sticky>()!=null){
//					GrappedTo.GetComponent<Sticky>().v_numberOfLinks+=1;
//				}
//
//				//gameObject.rigidbody.velocity = Vector3.zero;
//				if(howWasIShot==1)_myShooter.GetComponent<ShootF>()._target=_Collided.gameObject;
//
//				//si on touche un autre joueur
//				if(_Collided.gameObject.transform.Find("ApparenceAvatar")!=null){
//					if(_Collided.gameObject.transform.Find("ApparenceAvatar").gameObject.tag == "Player"){
//						//ET SI CE N EST PAS MOI
//						LinkStrenght _Linkcommited = _Collided.gameObject.GetComponent<LinkStrenght>();
//						_Linkcommited._LinkCommited += 1;
//
//						LinkStrenght _LinkcommitedToMe = _myShooter.GetComponent<LinkStrenght>();
//						_LinkcommitedToMe._LinkCommited += 1;
//
//						//audio joué quand on connect un joueur
//						GetComponent<AudioSource>().PlayOneShot(v_linkedToAPlayer);
//					}	
//				}else{
//					//audio joué quand on connect un block
//					GetComponent<AudioSource>().PlayOneShot(v_linkedToAnObject);
//				}
//			}
//		}
//	}

	void Update (){
		//collisions affinées
		//idée: on balance un raycast dans diverses directions (de base, juste en face) autour de la tete de lien
		//raycast de longueur choisir V_detectionRadius
		//on récupère la position relative du hit par rapport à l'objet qu'on a hit, et on la stocke dans where2
		//suite de where2 
		if(GrappedTo==null){
			if(_hitSmth==false){
				for (int j = 1; j < 7; j++) {
					//8 raycasts, 360 degrés, 360/8=45, appliqué sur un cercle trigonométrique et une conversion de degrés ° vers radia
					//6 raycasts, 360 degrés, 360/6=60, appliqué sur un cercle trigonométrique et une conversion de degrés ° vers radia

					RaycastHit hit;
//					if(Physics.Raycast(transform.position, new Vector3(Mathf.Cos(j*45f*Mathf.Deg2Rad), 0f, Mathf.Sin(j*45f*Mathf.Deg2Rad)), out hit, v_detectionRadius) && hit.collider.gameObject != _myShooter && hit.collider.gameObject.name!="NewHookhead(Clone)" && hit.collider.gameObject.name != "B 5Janv"){
					if(Physics.Raycast(transform.position, new Vector3(Mathf.Cos(j*60f*Mathf.Deg2Rad), 0f, Mathf.Sin(j*60f*Mathf.Deg2Rad)), out hit, v_detectionRadius) && hit.collider.gameObject != _myShooter && hit.collider.gameObject.name!="NewHookhead(Clone)" && hit.collider.gameObject.name != "B 5Janv" && hit.collider.gameObject.name != "BlocTriggerZone"){

						_hitSmth=true;
						GrappedTo=hit.collider.gameObject;
						if(GrappedTo.GetComponent<Sticky>()!=null){
							GrappedTo.GetComponent<Sticky>().v_numberOfLinks+=1;
						}
						if(howWasIShot==1)_myShooter.GetComponent<ShootF>()._target=GrappedTo;

//						Debug.Log("i hit "+ hit.collider.gameObject.name);
						where2 = hit.collider.gameObject.transform.InverseTransformPoint(hit.point);
//						newTensionLessDistance = Vector3.Distance(where2, _myShooterPos);
						newTensionLessDistance = Vector3.Distance(gameObject.transform.position, _myShooterPos);

						//newtensionlessdistance = "breakdistanceRatio" de newBreakDistance 
						newBreakDistance = newTensionLessDistance*100/breakDistanceRatio;
						break;
					}
//					Debug.DrawRay(transform.position, new Vector3(Mathf.Cos(j*45f*Mathf.Deg2Rad), 0f, Mathf.Sin(j*45f*Mathf.Deg2Rad)).normalized*v_detectionRadius, Color.red);

					Debug.DrawRay(transform.position, new Vector3(Mathf.Cos(j*60f*Mathf.Deg2Rad), 0f, Mathf.Sin(j*60f*Mathf.Deg2Rad)).normalized*v_detectionRadius, Color.red);
				}
			}
		}

		//si le grappin est attaché à un objet, il suit ses mvts
		if(GrappedTo != null){

			//uncomment below for non-contact points spacialization
			//ON UTILISE PLUS??
			gameObject.transform.position = GrappedTo.transform.TransformPoint(_localPos);

			//localisation ameliorée
			gameObject.transform.position = GrappedTo.transform.TransformPoint(where2);

			//ceci brise un lien lorsqu'il est trop grand
			//aucun feedback
			//aussi, vu que pour l'instant la tete du lien se met au centre (et donc bouge!) alors si on se connecte à la distance max, le lien se détruit automatiquement!
			if(newLinkSystem==false){
				if(Vector3.Distance(gameObject.transform.position, _myShooterPos)>=v_BreakDistance){
					_myShooter.GetComponent<ShootF>().DetachLink(howWasIShot - 1);
				}
			}else{
				if(Vector3.Distance(gameObject.transform.position, _myShooterPos)>=newBreakDistance){
					_myShooter.GetComponent<ShootF>().DetachLink(howWasIShot - 1);
				}
			}
		}

		//CECI ETAIT DANS LE FIXED UPDATE
		if(_myShooter!=null){
			_myShooterPos=_myShooter.transform.position;

			//on n'enclenche/ne permet le retour que si la tete du grappin n'est pas posée
			if (GrappedTo == null) {
				//si j'ai rien choppé
				//if(newLinkSystem==false){
					if(Vector3.Distance(gameObject.transform.position, _myShooterPos)>=v_BreakDistance){
						shouldIReturn=true;

					}
				//}
				/*else{
					if(Vector3.Distance(gameObject.transform.position, _myShooterPos)>=newBreakDistance){
						shouldIReturn=true;

					}
				}*/
				
				if(shouldIReturn==true){
					if(Vector3.Distance(gameObject.transform.position, gameObject.transform.Find("B 5Janv").GetComponent<LinkInTheMiddle>()._whereIsItShot)<=v_allowedProximity){
//						Debug.Log("death?");
						if(howWasIShot==1) Destroy(_myShooter.GetComponent<ShootF>()._myHook);
					}
				}
			}
		}
	}

	void FixedUpdate (){
		if(_myShooter!=null){
			//on n'enclenche/ne permet le retour que si la tete du grappin n'est pas posée
			if (GrappedTo == null) {
				if(shouldIReturn==true){
					Vector3 whereShouldIGo = _myShooterPos-gameObject.transform.position;
					whereShouldIGo.Normalize();
					gameObject.GetComponent<Rigidbody>().AddForce(whereShouldIGo*_myShooter.GetComponent<ShootF>().v_SpeedBullet*v_returnSpeedConst*1000);
				}
			}
		}
	}

	void OnDrawGizmos() {
		Color32 color = new Color32(255,204,0, 50);
		Gizmos.color = color;
		Gizmos.DrawSphere(transform.position, v_detectionRadius);
	}
}
