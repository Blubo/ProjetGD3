using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PodiumTextScore : MonoBehaviour {

	[SerializeField]
	private GameObject greenGauge, redGauge, blueGauge;
	private Text greenText, redText, blueText;

	// Use this for initialization
	void Awake () {
		greenText = greenGauge.transform.Find("CaseVerte/Canvas/ScoreGreen").GetComponent<Text>();
		redText = redGauge.transform.Find("CaseRouge/Canvas/ScoreRed").GetComponent<Text>();
		blueText = blueGauge.transform.Find("CaseBleue/Canvas/ScoreBlue").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		greenText.text = PlayerPrefs.GetInt("ScoreGreen").ToString();
		redText.text = PlayerPrefs.GetInt("ScoreBlue").ToString();
		blueText.text = PlayerPrefs.GetInt("ScoreRed").ToString();

	}
}
