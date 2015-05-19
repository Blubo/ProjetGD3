using UnityEngine;
using System.Collections;

public class ScoreAwake : MonoBehaviour {

	void Awake(){
		PlayerPrefs.SetInt("ScoreGreen", 0);
		PlayerPrefs.SetInt("ScoreRed", 0);
		PlayerPrefs.SetInt("ScoreBlue", 0);

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
