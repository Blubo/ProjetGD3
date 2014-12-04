using UnityEngine;
using System.Collections;
using XInputDotNetPure;


public class ArrowFollow : MonoBehaviour {

	public float v_movementSpeed;
	private float _rightStickX, _rightStickY;
	private Vector3 previousVectorMov, previousVectorRot;
	private Quaternion _oldRot;


	public PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;


	// Use this for initialization
	void Start () {
		_oldRot=this.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
	
		prevState = state;
		state = GamePad.GetState(playerIndex);

		Vector3 _temp = new Vector3(state.ThumbSticks.Right.X, 0 ,state.ThumbSticks.Right.Y);

		_rightStickX=state.ThumbSticks.Right.X;
		_rightStickY=state.ThumbSticks.Right.Y;

		//http://blog.rastating.com/creating-a-2d-rotating-aim-assist-in-unity/
	
//		si je vise pas
//		if(state.ThumbSticks.Right.X==0 && state.ThumbSticks.Right.Y==0){
//			this.transform.rotation = _oldRot;
//		}
		if(state.ThumbSticks.Right.X!=0 && state.ThumbSticks.Right.Y!=0){
			Vector3 player_pos = Camera.main.WorldToScreenPoint(this.transform.position);

			float angle = Mathf.Atan2 (_rightStickY, _rightStickX) * Mathf.Rad2Deg;
			this.transform.rotation = Quaternion.Euler (new Vector3(0, -angle, 0));
			//_oldRot = this.transform.rotation;
		}

	}
}
