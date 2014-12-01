﻿using UnityEngine;
using System.Collections;
using XInputDotNetPure;


public class MovementScript : MonoBehaviour {

	bool playerIndexSet = false;
	public PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;

	private float _movementSpeed;

	//Dash 
	private float _DashTimer;
	private float _DashTimerCD;

	private float _DashDuree;
	private float _DashCD;
	private bool _StopDash;


	//Delegate pour enlever et remettre 
	//delegate void Mydelegate();
	//Mydelegate Actions ;

	// Use this for initialization
	void Start () {
		_movementSpeed=10f;

		_DashDuree = 0.3f;
		_DashTimer = _DashDuree;
		_DashCD = 0.5f;
		_DashTimerCD = _DashCD;
		_StopDash = false;
	}
	
	// Update is called once per frame
	void Update () {
		//Timer du CD du dash qui décroit 
		if(_DashTimer> -1.0f){_DashTimer -= 1.0f*Time.deltaTime;}
		if(_DashTimerCD> -1.0f){_DashTimerCD -= 1.0f*Time.deltaTime;}

//		if (!playerIndexSet || !prevState.IsConnected)
//		{
//			for (int i = 0; i < 4; ++i)
//			{
//				PlayerIndex testPlayerIndex = (PlayerIndex)i;
//				GamePadState testState = GamePad.GetState(testPlayerIndex);
//				if (testState.IsConnected)
//				{
//					Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
//					playerIndex = testPlayerIndex;
//					playerIndexSet = true;
//				}
//			}
//		}
		prevState = state;
		state = GamePad.GetState(playerIndex);

		//Ajout d'un rigidbody
		if(rigidbody == null){
			gameObject.AddComponent<Rigidbody>();
			gameObject.rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationZ;
		}
		//Appui sur X = Dash 
		if (_DashTimer <= 0.0f) {
			if (_StopDash == true){gameObject.rigidbody.velocity = Vector3.zero; _StopDash = false;}
			Movement ();
			gameObject.rigidbody.mass = 1.0f;
		}
		if (prevState.Buttons.X == ButtonState.Released && state.Buttons.X == ButtonState.Pressed) {
			if(_DashTimerCD <= 0.0f){
				//Durant le dash on peut faire des tests colliders pour envoyer les gens plus loin, arreter le dash avant son intérgité etc.
				Dash ();
				//AR = (state.ThumbSticks.Left.X * _movementSpeed , 0.0f, state.ThumbSticks.Left.Y * _movementSpeed );
				_DashTimerCD =_DashCD;
			}else {Debug.Log("dash non dispo");}
		}
	}
	//Movement de Base avec joystick
	void Movement(){
		
		Vector3 movement = new Vector3(state.ThumbSticks.Left.X * _movementSpeed * Time.deltaTime, 0.0f, state.ThumbSticks.Left.Y * _movementSpeed * Time.deltaTime );
		transform.localPosition += movement;
		
		transform.eulerAngles=new Vector3(0,0,transform.eulerAngles.z);
	}

	//Action de dash 
	void Dash(){
		//possibilité de changer la hitbox pour faciliter le renre dedans
		gameObject.rigidbody.mass = 500.0f;
		gameObject.rigidbody.AddForce (new Vector3(state.ThumbSticks.Left.X * _movementSpeed , 0.0f, state.ThumbSticks.Left.Y * _movementSpeed)*1000.0f, ForceMode.Impulse);
		_StopDash = true;
		_DashTimer = _DashDuree;
	}
}
