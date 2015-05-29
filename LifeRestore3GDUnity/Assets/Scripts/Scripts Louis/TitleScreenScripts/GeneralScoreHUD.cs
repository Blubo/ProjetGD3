using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GeneralScoreHUD : MonoBehaviour {
	
	private GeneralScoreManager generalScoreManager;

	public GameObject Vert, Bleu, Rouge;
	private Text _Vert, _Bleu, _Rouge;
	
	// Use this for initialization
	void Start () {
		generalScoreManager = Camera.main.GetComponent<GeneralScoreManager>();
		
		_Vert = Vert.GetComponent<Text>();
		_Rouge = Rouge.GetComponent<Text>();
		_Bleu = Bleu.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		_Vert.text = "" + generalScoreManager.generalGreen;
		_Rouge.text = "" + generalScoreManager.generalRed;
		_Bleu.text = "" + generalScoreManager.generalBlue;
	}
}