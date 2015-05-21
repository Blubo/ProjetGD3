using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ReticuleHUD : MonoBehaviour {

	public GameObject viewRange, viewAngle, player1;
	private Text text_viewRange, text_viewAngle;
	private ReticuleCone p1retCone;

	// Use this for initialization
	void Start () {
		p1retCone = player1.GetComponent<ReticuleCone>();
		text_viewRange = viewRange.GetComponent<Text>();
		text_viewAngle = viewAngle.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		text_viewRange.text = "View Range " + p1retCone.viewRange;
		text_viewAngle.text = "View Angle " + p1retCone.viewAngle;
	}
}
