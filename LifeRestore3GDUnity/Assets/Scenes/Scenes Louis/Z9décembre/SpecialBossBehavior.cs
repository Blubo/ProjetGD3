using UnityEngine;
using System.Collections;

public class SpecialBossBehavior : MonoBehaviour {

	Boss_Playstate _playstate;


	private float _Speed;
	private bool isLinked, isGLinked, isRLinked, isBLinked;
	private Color _myColor;
	private float _green, _red, _blue, _colorConstant;

	void Start () {
		_myColor=gameObject.renderer.material.color;
		_green=gameObject.renderer.material.color.g;
		_red=gameObject.renderer.material.color.r;
		_blue=gameObject.renderer.material.color.b;
		_colorConstant=0.15f;
		
		isLinked=false;
		isGLinked=false;
		isRLinked=false;
		isBLinked=false;
		_playstate = GetComponent<Boss_Playstate> ();

	}
	
	void Update () {

		if(isRLinked==false){
			if(_red>0){
				_red-=_colorConstant*Time.deltaTime;
			}
		}

		if(isGLinked==false){
			if(_green>0){

				_green-=_colorConstant*Time.deltaTime;
			}
		}

		if(isBLinked==false){
			if(_blue>0){

				_blue-=_colorConstant*Time.deltaTime;
			}

		}
		_myColor=new Color(Mathf.Clamp(_red, 0, 1), Mathf.Clamp(_green, 0, 1), Mathf.Clamp(_blue, 0, 1));
		gameObject.renderer.material.color=_myColor;
	}

//	void OnTriggerEnter(Collider collided){
//		if(collided.gameObject.tag == "Pellet"){
//			Debug.Log("im hit!!");
//			Debug.Log("r " + collided.GetComponent<SpecialPelletScript>().v_whoShotMe.renderer.material.color.r);
//			Debug.Log("g " + collided.GetComponent<SpecialPelletScript>().v_whoShotMe.renderer.material.color.g);
//			Debug.Log("b " + collided.GetComponent<SpecialPelletScript>().v_whoShotMe.renderer.material.color.b);
//
//			if(collided.GetComponent<SpecialPelletScript>().v_whoShotMe.renderer.material.color.r==1){
//				isRLinked=true;
//				Debug.Log("my shooter is red");
//			}
//
//			if(collided.GetComponent<SpecialPelletScript>().v_whoShotMe.renderer.material.color.g==1){
//				isGLinked=true;
//				
//				Debug.Log("my shooter is green");
//			}
//
//			if(collided.GetComponent<SpecialPelletScript>().v_whoShotMe.renderer.material.color.b==1){
//				isBLinked=true;
//
//				Debug.Log("my shooter is blue");
//			}
//		}
//	}

	void OnTriggerStay(Collider collided){
		if(collided.gameObject.tag == "Pellet"){
			if(collided.GetComponent<SpecialPelletScript>().v_whoShotMe.renderer.material.color.r==1){
				isRLinked=true;
				if(_red<1){

					_red+=_colorConstant*Time.deltaTime;
				}
			//	Debug.Log("my shooter is red");
			}

			if(collided.GetComponent<SpecialPelletScript>().v_whoShotMe.renderer.material.color.g==1){
				isGLinked=true;
				if(_green<1){

				_green+=_colorConstant*Time.deltaTime;
				}
				//Debug.Log("my shooter is green");
			}

			if(collided.GetComponent<SpecialPelletScript>().v_whoShotMe.renderer.material.color.b==1){
				isBLinked=true;
				if(_blue<1){

				_blue+=_colorConstant*Time.deltaTime;
				}
			//	Debug.Log("my shooter is blue");
			}
		}
	}

	void OnTriggerExit(Collider collided){
		if(collided.gameObject.tag == "Pellet"){
			if(collided.GetComponent<SpecialPelletScript>().v_whoShotMe.renderer.material.color.r==1){
				isRLinked=false;
			}
			
			if(collided.GetComponent<SpecialPelletScript>().v_whoShotMe.renderer.material.color.g==1){
				isGLinked=false;
			}
			
			if(collided.GetComponent<SpecialPelletScript>().v_whoShotMe.renderer.material.color.b==1){
				isBLinked=false;
			}
		}
	}
}
