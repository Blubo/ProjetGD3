using UnityEngine;
using System.Collections;

public class LifeUI : MonoBehaviour {

	public GameObject v_hp;
	public Vector3 v_screenPos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		v_hp.guiText.fontSize = 30;
		//v_hp.guiText.fontStyle = FontStyle.Bold;
		v_hp.guiText.alignment = TextAlignment.Left;
		v_hp.guiText.anchor = TextAnchor.MiddleLeft;
		v_hp.transform.position = v_screenPos; //gameObject.transform.position; //new Vector3(0.95f,0.9f,0f);
		v_hp.guiText.text = "HP " + gameObject.name + ": " + gameObject.GetComponentInChildren<PlayerState>().v_myVisibleHP;
	}
}
