using UnityEngine;
using System.Collections;

public class ActivatedWhenTargetIsLinked : MonoBehaviour {

	[Tooltip("Put the item to check for here")]
	[SerializeField]
	private GameObject linkedGameObjectOrNot;

	//has this been already activated or not ?
	private bool alreadyActivated = false;

	// Update is called once per frame
	void Update () {
		if(linkedGameObjectOrNot.GetComponent<Sticky>() != null){
			if(alreadyActivated == false){
				if(linkedGameObjectOrNot.GetComponent<Sticky>().v_numberOfLinks!=0){
					alreadyActivated = true;
					gameObject.SendMessage("Activated");
				}
			}
		}
	}
}
