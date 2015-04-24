using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Dash : MonoBehaviour {

	bool playerIndexSet = false;
	public PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;

	private bool _IsDashing;
	private Vector3 _Direction;
	private Vector3 _PositionInit;
	private float _DistanceParcourue;

	//Les valeurs que les autres peuvent voir 
	public float _VitesseInitiale;
	public float _Distance;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		prevState = state;
		state = GamePad.GetState(playerIndex);

		//Debug.Log(_IsDashing);
		//Debug.Log(_Distance);

//		if (prevState.Buttons.Y == ButtonState.Released && state.Buttons.Y == ButtonState.Pressed) {
		if(prevState.Triggers.Left == 0 && state.Triggers.Left != 0){

			if(!_IsDashing){
				_Direction = gameObject.transform.forward ;
				_PositionInit = gameObject.transform.position;
				_IsDashing = true;
			}		
		}

		if(_IsDashing){
			Dash_Movement();
		}
	}

	void Dash_Movement(){
		gameObject.GetComponent<Rigidbody>().AddForce (_Direction * _VitesseInitiale, ForceMode.Impulse);

		_DistanceParcourue += 1.0f * Time.deltaTime;
		if(_DistanceParcourue>= _Distance){
				gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
				_IsDashing = false;
				_DistanceParcourue = 0.0f;
		}
	}
}
