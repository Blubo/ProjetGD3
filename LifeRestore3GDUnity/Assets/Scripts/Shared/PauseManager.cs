using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

public class PauseManager : MonoBehaviour {
	
	public GameObject continuer, recommencer, option, retourLevels, quitter;
	private List<GameObject> cases;
	private GameObject selected, unselected;
	private int selectedNumber, unselectedNumber;
	
	private bool inputsAllowed;
	
	bool playerIndexSet = false;
	public PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;
	
	private bool InPause;
	
	[SerializeField]
	private GameObject pauseScreen;
	
	// Use this for initialization
	void Start () {
		inputsAllowed = false;
		InPause = false;
		selectedNumber = 0;
		unselectedNumber = 1;
		
		cases = new List<GameObject>();
		cases.Add(continuer);
		cases.Add(recommencer);
		cases.Add(option);
		cases.Add(retourLevels);
		cases.Add(quitter);
	}
	
	
	// Update is called once per frame
	void Update () {
		prevState = state;
		state = GamePad.GetState(playerIndex);
		//PAUSE SYSTEM
		if(prevState.Buttons.Start == ButtonState.Released && state.Buttons.Start == ButtonState.Pressed && InPause == false){
			Pause();
		}else
		if (prevState.Buttons.Start == ButtonState.Released && state.Buttons.Start == ButtonState.Pressed && InPause == true){
			UnPause();
		}
		
		//PAUSE INPUTS
		selected = cases[selectedNumber];
		unselected = cases[unselectedNumber];
		
		
		
		if(inputsAllowed == true){
			if(prevState.ThumbSticks.Left.Y==0 && state.ThumbSticks.Left.Y>0){
				if(selectedNumber==0){
					selectedNumber = 4;
					unselectedNumber = 0;
				}else if(selectedNumber==1){
					selectedNumber = 0;
					unselectedNumber = 1;
				}else if(selectedNumber==2){
					selectedNumber = 1;
					unselectedNumber = 2;
				}else if(selectedNumber==3){
					selectedNumber = 2;
					unselectedNumber = 3;
				}else if(selectedNumber==4){
					selectedNumber = 3;
					unselectedNumber = 4;
				}
			}else if(prevState.ThumbSticks.Left.Y==0 && state.ThumbSticks.Left.Y<0){
				if(selectedNumber==0){
					selectedNumber = 1;
					unselectedNumber = 0;
				}else if(selectedNumber==1){
					selectedNumber = 2;
					unselectedNumber = 1;
				}else if(selectedNumber==2){
					selectedNumber = 3;
					unselectedNumber = 2;
				}else if(selectedNumber==3){
					selectedNumber = 4;
					unselectedNumber = 3;
				}else if(selectedNumber==4){
					selectedNumber = 0;
					unselectedNumber = 4;
				}
			}
			
			if(selected!=null && unselected != null){
				//			OnOff(selected, unselected);
				selected.SetActive(true);
				unselected.SetActive(false);
			}
			
			if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed){
				//continuer
				if(selectedNumber==0){
					UnPause();
					return;
				}
				
				//				recommencer
				if(selectedNumber==1){
					UnPause();
					Application.LoadLevel(Application.loadedLevelName);
					return;
				}
				//option
				//				if(selectedNumber==2)Application.LoadLevel(1);
				//ecran titre
				
				if(selectedNumber==3){
					UnPause();
					Application.LoadLevel(0);
					return;
				}
				//quitter
				if(selectedNumber==4){
					UnPause();
					Application.Quit();
					return;
				}
			}
		}
	}
	
	//PAUSE SYSTEM
	void Pause(){
		inputsAllowed = true;
		InPause = true;
		pauseScreen.SetActive(true);
		Time.timeScale = 0.0f;
		
		//		Debug.Log("pause");
		//		Camera.main.GetComponent<MusicManagerFMOD>().PlayMyMusic();
	}
	
	void UnPause(){
		inputsAllowed = false;
		InPause = false;
		pauseScreen.SetActive(false);
		Time.timeScale = 1.0f;
		
		//		Debug.Log("unpause");
		//		Camera.main.GetComponent<MusicManagerFMOD>().PlayMyMusic();
	}
	
	//PAUSE INPUTS
	void OnOff(GameObject selectedItem, GameObject unselectedItem){
		selectedItem.GetComponent<Pulsing>().enabled = true;
		unselectedItem.GetComponent<Pulsing>().enabled = false;
	}
	
	public void AllowInputs(){
		inputsAllowed = true;
	}
}
