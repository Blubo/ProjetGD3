using UnityEngine;
using System.Collections;

//ce script est posé sur la caméra et gère sa position

public class TitleScreenCameraManager : MonoBehaviour {

	public float v_cameraSpeedTranslate, specialCameraSpeedTranslate;
	[SerializeField]
	private Transform[] wayPoints;
	[SerializeField]
	private Transform currentWaypoint;
	public int currentWayPointNumber;
	private int lastWPnumber;
	private bool lerping = false;

	private GameObject[] colliderInterieurs, colliderExterieurs;

	private float _timeStartedLerping;
	public bool locked = true;
	[SerializeField]
	private float rotationSpeed, specialRotationSpeed;
	public float cameraFOVspeed;
	// Use this for initialization
	void Start () {
		gameObject.transform.position = currentWaypoint.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(locked == false){
//			if(lastWPnumber==0 && currentWayPointNumber != 1){
			if(currentWayPointNumber != 1){
				Camera.main.transform.position = Vector3.Lerp (Camera.main.transform.position, wayPoints[currentWayPointNumber].position, Time.deltaTime*v_cameraSpeedTranslate*0.9f);
				Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, wayPoints[currentWayPointNumber].rotation, Time.time * rotationSpeed);
				Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60, Time.deltaTime*cameraFOVspeed);;

			}
			else{
				Camera.main.transform.position = Vector3.Lerp (Camera.main.transform.position, wayPoints[currentWayPointNumber].position, Time.deltaTime);
				Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, wayPoints[currentWayPointNumber].rotation, Time.time * specialRotationSpeed);
				Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 30, Time.deltaTime*cameraFOVspeed);;
			}
//			Camera.main.transform.rotation = Quaternion.LookRotation(wayPoints[currentWayPointNumber].forward);	

		}
//		lastWPnumber = currentWayPointNumber;
	}

	public void MoveCamera(int targetWP){
		if(lerping == false){
			_timeStartedLerping = Time.time;
			lerping = true;
		}
		float timeSinceStarted = Time.time - _timeStartedLerping;
		float percentageComplete = timeSinceStarted / v_cameraSpeedTranslate;
		
		transform.position = Vector3.Lerp(currentWaypoint.position, wayPoints[targetWP].position, percentageComplete);
//		transform.position = Vector3.Lerp(transform.position, wayPoints[targetWP].position, percentageComplete);

		if(percentageComplete >= 1.0f){
			lerping = false;
			currentWaypoint = wayPoints[targetWP];
			_timeStartedLerping=0;
		}
	}

	IEnumerator CameraAngle(Transform target){
		currentWayPointNumber = 1;
		Camera.main.transform.position = Vector3.Lerp (Camera.main.transform.position, target.position, Time.deltaTime * specialCameraSpeedTranslate);
		Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, target.rotation, Time.time * specialRotationSpeed);

		yield return null;
	}

	public void PleaseCameraAngle(Transform target){
		StartCoroutine(CameraAngle(target));
	}

	public void RemoveCamera(){
		lerping = false;
	}
}
