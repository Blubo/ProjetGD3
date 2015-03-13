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
	public float v_returnDistance, v_returnSpeedConst, v_allowedProximity, v_BreakDistance;
	private bool shouldIReturn=false;

	public AudioClip v_linkedToAnObject, v_linkedToAPlayer;
	
	[HideInInspector]
	public Vector3 _localPos;

	//utilisés pour la détection de collision affinée
	[SerializeField]
	private float v_detectionRadius;
	private bool _hitSmth = false;
	private Vector3 where2;

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
		if(_Collided.gameObject != _myShooter && _Collided.gameObject.name!="NewHookhead(Clone)" && _Collided.gameObject.name!="B 5Janv" && _Collided.gameObject.tag!=("Respawn")){
//			Debug.Log("son nom est "+_Collided.name);

			if (GrappedTo == null){
				//ON UTILISE PLUS LOCALPOS?
				_localPos = _Collided.transform.InverseTransformPoint(gameObject.transform.position);
				GrappedTo = _Collided.gameObject;
				if(GrappedTo.GetComponent<Sticky>()!=null){
					GrappedTo.GetComponent<Sticky>().v_numberOfLinks+=1;
				}

				//gameObject.rigidbody.velocity = Vector3.zero;
				if(howWasIShot==1)_myShooter.GetComponent<ShootF>()._target=_Collided.gameObject;

				//si on touche un autre joueur
				if(_Collided.gameObject.transform.Find("ApparenceAvatar")!=null){
					if(_Collided.gameObject.transform.Find("ApparenceAvatar").gameObject.tag == "Player"){
						//ET SI CE N EST PAS MOI
						LinkStrenght _Linkcommited = _Collided.gameObject.GetComponent<LinkStrenght>();
						_Linkcommited._LinkCommited += 1;

						LinkStrenght _LinkcommitedToMe = _myShooter.GetComponent<LinkStrenght>();
						_LinkcommitedToMe._LinkCommited += 1;

						//audio joué quand on connect un joueur
						GetComponent<AudioSource>().PlayOneShot(v_linkedToAPlayer);
					}	
				}else{
					//audio joué quand on connect un block
					GetComponent<AudioSource>().PlayOneShot(v_linkedToAnObject);
				}
			}
		}
	}

	void Update (){
		//collisions affinées lol
		//idée: on balance un raycast dans diverses directions (de base, juste en face) autour de la tete de lien
		//raycast de longueur choisir V_detectionRadius
		//si un raycast touche qqch de layer 11 (Blocks) !!! alors
		//on récupère la position relative du hit par rapport à l'objet qu'on a hit, et on la stocke dans where2
		//suite de where2 
		if(_hitSmth==false){
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.forward, out hit, v_detectionRadius)||Physics.Raycast(transform.position, -transform.forward, out hit, v_detectionRadius)||Physics.Raycast(transform.position, transform.right, out hit, v_detectionRadius)||Physics.Raycast(transform.position, -transform.right, out hit, v_detectionRadius)){
				if(hit.collider.gameObject.layer == 11){
					Debug.Log("hit!");
					_hitSmth=true;
					where2 = hit.collider.gameObject.transform.InverseTransformPoint(hit.point);
				}
			}
			Debug.DrawRay(transform.position, transform.forward.normalized*v_detectionRadius, Color.red);
			Debug.DrawRay(transform.position, -transform.forward.normalized*v_detectionRadius, Color.red);
			Debug.DrawRay(transform.position, transform.right.normalized*v_detectionRadius, Color.red);
			Debug.DrawRay(transform.position, -transform.right.normalized*v_detectionRadius, Color.red);
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
			if(Vector3.Distance(gameObject.transform.position, _myShooterPos)>=v_BreakDistance){
				_myShooter.GetComponent<ShootF>().DetachLink(howWasIShot - 1);
			}
		}

		//CECI ETAIT DANS LE FIXED UPDATE
		if(_myShooter!=null){
			_myShooterPos=_myShooter.transform.position;

			//on n'enclenche/ne permet le retour que si la tete du grappin n'est pas posée
			if (GrappedTo == null) {
				//si j'ai rien choppé
				if(Vector3.Distance(gameObject.transform.position, _myShooterPos)>=v_returnDistance){
					shouldIReturn=true;
				}
				
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
