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
  public float newTensionLessDistance, _actualDistance;
	[TooltipAttribute("Break distance = this% de tensionLessD")]
	[SerializeField]
	[Range(0,100)]
	public float breakDistanceRatio;
	[HideInInspector]
	public float newBreakDistance;

	private bool shotRaycast = false;

	//on stocke la position à laquelle l'objet doit se situer s'il a choppé qqch
	private Vector3 positionDetermined;

	//on stocke la position de l'objet à chaque frame jusqu'à la derniere frame avant qu'il touche qqch
	private Vector3 myLastNonColPos;

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
	void OnTriggerEnter(Collider _Collided){
//		Debug.Log("i am " + gameObject.name);

		//si on touche qqch qui n'est:
		//ni mon shooter
		//ni une autre tete de tir
		//ni un lien
		//ni un trigger de changement de slide dans le cadre de la présentation du 14 janvier
//		if(_Collided.gameObject != _myShooter && _Collided.gameObject.name!="NewHookhead(Clone)" && _Collided.gameObject.name!="B 5Janv" && _Collided.gameObject.tag!=("Respawn")){

		//si je touche qqch qui a le script Sticky
		if(_Collided.gameObject.GetComponent<Sticky>() != null){
		
//			Debug.Log("son nom est "+_Collided.name);

			if (GrappedTo == null){
				if(_hitSmth==false){
					_hitSmth=true;
					//ON UTILISE PLUS LOCALPOS?
					GrappedTo = _Collided.gameObject;
					GrappedTo.GetComponent<Sticky>().v_numberOfLinks+=1;

					if(howWasIShot==1)_myShooter.GetComponent<ShootF>()._target=GrappedTo;

					_localPos = _Collided.transform.InverseTransformPoint(gameObject.transform.position);
					newTensionLessDistance = Vector3.Distance(gameObject.transform.position, _myShooterPos);
					newBreakDistance = newTensionLessDistance*100/breakDistanceRatio;
					gameObject.GetComponent<Rigidbody>().isKinematic=true;
					//si on touche un autre joueur
//					if(_Collided.gameObject.transform.Find("ApparenceAvatar")!=null){
//						if(_Collided.gameObject.transform.Find("ApparenceAvatar").gameObject.tag == "Player"){
//							//ET SI CE N EST PAS MOI
//							LinkStrenght _Linkcommited = _Collided.gameObject.GetComponent<LinkStrenght>();
//							_Linkcommited._LinkCommited += 1;
//
//							LinkStrenght _LinkcommitedToMe = _myShooter.GetComponent<LinkStrenght>();
//							_LinkcommitedToMe._LinkCommited += 1;
//
//							//audio joué quand on connect un joueur
//							GetComponent<AudioSource>().PlayOneShot(v_linkedToAPlayer);
//						}	
//					}else{
//						//audio joué quand on connect un block
//						GetComponent<AudioSource>().PlayOneShot(v_linkedToAnObject);
//					}
				}
			}
		}
	}

	void Update (){
		//si le grappin est attaché à un objet, il suit ses mvts
		if(GrappedTo != null){

			if(gameObject.GetComponent<Rigidbody>().isKinematic==true) gameObject.GetComponent<Rigidbody>().isKinematic=false;

			//uncomment below for non-contact points spacialization
			//ON UTILISE PLUS??

			if(shotRaycast==false){
				//il faut ici mettre le layer "linkable"
				int layerMaskMoi = 1<< 17;
				RaycastHit hit;

				//si distance entre shooter et moi avant la collision plus petite que entre shooter et objet collidé
				//aka si le hookhead choppe l'objet de face
//				if(Vector3.Distance(myLastNonColPos, _myShooterPos)<Vector3.Distance(_myShooterPos, GrappedTo.transform.position)){
//				
//
//					if(Physics.Raycast(_myShooterPos, GrappedTo.transform.TransformPoint(_localPos)-_myShooterPos, out hit, Mathf.Infinity, layerMaskMoi)){
//						shotRaycast = true;
//						positionDetermined = hit.collider.gameObject.transform.InverseTransformPoint(hit.point);
//
//						Debug.Log("hit : "+hit.collider.gameObject.name);
//						Debug.DrawRay(positionDetermined, -Vector3.up*10);
//						gameObject.transform.position= GrappedTo.transform.TransformPoint(_localPos);
//					}
//				}else{
					if(Physics.Raycast(_myShooterPos, GrappedTo.transform.TransformPoint(_localPos)-_myShooterPos, out hit, Mathf.Infinity, layerMaskMoi)){
						shotRaycast = true;
						positionDetermined = hit.collider.gameObject.transform.InverseTransformPoint(hit.point);

						Debug.Log("hit : "+hit.collider.gameObject.name);
						Debug.DrawRay(positionDetermined, -Vector3.up*10);
						gameObject.transform.position= GrappedTo.transform.TransformPoint(_localPos);
					}
//				}
			}
			else{
//				positionDetermined = GrappedTo.transform.TransformPoint(_localPos);
//			}
				gameObject.transform.position = GrappedTo.transform.TransformPoint(positionDetermined);
			}
			//localisation ameliorée
//			gameObject.transform.position = GrappedTo.transform.TransformPoint(where2);


			//ceci brise un lien lorsqu'il est trop grand
			//aucun feedback
			//aussi, vu que pour l'instant la tete du lien se met au centre (et donc bouge!) alors si on se connecte à la distance max, le lien se détruit automatiquement!
			/*if(newLinkSystem==false){
				if(Vector3.Distance(gameObject.transform.position, _myShooterPos)>=v_BreakDistance){
					_myShooter.GetComponent<ShootF>().DetachLink(howWasIShot - 1);
				}
			}else{
				if(Vector3.Distance(gameObject.transform.position, _myShooterPos)>=newBreakDistance){
					_myShooter.GetComponent<ShootF>().DetachLink(howWasIShot - 1);
				}
			}*/
		}

		_actualDistance = Vector3.Distance(gameObject.transform.position, _myShooterPos);
		//CECI ETAIT DANS LE FIXED UPDATE
		if (_myShooter != null)
		{
			_myShooterPos = _myShooter.transform.position;
			//on n'enclenche/ne permet le retour que si la tete du grappin n'est pas posée
			if (GrappedTo == null)
			{
				myLastNonColPos = gameObject.transform.position;

				//si j'ai rien choppé
				//if(newLinkSystem==false){
				if (Vector3.Distance(gameObject.transform.position, _myShooterPos) >= v_BreakDistance){
					shouldIReturn = true;
				}
        //}
        /*else{
          if(Vector3.Distance(gameObject.transform.position, _myShooterPos)>=newBreakDistance){
            shouldIReturn=true;

          }
        }*/
				if (shouldIReturn == true){
					if (Vector3.Distance(gameObject.transform.position, gameObject.transform.Find("B 5Janv").GetComponent<LinkInTheMiddle>()._whereIsItShot) <= v_allowedProximity)
					{
            //						Debug.Log("death?");
						if (howWasIShot == 1) Destroy(_myShooter.GetComponent<ShootF>()._myHook);
					}
				}
			}
		}

		//collisions affinées
		//idée: on balance un raycast dans diverses directions (de base, juste en face) autour de la tete de lien
		//raycast de longueur choisir V_detectionRadius
		//on récupère la position relative du hit par rapport à l'objet qu'on a hit, et on la stocke dans where2
		//suite de where2 
//		if(GrappedTo==null){
//			if(_hitSmth==false){
////				for (int j = 1; j < 7; j++) {
//				for (int j = 1; j < 9; j++) {
//					//8 raycasts, 360 degrés, 360/8=45, appliqué sur un cercle trigonométrique et une conversion de degrés ° vers radia
//					//6 raycasts, 360 degrés, 360/6=60, appliqué sur un cercle trigonométrique et une conversion de degrés ° vers radia
//
//					RaycastHit hit;
//					if(Physics.Raycast(transform.position, new Vector3(Mathf.Cos(j*45f*Mathf.Deg2Rad), 0f, Mathf.Sin(j*45f*Mathf.Deg2Rad)), out hit, v_detectionRadius)
////					if(Physics.Raycast(transform.position, new Vector3(Mathf.Cos(j*60f*Mathf.Deg2Rad), 0f, Mathf.Sin(j*60f*Mathf.Deg2Rad)), out hit, v_detectionRadius)
//					   && hit.collider.gameObject != _myShooter
//					   && hit.collider.gameObject.name!="NewHookhead(Clone)"
//					   && hit.collider.gameObject.name != "B 5Janv"
//					   && hit.collider.gameObject.name != "BlocTriggerZone"
//					   && hit.collider.gameObject.name != "ReceptacleCollider"
//					   && hit.collider.gameObject.name != "SupportReceptacle"
//					   && hit.collider.gameObject.name != "RoomManager"){
//
//						_hitSmth=true;
//						GrappedTo=hit.collider.gameObject;
//						if(GrappedTo.GetComponent<Sticky>()!=null){
//							GrappedTo.GetComponent<Sticky>().v_numberOfLinks+=1;
//						}
//						if(howWasIShot==1)_myShooter.GetComponent<ShootF>()._target=GrappedTo;
//
////						Debug.Log("i hit "+ hit.collider.gameObject.name);
//						where2 = hit.collider.gameObject.transform.InverseTransformPoint(hit.point);
////						newTensionLessDistance = Vector3.Distance(where2, _myShooterPos);
//						newTensionLessDistance = Vector3.Distance(gameObject.transform.position, _myShooterPos);
//
//						//newtensionlessdistance = "breakdistanceRatio" de newBreakDistance 
//						newBreakDistance = newTensionLessDistance*100/breakDistanceRatio;
//						gameObject.GetComponent<Rigidbody>().isKinematic=true;
//						break;
//					}
//					Debug.DrawRay(transform.position, new Vector3(Mathf.Cos(j*45f*Mathf.Deg2Rad), 0f, Mathf.Sin(j*45f*Mathf.Deg2Rad)).normalized*v_detectionRadius, Color.red);
////					Debug.DrawRay(transform.position, new Vector3(Mathf.Cos(j*60f*Mathf.Deg2Rad), 0f, Mathf.Sin(j*60f*Mathf.Deg2Rad)).normalized*v_detectionRadius, Color.red);
//				}
//			}
//		}
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
