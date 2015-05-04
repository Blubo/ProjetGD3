using UnityEngine;
using System.Collections;

public class ReticuleTarget : MonoBehaviour {

	[HideInInspector]
	public Renderer GREYRend, YRend, RRend, GRend, BRend;

	// Use this for initialization
	void Start () {
		GREYRend = gameObject.transform.Find("Reticule/VisuelGREY").GetComponent<Renderer>();
		YRend = gameObject.transform.Find("Reticule/VisuelY").GetComponent<Renderer>();
		RRend = gameObject.transform.Find("Reticule/VisuelR").GetComponent<Renderer>();
		GRend = gameObject.transform.Find("Reticule/VisuelG").GetComponent<Renderer>();
		BRend = gameObject.transform.Find("Reticule/VisuelB").GetComponent<Renderer>();

		GREYRend.enabled = false;
		YRend.enabled = false;
		RRend.enabled = false;
		GRend.enabled = false;
		BRend.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void LightReticuleUp(Renderer rend){
		rend.enabled = true;
	}

	public void TurnReticuleOff(Renderer rend){
		rend.enabled = false;
	}

}
