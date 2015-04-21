using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

public class ShootF : MonoBehaviour {
	
	bool playerIndexSet = false;
	public PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;
	
	private PlayerState _myPlayerState;
	private LinkStrenght _LinksBehavior;

	private float _timer = 1.5f;
	//v_oldDashingForce = une variable rajoutée le 2 dévrier qui commence à 1 de base et qui détermine la force de dash du joueur via input continu
	public float v_SpeedBullet, v_coolDown, v_sizeRatio, v_sizeGrowth;
	
	[HideInInspector]
	public float _initSizeRatio;
	
	//tete du grappin
	public GameObject _HookHead, v_instantiateur;
	
	//	[HideInInspector]
	//	public Vector3 _instantiatePos;
	
	[HideInInspector]
	public  GameObject _target, _myHook;
	//Bool pour permettre déplacement towards
	[HideInInspector]
	public bool _Dashing, _DashingTest;
	
	public float _Force;
	private float _thisForce;	
	private float _dashingDistance;
	
	private Vector3 _ThingGrapped;
	
	public AudioClip v_linkShot, v_linkBroken;
	public List<AudioClip> v_linkShotList = new List<AudioClip>();
	private int _whatSoundToPlay;
	
	[Space(15)]
	[Header("Dash")]
	[Tooltip("Check to change dashing system")]
	public bool _alternateDash;
	
	[Tooltip("change old dashing force")]
	public float v_oldDashingForce;
	[Tooltip("change new dashing force")]
	public float v_NewDashingForce;
	
	void Awake(){
		_alternateDash=false;
		_whatSoundToPlay=0;
		v_sizeRatio=0.5f;
		_initSizeRatio = v_sizeRatio;
		_myPlayerState = GetComponent<PlayerState>();
		_Dashing = false;
		_DashingTest = false;
	}
	
	// Update is called once per frame
	void Update () {
		//tir droit 
		_timer += Time.deltaTime;
		if(_timer>=v_coolDown){
			if(prevState.Triggers.Right == 0 && state.Triggers.Right != 0){
				if(_target != null){
					DetachLink(0);
				}
				Hook();
			}
		}
		
		if (prevState.Triggers.Right != 0 && state.Triggers.Right != 0 ) {
		} else {
			if(_target != null){
				DetachLink(0);
			}
		}
		
		prevState = state;
		state = GamePad.GetState(playerIndex);
		_LinksBehavior = GetComponent<LinkStrenght> ();
		
		if(gameObject.GetComponent<Rigidbody>().velocity.magnitude <= 45f){
			_DashingTest = false;
		}
		
		_thisForce = (_LinksBehavior._LinkCommited+1) *_Force;
	}
	
	// Grappin 1 
	void Hook(){
		//ANCIEN AUDIO SANS FMOD
//		_whatSoundToPlay = Random.Range(0, v_linkShotList.Count);
//		GetComponent<AudioSource>().PlayOneShot(v_linkShotList[_whatSoundToPlay]);

		//ON JOUE LE SON FMOD ICI POUR LE TIR DU LIEN 


		//droite
		_myHook = Instantiate(_HookHead, v_instantiateur.transform.position, transform.rotation) as GameObject;
		Rigidbody rb = _myHook.GetComponent<Rigidbody>();
		if (rb != null)	rb.AddForce(gameObject.transform.forward* v_SpeedBullet * 1000);
		_myHook.GetComponent<HookHeadF>()._myShooter=gameObject;
		_myHook.GetComponent<HookHeadF>().howWasIShot=1;
		_timer = 0;
	}
	
	//detache le(s) liens
	public void DetachLink(int _Todestroy){
		GetComponent<AudioSource>().PlayOneShot(v_linkBroken);
		
		//Grappin 1
		if(_Todestroy == 0){
			
			if(_target != null){
			//	if(Vector3.Distance(gameObject.transform.position, _myHook.transform.position)>=gameObject.GetComponent<ElasticScript>().v_tensionLessDistanceRatio*_myHook.GetComponent<HookHeadF>().v_BreakDistance){
          Vector3 direction = _myHook.transform.position-gameObject.transform.position;
					direction.Normalize();
					//gameObject.GetComponent<ElasticScript>().ElasticBreak(direction, gameObject.GetComponent<ElasticScript>()._breaking1);
			//	}
				Destroy(_myHook);
			}
			
			if(_target.gameObject.tag!="Player"){
				if(_target.gameObject.GetComponent<Sticky>()!=null){
					_target.gameObject.GetComponent<Sticky>().v_numberOfLinks-=1;
				}
			}
			
			if(_target.gameObject.tag=="Player"){
				if(_target.gameObject.GetComponent<LinkStrenght>()!=null){
					_target.gameObject.GetComponent<LinkStrenght>()._LinkCommited-=1;
					gameObject.GetComponent<LinkStrenght>()._LinkCommited-=1;
				}
			}
			_target=null;
		}
	}
	
	//élément pour le dash 
	void OnCollisionEnter(Collision _Collision){
		//destruction du lien
		if (_Collision.gameObject.transform.Find("ApparenceAvatar")!=null){
			if (_Collision.gameObject.transform.Find("ApparenceAvatar").tag == "Player" && _Collision.gameObject != gameObject ) {
				//Supprimer tous les liens du joueurs
				if (_Dashing){
					//				if (_DashingTest){
					ShootF _PlayerTarget = _Collision.gameObject.GetComponent<ShootF>();
					if(_PlayerTarget != null){
						_Collision.gameObject.GetComponent<ShootF>().SendMessage("DetachLink", 2);
					}
				}
			}	
		}
	}
}
