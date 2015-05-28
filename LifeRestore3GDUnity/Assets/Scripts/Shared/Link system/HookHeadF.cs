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
	private Player_Status myPlayerStatus;
	[HideInInspector]
	public int howWasIShot;

	private Vector3 _myShooterPos;
	public float v_returnSpeedConst, v_allowedProximity, v_BreakDistance;
	private bool shouldIReturn=false;

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

	//ce layerMask est à compléter dans l'inspecteur: mettre "tout" et enlever les objets qu'on ne souhaite pas capter
	//ou ne mettre que les objets qu'on souhaite, au choix
	//hookhead a son propre layer!
	[SerializeField]
	private LayerMask layermaskForLocalisation;

	private SpringJoint spring;
	[SerializeField]
	private float springValue;

	private ShootF myShooterScript;
	private ReticuleCone myShooterRetCone;
	private Rigidbody myRigidbody;

	void Start(){
		if(_myShooter.GetComponent<Player_Status>()!=null) myPlayerStatus = _myShooter.GetComponent<Player_Status>();
		if(gameObject.GetComponent<Rigidbody>()!=null) myRigidbody = gameObject.GetComponent<Rigidbody>();
		if(_myShooter.GetComponent<ShootF>()!=null) myShooterScript = _myShooter.GetComponent<ShootF>();
		if(_myShooter.GetComponent<ReticuleCone>()!=null) myShooterRetCone = _myShooter.GetComponent<ReticuleCone>();

		if(myShooterScript.playerIndex==XInputDotNetPure.PlayerIndex.One){
			gameObject.transform.Find("B 5Janv").GetComponent<Renderer>().material.color=new Color32(107,142,35,255);
			gameObject.GetComponent<Renderer>().material.color=new Color32(107,142,35,255);
		}
		if(myShooterScript.playerIndex==XInputDotNetPure.PlayerIndex.Two){
			gameObject.transform.Find("B 5Janv").GetComponent<Renderer>().material.color=Color.red;
			gameObject.GetComponent<Renderer>().material.color=Color.red;
		}		
		if(myShooterScript.playerIndex==XInputDotNetPure.PlayerIndex.Three){
			gameObject.transform.Find("B 5Janv").GetComponent<Renderer>().material.color=Color.blue;
			gameObject.GetComponent<Renderer>().material.color=Color.blue;

		}		
		if(myShooterScript.playerIndex==XInputDotNetPure.PlayerIndex.Four){
			gameObject.transform.Find("B 5Janv").GetComponent<Renderer>().material.color=Color.yellow;
			gameObject.GetComponent<Renderer>().material.color=Color.yellow;
		}
	} 

	//ce que touche la tete:
	void OnTriggerEnter(Collider _Collided){
		//si je touche qqch qui a le script Sticky
		if(_Collided.gameObject.GetComponent<Sticky>() != null && _Collided.gameObject.tag.Equals("Unlinkable")==false){
			if(_Collided.gameObject.GetComponent<Sticky>().v_numberOfLinks<_Collided.gameObject.GetComponent<Sticky>().authorizedNumberOfLinks){
				if (GrappedTo == null){
					if(_hitSmth==false){
						_hitSmth=true;
						//ON UTILISE PLUS LOCALPOS?
						GrappedTo = _Collided.gameObject;
						myPlayerStatus.linkedObject = GrappedTo;
						GrappedTo.GetComponent<Sticky>().v_numberOfLinks+=1;
						GrappedTo.GetComponent<Sticky>().myHolderPlayer = _myShooter;
						if(howWasIShot==1)myShooterScript._target=GrappedTo;

						_localPos = _Collided.transform.InverseTransformPoint(gameObject.transform.position);
	//					Debug.DrawRay(GrappedTo.transform.TransformPoint(_localPos), Vector3.up*10);
						newTensionLessDistance = Vector3.Distance(gameObject.transform.position, _myShooterPos);
            Debug.Log("TensionLessDistance  " + newTensionLessDistance);
						newBreakDistance = newTensionLessDistance*100/breakDistanceRatio;
						myRigidbody.isKinematic=true;

						//on ajoute un spring entre le joueur et grappedTo si grappedTo est une fronde, sinon on laisse juste elasticScript
	//					if(GrappedTo.gameObject.tag.Equals("Fronde")==true){
	//						_myShooter.AddComponent<SpringJoint>();
	//						spring = _myShooter.GetComponent<SpringJoint>();
	//						spring.connectedBody = GrappedTo.GetComponent<Rigidbody>();
	//						spring.autoConfigureConnectedAnchor = false;
	//	//					spring.connectedAnchor = spring.connectedBody.gameObject.transform.InverseTransformPoint(spring.connectedBody.gameObject.transform.position);
	//
	//						spring.minDistance=Vector3.Distance(GrappedTo.gameObject.transform.position, _myShooterPos);
	//						spring.enableCollision=true;
	//					spring.spring = springValue;
	//					}
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
	}

	void Update (){
    Debug.Log("HookheadFpos   " + gameObject.transform.position);
		//si le grappin est attaché à un objet, il suit ses mvts
		if(GrappedTo != null){

			_myShooter.transform.Find("Pointillés").gameObject.GetComponent<AimHelperReticule>().HideHelper();
			ReticuleTarget grappedToRetTarget = GrappedTo.GetComponent<ReticuleTarget>();

			if(myShooterRetCone.numberOfThisPlayer == 1){
				grappedToRetTarget.LightReticuleUp(grappedToRetTarget.GRend);

			}else if(myShooterRetCone.numberOfThisPlayer == 2){
				grappedToRetTarget.LightReticuleUp(grappedToRetTarget.RRend);

			}else if(myShooterRetCone.numberOfThisPlayer == 3){
				grappedToRetTarget.LightReticuleUp(grappedToRetTarget.BRend);

			}else if(myShooterRetCone.numberOfThisPlayer == 4){
				grappedToRetTarget.LightReticuleUp(grappedToRetTarget.YRend);
			}

//			if(_myShooter.GetComponent<ReticuleCone>().numberOfThisPlayer == 1){
//				GrappedTo.GetComponent<ReticuleTarget>().TurnReticuleOff(GrappedTo.GetComponent<ReticuleTarget>().YRend);
//
//			}else if(_myShooter.GetComponent<ReticuleCone>().numberOfThisPlayer == 2){
//				GrappedTo.GetComponent<ReticuleTarget>().TurnReticuleOff(GrappedTo.GetComponent<ReticuleTarget>().RRend);
//
//			}else if(_myShooter.GetComponent<ReticuleCone>().numberOfThisPlayer == 3){
//				GrappedTo.GetComponent<ReticuleTarget>().TurnReticuleOff(GrappedTo.GetComponent<ReticuleTarget>().GRend);
//
//			}else if(_myShooter.GetComponent<ReticuleCone>().numberOfThisPlayer == 4){
//				GrappedTo.GetComponent<ReticuleTarget>().TurnReticuleOff(GrappedTo.GetComponent<ReticuleTarget>().BRend);
//			}

			if(myRigidbody.isKinematic==true) myRigidbody.isKinematic=false;

			//uncomment below for non-contact points spacialization
			//ON UTILISE PLUS??

			if(shotRaycast==false){
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
				Vector3 shooterPosHigher = new Vector3(_myShooterPos.x, _myShooterPos.y+1f, _myShooterPos.z);
//				Debug.DrawRay(_myShooterPos, GrappedTo.transform.TransformPoint(_localPos)-_myShooterPos);
//				if(Physics.Raycast(_myShooterPos, GrappedTo.transform.TransformPoint(_localPos)-_myShooterPos, out hit, Mathf.Infinity, layermaskForLocalisation)){
//				Debug.DrawRay(shooterPosHigher, GrappedTo.transform.TransformPoint(_localPos)-shooterPosHigher);
				if(Physics.Raycast(shooterPosHigher, GrappedTo.transform.TransformPoint(_localPos)-shooterPosHigher, out hit, Mathf.Infinity, layermaskForLocalisation)){
					shotRaycast = true;
//					Debug.Log(hit.collider.name);
					positionDetermined = hit.collider.gameObject.transform.InverseTransformPoint(hit.point);
					gameObject.transform.position= GrappedTo.transform.TransformPoint(_localPos);
//					spring.connectedAnchor = hit.collider.gameObject.transform.InverseTransformPoint(hit.point);
				}
			}else{
				gameObject.transform.position = GrappedTo.transform.TransformPoint(positionDetermined);
			}
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
				if (Vector3.Distance(gameObject.transform.position, _myShooterPos) >= v_BreakDistance){
					if(shouldIReturn == false) myRigidbody.velocity= Vector3.zero;
					shouldIReturn = true;
				}

				if (shouldIReturn == true){
					if (Vector3.Distance(gameObject.transform.position, gameObject.transform.Find("B 5Janv").GetComponent<LinkInTheMiddle>()._whereIsItShot) <= v_allowedProximity)
					{
						if (howWasIShot == 1) Destroy(myShooterScript._myHook);
					}
				}
			}
		}

		//if faut gérer la destruction de l'objet auquel je me suis lié
		//genre jme connecte à un truc et qqn d'autre le pète: paf detachLink
		if(_hitSmth==true && GrappedTo ==null){
			myShooterScript.DetachLink(0);
		}
	}

	void FixedUpdate (){
		if(_myShooter!=null){
			//on n'enclenche/ne permet le retour que si la tete du grappin n'est pas posée
			if (GrappedTo == null) {
				if(shouldIReturn==true){
					Vector3 whereShouldIGo = _myShooterPos-gameObject.transform.position;
					whereShouldIGo.Normalize();
					myRigidbody.AddForce(whereShouldIGo*myShooterScript.v_SpeedBullet*v_returnSpeedConst*1000);
				}
			}
		}
	}
}
