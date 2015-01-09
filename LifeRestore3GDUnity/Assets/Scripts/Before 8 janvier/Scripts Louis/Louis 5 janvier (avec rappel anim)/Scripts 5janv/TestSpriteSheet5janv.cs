using UnityEngine;
using System.Collections;

public class TestSpriteSheet5janv : MonoBehaviour {
	public Animator myAnim;
	// Use this for initialization
	void Start () {
		myAnim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Keypad0)){
			if(myAnim!=null){
				myAnim.SetInteger("selected", 0);
			}
		}

		if(Input.GetKey(KeyCode.Keypad1)){
			if(myAnim!=null){
				myAnim.SetInteger("selected", 1);
			}
		}
		if(Input.GetKey(KeyCode.Keypad2)){
			if(myAnim!=null){
				myAnim.SetInteger("selected", 2);
			}
		}

		if(Input.GetKey(KeyCode.Keypad3)){
			if(myAnim!=null){
				myAnim.SetInteger("selected", 3);
			}
		}
	}
}
