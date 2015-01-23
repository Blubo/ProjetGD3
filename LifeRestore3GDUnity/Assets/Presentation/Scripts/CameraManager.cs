using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class CameraManager : MonoBehaviour {

	bool playerIndexSet = false;
	public PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;

	public float v_cameraSpeedTranslate;
	public int _CurrentSlide;
	public Transform[] v_myCameras;
	public AudioSource sonSandbox;
	//public Transform v_PlacementCameras;

	// Use this for initialization
	void Start () {
		_CurrentSlide = 0;
	}
	
	// Update is called once per frame
	void Update () {

		prevState = state;
		state = GamePad.GetState(playerIndex);

		Camera.main.transform.position = Vector3.Lerp (Camera.main.transform.position, v_myCameras[_CurrentSlide].position, Time.deltaTime*v_cameraSpeedTranslate*0.9f);

		if(prevState.Buttons.Start == ButtonState.Released && state.Buttons.Start == ButtonState.Pressed){
			_CurrentSlide +=1 ;
			transform.position = Camera.main.transform.position- new Vector3(-100f,85f);
		}
		if(prevState.Buttons.Back == ButtonState.Released && state.Buttons.Back == ButtonState.Pressed){
			_CurrentSlide -=1 ;
			transform.position = Camera.main.transform.position- new Vector3(100f,85f);;
		}

		if (_CurrentSlide ==14) {
			if (sonSandbox.isPlaying == false) {
				sonSandbox.Play ();
			}
		} else {
			sonSandbox.Pause ();
		}

	}

	void OnTriggerEnter(Collider _collider) {
		if(_collider.gameObject.tag == "Respawn"){
			int _index = -1;
			int.TryParse(_collider.name, out _index);
			_CurrentSlide = _index;
		}
	}
}
