using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PodiumTextScore : MonoBehaviour {

	[SerializeField]
	private GameObject greenGauge, redGauge, blueGauge;
	private Text greenText, redText, blueText;
	private int myGreenInt, myRedInt, myBlueInt;
	private PodiumScore myPodiumScore;
	private GameObject first, second, third;
	// Use this for initialization
//	void Awake () {
//		myPodiumScore = Camera.main.GetComponent<PodiumScore>();
//		greenText = greenGauge.transform.Find("CaseVerte/Canvas/ScoreGreen").GetComponent<Text>();
//		redText = redGauge.transform.Find("CaseRouge/Canvas/ScoreRed").GetComponent<Text>();
//		blueText = blueGauge.transform.Find("CaseBleue/Canvas/ScoreBlue").GetComponent<Text>();
//	}
//
//	void Start(){
//		if(myPodiumScore.scores[0] == PlayerPrefs.GetInt("ScoreGreen")){
//
//		}
//	}
//
//	// Update is called once per frame
//	void Update () {
//
//		//but: faire monter graduellement le score de chaque joueur
//		//pour ca
//		//il faut d'une part une variable temporaire de score de chaque joueur qu'on incrémente et qu'on render sur la scene
//		//d'autre part le vrai score de chaque joueur
//		//encore d'autre part, il faut que ce score finisse d'augmenter quand la barre du joueur attend son max
//		//autrement dit, le score doit s'incrémenter d'une valeur proportionelle à l'avancement/progression/vitesse de la barre
//		//ex: à chaque frame, scoreJoueur+=(VraiScoreJoueur/tempsDeMontéeDeLaJauge);
//
////		if(myPodiumScore<PlayerPrefs.GetInt("ScoreGreen")){
////			myGreenInt+=PlayerPrefs.GetInt("ScoreGreen")/myPodiumScore
////		}
//
//		greenText.text = PlayerPrefs.GetInt("ScoreGreen").ToString();
//		redText.text = PlayerPrefs.GetInt("ScoreRed").ToString();
//		blueText.text = PlayerPrefs.GetInt("ScoreBlue").ToString();
//
//	}
}
