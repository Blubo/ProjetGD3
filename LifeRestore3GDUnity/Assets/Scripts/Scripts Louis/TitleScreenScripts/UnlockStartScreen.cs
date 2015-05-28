using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class UnlockStartScreen : MonoBehaviour {

	bool playerIndexSet = false;
	public PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;

	private bool locked = true;

	[SerializeField]
	private GameObject[] players;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(locked == true){
			if (prevState.Buttons.Start == ButtonState.Released && state.Buttons.Start == ButtonState.Pressed ) {
				locked = false;

				UnlockScreen();
			}
		}
		prevState = state;
		state = GamePad.GetState(playerIndex);
	}

	void UnlockScreen(){
		for (int i = 0; i < players.Length; i++) {
			players[i].GetComponent<MovementScript5Janv>().enabled = true;
			players[i].GetComponent<ShootF>().enabled = true;
		}

		Camera.main.GetComponent<TitleScreenCameraManager>().locked = false;

	}
}
