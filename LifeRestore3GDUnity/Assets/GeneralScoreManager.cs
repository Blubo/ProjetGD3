using UnityEngine;
using System.Collections;

public class GeneralScoreManager : MonoBehaviour {

	[HideInInspector]
	public int generalGreen, generalRed, generalBlue;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		generalGreen = PlayerPrefs.GetInt("ScoreBlue");
		generalRed = PlayerPrefs.GetInt("ScoreRed");
		generalBlue = PlayerPrefs.GetInt("ScoreGreen");
	}
}
