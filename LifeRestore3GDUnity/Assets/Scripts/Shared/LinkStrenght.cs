using UnityEngine;
using System.Collections;

public class LinkStrenght : MonoBehaviour {

	[HideInInspector]
	public int _LinkCommited;

	// Use this for initialization
	void Start () {
		_LinkCommited = 0;
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log ("link commited "+ _LinkCommited);

		//Si le joueur est lié (ou a lié du coup) son layer change et il peut rentrer dans les zones interdites auparavant
		if (_LinkCommited != 0) {
			gameObject.layer=13;
		} else {
			gameObject.layer= LayerMask.NameToLayer("Usable");
		}


		//normalement, ca devrait aller, mais ptet que ce clamp est une solution trop simple
		_LinkCommited=(int)Mathf.Clamp(_LinkCommited, 0, Mathf.Infinity);
	}
}
