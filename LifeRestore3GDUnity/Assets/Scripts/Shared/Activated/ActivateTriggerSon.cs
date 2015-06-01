using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActivateTriggerSon : MonoBehaviour {

	[SerializeField]
	[Tooltip("Insert gameObjects affected by this réceptacle")]
	private List<GameObject> activatedItem;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag.Equals("Player") == true){
			for (int i = 0; i < activatedItem.Count; i++) {
				activatedItem[i].SendMessage("Activated");
			}
		}
	}
}
