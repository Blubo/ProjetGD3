using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	[HideInInspector]
    public int Score_Jaune, Score_Bleu, Score_Rouge, Score_Vert;

	private Animator gaugeHUDanimator;

	void Start () {
		gaugeHUDanimator = Camera.main.transform.Find("GaugeHUD").GetComponent<Animator>();
	  Score_Jaune = 0;
      Score_Bleu = 0;
      Score_Rouge = 0;
      Score_Vert = 0;
	}
	

	void Update () {
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
            case "Jaune":
                Score_Jaune += _value;
                break;

			case "Rouge":
				Score_Rouge += _value;
				break;


			case "Vert":
				Score_Vert += _value;
				break;

            case "Bleu":
                Score_Bleu += _value;
                break;
        }
    }
}
