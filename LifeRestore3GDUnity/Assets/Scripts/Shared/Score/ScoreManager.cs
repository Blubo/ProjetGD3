using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	[HideInInspector]
    public int Score_Jaune, Score_Bleu, Score_Rouge, Score_Vert;

	private Animator gaugeHUDanimator;

	void Start () {
		gaugeHUDanimator = Camera.main.transform.Find("GaugeHUD").GetComponent<Animator>();
	  Score_Jaune = 0;
		Score_Bleu = PlayerPrefs.GetInt("ScoreBlue");
		Score_Rouge = PlayerPrefs.GetInt("ScoreRed");
		Score_Vert = PlayerPrefs.GetInt("ScoreGreen");
	}
	

	void Update () {
		if(Score_Jaune<0) Score_Jaune = 0;
		if(Score_Vert<0) Score_Vert = 0;
		if(Score_Rouge<0) Score_Rouge = 0;
		if(Score_Bleu<0) Score_Bleu = 0;

		int maxScore = Mathf.Max(Score_Vert, Score_Rouge, Score_Bleu);
		if(maxScore == Score_Vert){
			gaugeHUDanimator.SetInteger("WhoIsHighest", 1);
		}else if(maxScore == Score_Rouge){
			gaugeHUDanimator.SetInteger("WhoIsHighest", 2);
		}else if(maxScore == Score_Bleu){
			gaugeHUDanimator.SetInteger("WhoIsHighest", 3);
		}
		if(Score_Vert== Score_Rouge && Score_Rouge == Score_Bleu){
			gaugeHUDanimator.SetInteger("WhoIsHighest", 0);
		}

	}

    public void Increase_score(string _Player, int _value){
        switch (_Player)
        {
			case "Rouge":
				Score_Rouge += _value;
			PlayerPrefs.SetInt("ScoreRed", Score_Rouge);
				break;


			case "Vert":
				Score_Vert += _value;
			PlayerPrefs.SetInt("ScoreGreen", Score_Vert);
				break;

            case "Bleu":
                Score_Bleu += _value;
			PlayerPrefs.SetInt("ScoreBlue", Score_Bleu);
                break;
        }
    }
}
