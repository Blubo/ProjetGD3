using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

public class LaunchMenu : MonoBehaviour {

	public GameObject start, newGame, highscores, quitGame;
	//public GameObject howTo;
	
	public List<GameObject> cases;
	private GameObject selected;
	private int selectedNumber, currentLVL;
	private bool splash;
	
	public bool playerIndexSet = false;
	public PlayerIndex playerIndex;
	public GamePadState state;
	public GamePadState prevState;
	
	// Use this for initialization
	void Start () {
		currentLVL=0;
		cases = new List<GameObject>();
		cases.Add(newGame);
		cases.Add(highscores);
		//cases.Add(howTo);
		cases.Add(quitGame);
		
		splash=true;
		
		start.GetComponent<GUIText>().fontSize = 30;
		start.GetComponent<GUIText>().alignment = TextAlignment.Center;
		start.GetComponent<GUIText>().anchor = TextAnchor.MiddleCenter;
		start.transform.position = new Vector3(0.5f,0.2f,0f);
		start.GetComponent<GUIText>().text = "Press Start Button";
		
		newGame.GetComponent<GUIText>().fontSize = 30;
		newGame.GetComponent<GUIText>().alignment = TextAlignment.Center;
		newGame.GetComponent<GUIText>().anchor = TextAnchor.MiddleCenter;
		newGame.transform.position = new Vector3(0.5f,0.4f,0f);
		newGame.GetComponent<GUIText>().text = "New Game";
		
		highscores.GetComponent<GUIText>().fontSize = 30;
		highscores.GetComponent<GUIText>().alignment = TextAlignment.Center;
		highscores.GetComponent<GUIText>().anchor = TextAnchor.MiddleCenter;
		highscores.transform.position = new Vector3(0.5f,0.3f,0f);
		highscores.GetComponent<GUIText>().text = "Highscores";
		
		/*howTo.guiText.fontSize = 30;
		howTo.guiText.alignment = TextAlignment.Center;
		howTo.guiText.anchor = TextAnchor.MiddleCenter;
		howTo.transform.position = new Vector3(0.5f,0.3f,0f);
		howTo.guiText.text = "How to play";*/
		
		quitGame.GetComponent<GUIText>().fontSize = 30;
		quitGame.GetComponent<GUIText>().alignment = TextAlignment.Center;
		quitGame.GetComponent<GUIText>().anchor = TextAnchor.MiddleCenter;
		quitGame.transform.position = new Vector3(0.5f,0.2f,0f);
		quitGame.GetComponent<GUIText>().text = "quit game";
		
		newGame.SetActive(false);
		highscores.SetActive(false);
		//howTo.SetActive(false);
		quitGame.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		PlayerIndex playerIndex = (PlayerIndex)0;
		prevState = state;
		state = GamePad.GetState(playerIndex);
		
		start.GetComponent<GUIText>().fontSize = 30;
		newGame.GetComponent<GUIText>().fontSize = 30;
		highscores.GetComponent<GUIText>().fontSize = 30;
		//howTo.guiText.fontSize = 30;
		quitGame.GetComponent<GUIText>().fontSize = 30;
		
		selected = cases[selectedNumber];
		
		if(selected!=null){
			highLight(selected);
		}
		
		//		if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed){
		//			Application.LoadLevel("Playtest1");
		//		}
		
		
		if (prevState.Buttons.Start == ButtonState.Released && state.Buttons.Start == ButtonState.Pressed){
			if(splash==true){
				selectedNumber = 0;
				start.SetActive(false);
				newGame.SetActive(true);
				highscores.SetActive(true);
				//howTo.SetActive(true);
				quitGame.SetActive(true);
				
				splash=false;
			}
		}
		
		if(splash==false){
			//			if(prevState.ThumbSticks.Left.Y==0 && state.ThumbSticks.Left.Y>0){
			//				if(selectedNumber==0) selectedNumber = 3;
			//				else if(selectedNumber==1) selectedNumber = 0;
			//				else if(selectedNumber==2) selectedNumber = 1;
			//				else if(selectedNumber==3) selectedNumber = 2;
			//			}
			//
			//			if(prevState.ThumbSticks.Left.Y==0 && state.ThumbSticks.Left.Y<0){
			//				if(selectedNumber==0) selectedNumber = 1;
			//				else if(selectedNumber==1) selectedNumber = 2;
			//				else if(selectedNumber==2) selectedNumber = 3;
			//				else if(selectedNumber==3) selectedNumber = 0;
			//			}
			
			if(prevState.ThumbSticks.Left.Y==0 && state.ThumbSticks.Left.Y>0){
				if(selectedNumber==0) selectedNumber = 2;
				else if(selectedNumber==1) selectedNumber = 0;
				else if(selectedNumber==2) selectedNumber = 1;
			}
			
			if(prevState.ThumbSticks.Left.Y==0 && state.ThumbSticks.Left.Y<0){
				if(selectedNumber==0) selectedNumber = 1;
				else if(selectedNumber==1) selectedNumber = 2;
				else if(selectedNumber==2) selectedNumber = 0;
			}
			
			if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed){
				if(selectedNumber==0)Application.LoadLevel("CountDown pregame");
				if(selectedNumber==1){
					PlayerPrefs.SetInt("LastLevel",currentLVL);
					Application.LoadLevel("Highscores");
				}
				if(selectedNumber==2)Application.Quit();
			}
		}
	}
	
	void highLight(GameObject selected){
		selected.GetComponent<GUIText>().fontSize = 50;
	}
}
