using UnityEngine;
using System.Collections;

public class ScoreAwake : MonoBehaviour {

	public int scoreToSet;

	void Awake(){
		PlayerPrefs.SetInt("ScoreGreen", scoreToSet);
		PlayerPrefs.SetInt("ScoreRed", scoreToSet);
		PlayerPrefs.SetInt("ScoreBlue", scoreToSet);

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
