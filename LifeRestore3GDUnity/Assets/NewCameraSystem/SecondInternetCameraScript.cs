using UnityEngine;
using System.Collections;

public class SecondInternetCameraScript : MonoBehaviour {
	
	private bool isOrthographic;
	GameObject[] targets;
	float currentDistance;
	float largestDistance;
	Camera theCamera;
	float height = 5.0f;
	float avgDistance;
	float distance = 0.0f;
	// Default Distance
	float speed = 1;
	float offset;
	
//	========================================
	
	void Start(){
		theCamera = Camera.main;
		targets = GameObject.FindGameObjectsWithTag("Player"); 
		
		if(theCamera) isOrthographic = theCamera.orthographic;
		
	}
	
	void OnGUI(){
		
		GUI.Label(new Rect(10, 10, 200, 20), "largest distance is = " + largestDistance.ToString());
		
		GUI.Label(new Rect(10, 40, 200, 20), "height = " + height.ToString());
		
		GUI.Label(new Rect(10, 70, 200, 20), "number of players = " + targets.Length.ToString());
		
	}
	
	void LateUpdate (){
		
		targets = GameObject.FindGameObjectsWithTag("Player"); 
		
		if (!GameObject.FindWithTag("Player")) return;
		
		Vector3 sum = new Vector3(0,0,0);
		
		for (int n = 0; n < targets.Length ; n++){
			sum += targets[n].transform.position;
		}
		
		Vector3 avgDistance = sum / targets.Length;
		// Debug.Log(avgDistance);
		float largestDifference = returnLargestDifference();
		height = Mathf.Lerp(height,largestDifference,Time.deltaTime * speed);
		
		if(isOrthographic){
			theCamera.transform.position = new Vector3(avgDistance.x, height, theCamera.transform.position.z);
			theCamera.orthographicSize = largestDifference;
			theCamera.transform.LookAt(avgDistance);
		} else {
			theCamera.transform.position = new Vector3(avgDistance.x, height, avgDistance.z - distance + largestDifference);
			theCamera.transform.LookAt(avgDistance);
		}

		Vector3 thisVect = theCamera.transform.forward.normalized;
		
		thisVect = Quaternion.Euler(0, 180,0)*thisVect;
		theCamera.transform.forward = thisVect;
		Debug.DrawRay(avgDistance, Vector3.up);
	}
	
	float returnLargestDifference(){
		
		currentDistance = 0.0f;
		largestDistance = 0.0f;
		
		for(int i = 0; i < targets.Length; i++){
			for(int j = 0; j <  targets.Length; j++){
				currentDistance = Vector3.Distance(targets[i].transform.position,targets[j].transform.position);
				
				if(currentDistance > largestDistance){	
					largestDistance = currentDistance;
				}	
			}
		}
		return largestDistance;
	}
}
