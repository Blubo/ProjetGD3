using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PauseManager : MonoBehaviour {

	bool playerIndexSet = false;
	public PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;

  private bool InPause;
	// Use this for initialization
	void Start () {
    InPause = false;
	}
	
	// Update is called once per frame
	void Update () {
    		prevState = state;
		state = GamePad.GetState(playerIndex);

		if(prevState.Buttons.Start == ButtonState.Released && state.Buttons.Start == ButtonState.Pressed && InPause == false){
			Pause();
		}else
    if (prevState.Buttons.Start == ButtonState.Released && state.Buttons.Start == ButtonState.Pressed && InPause == true){
      UnPause();
    }


	}

	void Pause(){
    InPause = true;
		Debug.Log("pause");
    Time.timeScale = 0.0f;
		Camera.main.GetComponent<MusicManagerFMOD>().PlayMyMusic();
	}
void UnPause(){
  InPause = false;
  Debug.Log("unpausepause");
  Time.timeScale = 1.0f;
 // Camera.main.GetComponent<MusicManagerFMOD>().PlayMyMusic();
  }
}
