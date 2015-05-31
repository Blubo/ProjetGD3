using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

public class MenuEnd : MonoBehaviour {
	
	public GameObject rejouer, quitter;
	private List<GameObject> cases;
	private GameObject selected, unselected;
	private int selectedNumber, unselectedNumber;

	public bool playerIndexSet = false;
	public PlayerIndex playerIndex;
	public GamePadState state;
	public GamePadState prevState;

	private bool inputsAllowed;

	private GameObject alphaFeather;
	[HideInInspector]
	public AlphaManager _alphaManager;

	void Awake(){
		alphaFeather =  GameObject.Find("PlumeAlpha");
		if(alphaFeather!=null){
			_alphaManager = alphaFeather.GetComponent<AlphaManager>();
//			Debug.Log("well");
		}
	}

	// Use this for initialization
	void Start () {
		selectedNumber = 0;
		unselectedNumber = 1;

		cases = new List<GameObject>();
		cases.Add(rejouer);
		cases.Add(quitter);
	}
	
	// Update is called once per frame
	void Update (){
		PlayerIndex playerIndex = (PlayerIndex)0;
		prevState = state;
		state = GamePad.GetState(playerIndex);

		selected = cases[selectedNumber];
		unselected = cases[unselectedNumber];

		if(selected!=null && unselected != null){
			OnOff(selected, unselected);
		}		
			
		if(inputsAllowed == true){
			if(prevState.ThumbSticks.Left.X==0 && state.ThumbSticks.Left.X>0 || prevState.ThumbSticks.Left.X==0 && state.ThumbSticks.Left.X<0){
				if(selectedNumber==0){
					selectedNumber = 1;
					unselectedNumber = 0;
				}else if(selectedNumber==1){
					selectedNumber = 0;
					unselectedNumber = 1;
				}
			}

			if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed){
				if(selectedNumber==0)Application.LoadLevel(0);
				if(selectedNumber==1)Application.Quit();
			}
		}
	}
	
	void OnOff(GameObject selectedItem, GameObject unselectedItem){
		selectedItem.GetComponent<Pulsing>().enabled = true;
		unselectedItem.GetComponent<Pulsing>().enabled = false;
	}

	public void AllowInputs(){
		inputsAllowed = true;
	}

	public void AlphaBonus(){
		int _playerNumber = _alphaManager.posessor.GetComponent<Player_Status>().playerNumber;
		if(_playerNumber == 1){
			PlayerPrefs.SetInt("ScoreGreen", _alphaManager.scoreBonusFlat);
		}else if(_playerNumber == 2){
			PlayerPrefs.SetInt("ScoreRed", _alphaManager.scoreBonusFlat);
		}else if(_playerNumber == 3){
			PlayerPrefs.SetInt("ScoreBlue", _alphaManager.scoreBonusFlat);
		}
	}
}
