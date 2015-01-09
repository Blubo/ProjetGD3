using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class NewShoot : MonoBehaviour {

	bool playerIndexSet = false;
	public PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;

	private PlayerState _myPlayerState;
	private LinkStrenght _LinksBehavior;

	private float _timer = 1.5f;
	public float _SpeedBullet;

	//tete du grappin
	public GameObject _HookHead, _myHook;
	//ici la target est l'objet touché
	public  GameObject _target, _target1;
	//Bool pour permettre déplacement towards
	[HideInInspector]
	//public bool _Hookheadplaced;
	private bool _Dashing;
	private MovementScript _movement;
	private DrawLink _link;
	private LineRenderer _renderer;

	public float _Force;
	private float _thisForce;	

	void Awake(){
		_myPlayerState = GetComponent<PlayerState>();
		//_Hookheadplaced = false;
		_Dashing = false;
		_movement = GetComponent<MovementScript> ();
		_link = GetComponent<DrawLink>();
		_link.enabled = false;

//		_Force = 200.0f;
	}

	void FixedUpdate(){
		//Dash
		if(state.Buttons.A == ButtonState.Pressed && _target != null){
			MovetowardsHook(_target);
			_Dashing = true;
		}else{
			_Dashing = false;
		}

		//si la tete de grappin posée alors
//		if (_Hookheadplaced) {
//			if (_renderer !=null){
//				_renderer.enabled = true;
//			}
//			CreateHook();
//			
//			//Attirer
			if(state.Buttons.RightShoulder == ButtonState.Pressed && _target != null){
				PullTowardsPlayer(_target);
			}
//		}else{
//			_link.enabled = false;
//			if (_renderer !=null && _target == null){
//				_renderer.enabled = false;
//				_link.enabled = false;
//			}
//		}
	}
	// Update is called once per frame
	void Update () {
		if(prevState.Buttons.B == ButtonState.Released && state.Buttons.B == ButtonState.Pressed){
			Destroy(_myHook);
		}
		prevState = state;
		state = GamePad.GetState(playerIndex);

		_renderer = GetComponent<LineRenderer> ();
		_LinksBehavior = GetComponent<LinkStrenght> ();

//		_Force =( _LinksBehavior._LinkCommited+1) *200;
		_thisForce =( _LinksBehavior._LinkCommited+1) *_Force;

		//tir droit 
		_timer += 1 *Time.deltaTime;
		if(_timer>=1.5f){
			if(state.ThumbSticks.Right.X >0.5 || state.ThumbSticks.Right.X <-0.5||state.ThumbSticks.Right.Y >0.5||state.ThumbSticks.Right.Y<-0.5){
				if(prevState.Triggers.Right == 0 && state.Triggers.Right != 0){
					Hook();
				}
			}
		}

//		//si la tete de grappin posée alors
//		if (_Hookheadplaced) {
//			if (_renderer !=null){
//				_renderer.enabled = true;
//			}
//			CreateHook();
//
//			//Attirer
//			if(state.Buttons.RightShoulder == ButtonState.Pressed && _target != null){
//				PullTowardsPlayer(_target);
//			}
//		}else{
//			_link.enabled = false;
//			if (_renderer !=null && _target == null){
//				_renderer.enabled = false;
//				_link.enabled = false;
//			}
//		}

		if(_target == null){
			//_Hookheadplaced = false;
		}
	}



	// Grappin 1 
	void Hook(){
//		Vector3 _temp = new Vector3(state.ThumbSticks.Right.X, 0 ,state.ThumbSticks.Right.Y);
//		RaycastHit _hit;
//
//		if (Physics.Raycast (transform.position, _temp, out _hit, 100.0f)) {
//			if (_target != null){
//				DetachChildren();
//			}
//			_target = Instantiate(_HookHead, _hit.point, Quaternion.identity)as GameObject;
//
//			_Hookheadplaced = true;
//		}
		//manquant:
		//DetachChildren();
		//le placage de la tete du grappin à l'endroit de l'impact
		//hookheadplaced=true

		//SWITCH THE COMMENTS ON THE TWO NEXT LINES FOR MANAGING THE WIDTH OF THE AVATAR
//		GameObject newBullet = Instantiate(_HookHead, transform.TransformPoint(0f,0f,0f), transform.rotation) as GameObject;
		_myHook = Instantiate(_HookHead, transform.TransformPoint(0f,0f,0f), transform.rotation) as GameObject;

//		GameObject newBullet = Instantiate(_HookHead, transform.TransformPoint(0f,0f,4f), transform.rotation) as GameObject;

//		Rigidbody rb = newBullet.GetComponent<Rigidbody>();
		Rigidbody rb = _myHook.GetComponent<Rigidbody>();

		if (rb != null)	rb.AddForce(gameObject.transform.forward* _SpeedBullet);
//		newBullet.GetComponent<GrappleComebackLouis5Janv>()._myShooter=gameObject;
//		newBullet.GetComponent<NewHookHead>()._myShooter=gameObject;
		_myHook.GetComponent<NewHookHead>()._myShooter=gameObject;

//		_myHook.GetComponent<NewHookHead>().howWasIShot=1;
//		_myHook.GetComponent<NewHookHead>().howWasIShot=2;
		_timer = 0;
	}
	
	void MovetowardsHook(GameObject _target){
		//_movement.enabled = false;
//		transform.LookAt(_target.transform);
		transform.LookAt(_myHook.transform);

		//transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, 100.0f*Time.deltaTime); 
		rigidbody.AddForce (transform.forward*_thisForce);

		if(Vector3.Distance(transform.position, _target.transform.position)<1.0f){
			//on enlève l'enfant et détruis le grappin 
			DetachChildren();
			//Moyen d'en faire une fonction pour randre ça plus propre
			//_Hookheadplaced = false;
			_renderer.enabled = false;
			_movement.enabled = true;
		}
	}

	void PullTowardsPlayer(GameObject _target){
//		_target.transform.position = Vector3.MoveTowards (_target.transform.position,transform.position,(_Force)*Time.deltaTime);
//		_myHook.transform.position = Vector3.MoveTowards (_target.transform.position,transform.position,(_Force)*Time.deltaTime);
		_target.transform.position = Vector3.MoveTowards (_target.transform.position,transform.position,(_thisForce)*Time.deltaTime);
		_myHook.transform.position = Vector3.MoveTowards (_myHook.transform.position,transform.position,(_thisForce)*Time.deltaTime);

		if(Vector3.Distance(transform.position, _target.transform.position)<1.0f){
			DetachChildren();
			//Moyen d'en faire une fonction pour randre ça plus propre
			//_Hookheadplaced = false;
			_renderer.enabled = false;
			_movement.enabled = true;
		}
	}
	
	void CreateHook(){
		_link.enabled = true;
	}

	//detache le(s) liens
	void DetachChildren(){
		Debug.Log("detached child 1 (so number2)");
		//pour un seul enfant yet(a modifier ici ou autre part) 
		if (_target != null ){
			Transform _child = _target.transform.GetChild(1);
			if (_child != null ){
				LinkStrenght _linkcount = _child.gameObject.GetComponent<LinkStrenght>();
				if(_linkcount != null){
					_linkcount._LinkCommited -= 1 ;
				}
				_child.transform.parent = null;
				Destroy(_target);
			}else {
				Destroy(_target);
			}
		}
	}

	void OnCollisionEnter(Collision _Collision){
		//destruction du lien
		if (_Collision.gameObject.tag == "Player" && _Collision.gameObject != gameObject ) {
			//Supprimer tous les liens du joueurs
			if (_Dashing){
				NewShoot _PlayerTarget = _Collision.gameObject.GetComponent<NewShoot>();
				if(_PlayerTarget != null){
					_PlayerTarget.DetachChildren();
				}
			}
		}
	}
}
