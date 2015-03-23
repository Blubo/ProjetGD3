using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    public int Score_Jaune, Score_Bleu, Score_Rouge, Score_Vert;

	void Start () {
	  Score_Jaune = 0;
      Score_Bleu = 0;
      Score_Rouge = 0;
      Score_Vert = 0;
	}
	

	void Update () {
	
	}

    public void Increase_score(string _Player, int _value){
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
