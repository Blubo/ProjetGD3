using UnityEngine;
using System.Collections;

public class DisplayLevelStats : MonoBehaviour {

	[SerializeField]
	private GameObject objectToDisplay;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag.Equals("Unlinkable")){
			objectToDisplay.GetComponent<Renderer>().enabled = true;

		}
	}

	void OnTriggerExit(Collider col){
		if(col.gameObject.tag.Equals("Unlinkable")){
			objectToDisplay.GetComponent<Renderer>().enabled = false;
		}
	}
}
