using UnityEngine;
using System.Collections;

public class EndManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TheEnd(){
		PlayerPrefs.SetInt("ScoreGreen", Camera.main.GetComponent<ScoreManager>().Score_Vert);
		PlayerPrefs.SetInt("ScoreRed", Camera.main.GetComponent<ScoreManager>().Score_Rouge);
		PlayerPrefs.SetInt("ScoreBlue", Camera.main.GetComponent<ScoreManager>().Score_Bleu);

	}

}
