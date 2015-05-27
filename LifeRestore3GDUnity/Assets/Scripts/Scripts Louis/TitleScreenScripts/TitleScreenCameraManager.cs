using UnityEngine;
using System.Collections;

public class TitleScreenCameraManager : MonoBehaviour {

	public float v_cameraSpeedTranslate;
	[SerializeField]
	private Transform[] wayPoints;
	[SerializeField]
	private Transform currentWaypoint;
	private bool lerping = false;

	private GameObject[] colliderInterieurs, colliderExterieurs;

	private float _timeStartedLerping;

	// Use this for initialization
	void Start () {
		gameObject.transform.position = currentWaypoint.position;
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Space)){
			Debug.Log("urrent wp is "+currentWaypoint.name + "" + currentWaypoint.transform.position);
		}

//		if(lerping == false){
//			_timeStartedLerping=0;
//			float _timeLerping = Time.time;
//		
//			float timeSinceStarted = Time.time - _timeLerping;
//			float percentageComplete = timeSinceStarted / v_cameraSpeedTranslate;
//			
//			transform.position = Vector3.Lerp(transform.position, currentWaypoint.position, percentageComplete);
////			if(percentageComplete >= 1.0f){
////				lerping = false;
////				currentWaypoint = targetWP;
////			}
//		}				
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

	public void RemoveCamera(){
		lerping = false;
	}

	public void PleaseMoveCamera(int targetWP){

	}
}
