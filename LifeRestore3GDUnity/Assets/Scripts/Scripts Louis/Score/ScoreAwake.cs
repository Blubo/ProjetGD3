using UnityEngine;
using System.Collections;

public class ScoreAwake : MonoBehaviour {

	public int scoreToSetGreen, scoreToSetRed, scoreToSetBlue;

	void Awake(){
		PlayerPrefs.SetInt("ScoreGreen", scoreToSetGreen);
		PlayerPrefs.SetInt("ScoreRed", scoreToSetRed);
		PlayerPrefs.SetInt("ScoreBlue", scoreToSetBlue);

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		if(Input.GetKeyDown(KeyCode.A)){
//			Debug.Log("a");
//			PlayerPrefs.SetInt("ScoreGreen", 100);
//			PlayerPrefs.SetInt("ScoreRed", 60);
//			PlayerPrefs.SetInt("ScoreBlue", 50);
//		}
	}
}
