using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score_HUD : MonoBehaviour {

    private ScoreManager _ScoreManager;
    private string _ThisScore;

    public GameObject Jaune, Rouge, Vert, Bleu;
    private Text _Jaune, _Rouge, _Vert, _Bleu;

	// Use this for initialization
	void Start () {
        _ThisScore = gameObject.name;
        _ScoreManager = Camera.main.GetComponent<ScoreManager>();

                _Jaune = Jaune.GetComponent<Text>();
                _Rouge = Rouge.GetComponent<Text>();
                _Vert = Vert.GetComponent<Text>();
                _Bleu = Bleu.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        _Jaune.text = ""+ _ScoreManager.Score_Jaune ;
        _Rouge.text = "" + _ScoreManager.Score_Rouge;
        _Vert.text = "" + _ScoreManager.Score_Vert;
        _Bleu.text = "" + _ScoreManager.Score_Bleu;
	}
}