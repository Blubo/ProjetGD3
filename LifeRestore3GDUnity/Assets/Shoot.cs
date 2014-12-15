using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Shoot : MonoBehaviour {

	bool playerIndexSet = false;
	public PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;

	private PlayerState _myPlayerState;

	//tete du grappin
	public GameObject _HookHead;
	public  GameObject _target;
	//Bool pour permettre déplacement towards
	private bool _Hookheadplaced;
	private MovementScript _movement;
	private DrawLink _link;

	void Awake(){
		_myPlayerState = GetComponent<PlayerState>();
		_Hookheadplaced = false;
		_movement = GetComponent<MovementScript> ();
		_link = GetComponent<DrawLink>();
		_link.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		prevState = state;
		state = GamePad.GetState(playerIndex);
		//tir 
		if(state.ThumbSticks.Right.X >0.5 || state.ThumbSticks.Right.X <-0.5||state.ThumbSticks.Right.Y >0.5||state.ThumbSticks.Right.Y<-0.5){
			//Si gachette droite alors tir grappin
			if(prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed){
				Hook();
			}
		}
		//si la tete de grappin posée alors
		if (_Hookheadplaced) {
			if(state.Triggers.Right != 0){
				MovetowardsHook(_target);
				CreateHook();
			}
		}else{_link.enabled = false;}
	}

	void Hook(){
		Vector3 _temp = new Vector3(state.ThumbSticks.Right.X, 0 ,state.ThumbSticks.Right.Y);
		RaycastHit _hit;

		if (Physics.Raycast (transform.position, _temp, out _hit, 100.0f)) {
			_target = Instantiate(_HookHead, _hit.point, Quaternion.identity)as GameObject;
			_Hookheadplaced = true;
		}
	}

	void MovetowardsHook(GameObject _target){
		_movement.enabled = false;
		transform.LookAt(_target.transform);
		transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, 10.0f*Time.deltaTime); 

		if(Vector3.Distance(transform.position, _target.transform.position)<0.5f){
			_Hookheadplaced = false;
			_movement.enabled = true;
		}
	}

	void CreateHook(){
		_link.enabled = true;
	}
}
