using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score_HUD : MonoBehaviour {

    private ScoreManager _ScoreManager;
    private string _ThisScore;

	public GameObject Vert, Bleu, Rouge;
	private Text _Vert, _Bleu, _Rouge;

	// Use this for initialization
	void Start () {
        _ThisScore = gameObject.name;
        _ScoreManager = Camera.main.GetComponent<ScoreManager>();

		_Vert = Vert.GetComponent<Text>();
		_Rouge = Rouge.GetComponent<Text>();
		_Bleu = Bleu.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		_Vert.text = "" + _ScoreManager.Score_Vert;
        _Rouge.text = "" + _ScoreManager.Score_Rouge;
        _Bleu.text = "" + _ScoreManager.Score_Bleu;
	}
}