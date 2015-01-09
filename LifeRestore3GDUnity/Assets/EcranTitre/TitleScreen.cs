using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class TitleScreen : MonoBehaviour {
	//Xinput
	bool playerIndexSet = false;
	public PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;
	
	private PlayerState _myPlayerState;

	void Awake(){
		_myPlayerState = GetComponent<PlayerState>();
	}

	void Update () {
		//Xinput
		prevState = state;
		state = GamePad.GetState(playerIndex);

		//Si le joueur 1 Appuie sur A alors on lance la partie
		if(prevState.Buttons.A ==ButtonState.Released && state.Buttons.A == ButtonState.Pressed){
			Application.LoadLevel("Vir_Scene_3DgamesceneCopieLouis");
		}
	}
}
