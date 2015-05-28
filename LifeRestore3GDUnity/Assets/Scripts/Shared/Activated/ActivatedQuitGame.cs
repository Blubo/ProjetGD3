using UnityEngine;
using System.Collections;

public class ActivatedQuitGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Activated(){
		Application.Quit();
	}
}
