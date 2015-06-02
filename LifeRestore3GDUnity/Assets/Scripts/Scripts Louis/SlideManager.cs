using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

public class SlideManager : MonoBehaviour {

	bool playerIndexSet = false;
	public PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;
	
	private bool inSlide;
	
	[SerializeField]
	private GameObject slideScreen;
	
	// Use this for initialization
	void Start () {
		inSlide = false;
	}
	
	// Update is called once per frame
	void Update () {
		prevState = state;
		state = GamePad.GetState(playerIndex);
		
		if(prevState.Buttons.Back == ButtonState.Released && state.Buttons.Back == ButtonState.Pressed && inSlide == false){
			Slide();
		}else
		if (prevState.Buttons.Back == ButtonState.Released && state.Buttons.Back == ButtonState.Pressed && inSlide == true){
			UnSlide();
		}
	}
	
	//PAUSE SYSTEM
	void Slide(){
		inSlide = true;
		slideScreen.SetActive(true);
		Time.timeScale = 0.0f;
		
		//		Debug.Log("pause");
		//		Camera.main.GetComponent<MusicManagerFMOD>().PlayMyMusic();
	}
	
	void UnSlide(){
		inSlide = false;
		slideScreen.SetActive(false);
		Time.timeScale = 1.0f;
		
		//		Debug.Log("unpause");
		//		Camera.main.GetComponent<MusicManagerFMOD>().PlayMyMusic();
	}
}
