using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class ChangeLevelDebug : MonoBehaviour {

	bool playerIndexSet = false;
	public PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(prevState.Buttons.X == ButtonState.Released && state.Buttons.X == ButtonState.Pressed){
			//		if(prevState.Buttons.Back == ButtonState.Released && state.Buttons.Back == ButtonState.Pressed){
			Application.LoadLevel(Application.loadedLevel-1);
		}

		if(prevState.Buttons.A == ButtonState.Released && state.Buttons.Y == ButtonState.Pressed){
//		if(prevState.Buttons.Back == ButtonState.Released && state.Buttons.Back == ButtonState.Pressed){
			Application.LoadLevel(Application.loadedLevelName);
		}
		
		if(prevState.Buttons.B == ButtonState.Released && state.Buttons.B == ButtonState.Pressed){
//		if(prevState.Buttons.Start == ButtonState.Released && state.Buttons.Start == ButtonState.Pressed){
			Application.LoadLevel(Application.loadedLevel+1);
		}


		prevState = state;
		state = GamePad.GetState(playerIndex);
	}
}
