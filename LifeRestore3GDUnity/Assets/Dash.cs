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

		Debug.Log(_IsDashing);
		//Debug.Log(_Distance);

		if (prevState.Buttons.X == ButtonState.Released && state.Buttons.X == ButtonState.Pressed) {
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
		gameObject.rigidbody.AddForce (_Direction * _VitesseInitiale, ForceMode.Impulse);
		float _DistanceParcourue = Vector3.Distance (_PositionInit, gameObject.transform.position);

		if(_DistanceParcourue>= _Distance){
				gameObject.rigidbody.velocity = Vector3.zero;
				_IsDashing = false;
		}
	}
}
