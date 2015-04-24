using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraManagerTests : MonoBehaviour {

	public List<Camera> v_cameraList;
	private string keyPressed;
	private int currentCameraIndex;

	// Use this for initialization
	void Start () {
		currentCameraIndex = 0;
		
		//Turn all cameras off, except the first default one
		for (int i=1; i<v_cameraList.Count; i++) 
		{
			v_cameraList[i].gameObject.SetActive(false);
		}
		
		//If any cameras were added to the controller, enable the first one
		if (v_cameraList.Count>0)
		{
			v_cameraList [0].gameObject.SetActive (true);
			Debug.Log ("Camera with name: " + v_cameraList [0].GetComponent<Camera>().name + ", is now enabled");
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.anyKeyDown)
		{
			if(Input.GetKeyDown(KeyCode.Keypad0)||
			   Input.GetKeyDown(KeyCode.Keypad1)||
			   Input.GetKeyDown(KeyCode.Keypad2)||
			   Input.GetKeyDown(KeyCode.Keypad3)|| 
			   Input.GetKeyDown(KeyCode.Keypad4)||
			   Input.GetKeyDown(KeyCode.Keypad5)||
			   Input.GetKeyDown(KeyCode.Keypad6)||
			   Input.GetKeyDown(KeyCode.Keypad7)||
			   Input.GetKeyDown(KeyCode.Keypad8)||
			   Input.GetKeyDown(KeyCode.Keypad9)){
				keyPressed = Input.inputString;
				currentCameraIndex=int.Parse(keyPressed);
				Debug.Log (keyPressed + "button has been pressed. Switching to the assigned camera");

				for (int i = 0; i < v_cameraList.Count; i++) {
					v_cameraList[i].gameObject.SetActive(false);
				}
				if(currentCameraIndex<=v_cameraList.Count){
				v_cameraList[currentCameraIndex].gameObject.SetActive(true);
				}
			}
		}
	}
}