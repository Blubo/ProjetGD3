using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PauseManager : MonoBehaviour {

	bool playerIndexSet = false;
	public PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(prevState.Buttons.Start == ButtonState.Released && state.Buttons.Start == ButtonState.Pressed){
			Pause();
		}


		prevState = state;
		state = GamePad.GetState(playerIndex);
	}

	void Pause(){
		Debug.Log("pause");
		Camera.main.GetComponent<MusicManagerFMOD>().PlayMyMusic();
	}
}
