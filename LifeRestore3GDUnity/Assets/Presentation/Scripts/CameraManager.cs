using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class CameraManager : MonoBehaviour {
	
	public float v_cameraSpeedTranslate;
	public int _CurrentSlide;
	public Transform[] v_myCameras;
	//public Transform v_PlacementCameras;

	// Use this for initialization
	void Start () {
		_CurrentSlide = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Camera.main.transform.position = Vector3.Lerp (Camera.main.transform.position, v_myCameras[_CurrentSlide].position, Time.deltaTime*v_cameraSpeedTranslate*0.9f);
	}

	void OnTriggerEnter(Collider _collider) {
		if(_collider.gameObject.tag == "Respawn"){
			int _index = -1;
			int.TryParse(_collider.name, out _index);
			_CurrentSlide = _index;
		}
	}
}
