using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Shoot : MonoBehaviour {

	bool playerIndexSet = false;
	public PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;

	private PlayerState _myPlayerState;
	private LinkStrenght _LinksBehavior;
	private HookHead _HookHeadbehavior,_HookHeadbehavior1;

	//tete du grappin
	public GameObject _HookHead;
	public  GameObject _target, _target1;
	//Bool pour permettre déplacement towards
	private bool _Dashing;
	private MovementScript _movement;
	private DrawLink _link;
	private LineRenderer _renderer;

	private float _Force;

	private Vector3 _ThingGrapped;
		
	void Awake(){
		_myPlayerState = GetComponent<PlayerState>();
		_Dashing = false;
		_movement = GetComponent<MovementScript> ();
		_link = GetComponent<DrawLink>();
		_link.enabled = false;

		_Force = 200.0f;
	}

	void FixedUpdate(){
		//dash vers 2
		if(state.Buttons.X == ButtonState.Pressed && _target1 != null){
			MovetowardsHook(_target1);
			_Dashing = true;
		}else{_Dashing = false;}
		//Dash vers 1
		if(state.Buttons.B == ButtonState.Pressed && _target != null){
			MovetowardsHook(_target);
			_Dashing = true;
		}else{_Dashing = false;}
	}

	// Update is called once per frame
	void Update () {
		prevState = state;
		state = GamePad.GetState(playerIndex);

		_renderer = GetComponent<LineRenderer> ();
		_LinksBehavior = GetComponent<LinkStrenght> ();

		_Force =( _LinksBehavior._LinkCommited+1) *200;

		//si la tete de grappin posée alors
		if (_target != null) {
			if (_renderer !=null){_renderer.enabled = true;}
			CreateHook();
			//get ce qui est attaché à la tete de grappin
			_HookHeadbehavior = _target.GetComponent<HookHead>();
		}else{
			_link.enabled = false;
			if (_renderer !=null && _target == null){_renderer.enabled = false; _link.enabled = false;}
		}

		if (_target1 != null) {
			//get ce qui est attaché à la tete de grappin
			_HookHeadbehavior1 = _target1.GetComponent<HookHead>();
		}
		//*******************************************************************INPUTS (sauf dash qui est dans le fixed upadte)
		//tir droit 
		if(state.ThumbSticks.Right.X >0.5 || state.ThumbSticks.Right.X <-0.5||state.ThumbSticks.Right.Y >0.5||state.ThumbSticks.Right.Y<-0.5){
			if(prevState.Triggers.Right == 0 && state.Triggers.Right != 0){
				if(_target != null){
					DetachLink(0);
				}
				Hook();
			}
		}
		//tir gauche
		if(state.ThumbSticks.Right.X >0.5 || state.ThumbSticks.Right.X <-0.5||state.ThumbSticks.Right.Y >0.5||state.ThumbSticks.Right.Y<-0.5){
			if(prevState.Triggers.Left == 0 && state.Triggers.Left != 0){
				if(_target1 != null){
					DetachLink(1);
				}
				Hook1();
			}
		}
		//Annluation des liens 
		if(state.Buttons.A == ButtonState.Pressed && _target != null){
			DetachLink(2);
		}
		//
		if(state.Buttons.RightShoulder == ButtonState.Pressed && _target != null){
			PullTowardsPlayer();
		}
		if(state.Buttons.LeftShoulder == ButtonState.Pressed && _target1 != null){
			PullTowardsPlayer1();
		}
	}

	// Grappin 1 
	void Hook(){
		Vector3 _temp = new Vector3(state.ThumbSticks.Right.X, 0 ,state.ThumbSticks.Right.Y);
		RaycastHit _hit;

		if (Physics.Raycast (transform.position, _temp, out _hit, 100.0f)) {
			_target = Instantiate(_HookHead, _hit.point, Quaternion.identity)as GameObject;
		}
	}

	void MovetowardsHook(GameObject _target){
		//_movement.enabled = false;
		//transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, 100.0f*Time.deltaTime); 

		transform.LookAt(_target.transform);
		rigidbody.AddForce (transform.forward*_Force);

	}

	void PullTowardsPlayer(){
		//Not physics
		//_target.transform.position = Vector3.MoveTowards (_target.transform.position,transform.position,(_Force)*Time.deltaTime);
		//_ThingGrapped = _target.transform.position;
		//_HookHeadbehavior.GrappedTo.transform.position = _ThingGrapped;

		//physics (baisser la force ?)
		_HookHeadbehavior.GrappedTo.gameObject.rigidbody.AddForce ((transform.position - _target.transform.position) * _Force/ 10);
	}

	//création visuelle du lien 
	void CreateHook(){
		_link.enabled = true;
	}

	//detache le(s) liens
	void DetachLink(int _Todestroy){
		//Grappin 1
		if(_Todestroy == 0){
			if(_target != null)Destroy(_target);
			if(_HookHeadbehavior.GrappedTo.gameObject.tag != "Player"){
				Destroy (_HookHeadbehavior.GrappedTo.gameObject.rigidbody);
			}
			if(_HookHeadbehavior1.GrappedTo.gameObject.tag != "Player"){
				Destroy (_HookHeadbehavior1.GrappedTo.gameObject.rigidbody);
			}
		}
		//grappin 2
		if(_Todestroy == 1){
			if(_target1 != null)Destroy(_target1);
			if(_HookHeadbehavior.GrappedTo.gameObject.tag != "Player"){
				Destroy (_HookHeadbehavior.GrappedTo.gameObject.rigidbody);
			}
			if(_HookHeadbehavior1.GrappedTo.gameObject.tag != "Player"){
				Destroy (_HookHeadbehavior1.GrappedTo.gameObject.rigidbody);
			}
		}
		//Tous grappins
		if(_Todestroy == 2){
			if(_target != null)Destroy(_target);
			if(_target1 != null)Destroy(_target1);
			if(_HookHeadbehavior.GrappedTo.gameObject.tag != "Player"){
				Destroy (_HookHeadbehavior.GrappedTo.gameObject.rigidbody);
			}
			if(_HookHeadbehavior1.GrappedTo.gameObject.tag != "Player"){
				Destroy (_HookHeadbehavior1.GrappedTo.gameObject.rigidbody);
			}
		}
	}
	//élément pour le dash 
	void OnCollisionEnter(Collision _Collision){
		//destruction du lien
		if (_Collision.gameObject.tag == "Player" && _Collision.gameObject != gameObject ) {
			//Supprimer tous les liens du joueurs
			if (_Dashing){
				Shoot _PlayerTarget = _Collision.gameObject.GetComponent<Shoot>();
				if(_PlayerTarget != null){
					DetachLink(2);
				}
			}
		}
	}
	//********************DOUBLE SCRIPTUUU
	// Grappin 2
	void Hook1(){
		Vector3 _temp = new Vector3(state.ThumbSticks.Right.X, 0 ,state.ThumbSticks.Right.Y);
		RaycastHit _hit;
		
		if (Physics.Raycast (transform.position, _temp, out _hit, 100.0f)) {
			_target1 = Instantiate(_HookHead, _hit.point, Quaternion.identity)as GameObject;
		}
	}
	void PullTowardsPlayer1(){
		//Not physics
		//_target.transform.position = Vector3.MoveTowards (_target.transform.position,transform.position,(_Force)*Time.deltaTime);
		//_ThingGrapped = _target.transform.position;
		//_HookHeadbehavior.GrappedTo.transform.position = _ThingGrapped;
		
		//physics (baisser la force ?)
		_HookHeadbehavior1.GrappedTo.gameObject.rigidbody.AddForce ((transform.position - _target1.transform.position) * _Force/ 10);
	}
}
