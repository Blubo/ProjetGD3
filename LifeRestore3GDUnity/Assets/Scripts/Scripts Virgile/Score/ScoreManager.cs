using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    public float Score_Jaune, Score_Bleu, Score_Rouge, Score_Vert;

	void Start () {
	  Score_Jaune = 0.0f;
      Score_Bleu = 0.0f;
      Score_Rouge = 0.0f;
      Score_Vert = 0.0f;
	}
	

	void Update () {
	
	}

    public void Increase_score(string _Player, float _value){
        switch (_Player)
        {
            case "Jaune":
                Score_Jaune += _value;
                break;

            case "Bleu":
                Score_Bleu += _value;
                break;

            case "Vert":
                Score_Vert += _value;
                break;

            case "Rouge":
                Score_Rouge += _value;
                break;
        }
    }
}
