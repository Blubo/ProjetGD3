using UnityEngine;
using System.Collections;

public class FirstInternetCameraScript : MonoBehaviour {
	[HideInInspector]
	public float minX, maxX, minZ, maxZ;
	private float camSize, sizeX, sizeZ, lerpSpeed, lerpSpeed2;
	private GameObject[] players;
//	private GameObject[] allTargets;
	private Quaternion rot;
	private Vector3 pos, finalLookAt;

	public Vector3 angles, cameraBuffer;
	public float camSpeed, camDist, LerpConst, camSpeedTime, camSpeedTime2, myTime;

	public GameObject newCameraFocus;
	[HideInInspector]
	public GameObject currentZone;
	private CameaZoneResizer currentZoneSizer;

	// Update is called once per frame
	void Update() {
		currentZoneSizer = currentZone.GetComponent<CameaZoneResizer>();

		CalculateBounds();
	
		CalculateCameraPosAndSize();
	}

	void CalculateBounds() {
		minX = Mathf.Infinity;
		maxX = -Mathf.Infinity;
		minZ = Mathf.Infinity;
		maxZ = -Mathf.Infinity;
		
		players = GameObject.FindGameObjectsWithTag("Player");
		GameObject[] allTargets = new GameObject[players.Length+currentZoneSizer.focusPoints.Length];
		players.CopyTo(allTargets, 0);
		currentZoneSizer.focusPoints.CopyTo(allTargets, players.Length);


		foreach (GameObject player in players){
			
			Vector3 tempPlayer = player.transform.position;
			
			//X Bounds
			if (tempPlayer.x < minX)
				minX = tempPlayer.x;
			
			if (tempPlayer.x > maxX)
				maxX = tempPlayer.x;
			
			//Y Bounds
			if (tempPlayer.z < minZ)
				minZ = tempPlayer.z;
			
			if (tempPlayer.z > maxZ)
				maxZ = tempPlayer.z;
		}

		if(currentZoneSizer.focusPoints.Length!=0){
			
			foreach (GameObject target in allTargets){
				
				Vector3 tempTarget = target.transform.position;
				
				//X Bounds
				if (tempTarget.x < minX)
					minX = tempTarget.x;
				
				if (tempTarget.x > maxX)
					maxX = tempTarget.x;
				
				//Y Bounds
				if (tempTarget.z < minZ)
					minZ = tempTarget.z;
				
				if (tempTarget.z > maxZ)
					maxZ = tempTarget.z;
			}
			
		}
	}

	void CalculateCameraPosAndSize() {
		//Position
		Vector3 cameraCenter = Vector3.zero;
		Vector3 focusCenter = Vector3.zero;

		foreach(GameObject player in players){
			cameraCenter += player.transform.position;
		}
		foreach(GameObject localFocus in currentZoneSizer.focusPoints){
			focusCenter += localFocus.transform.position;
		}
			
		Vector3 secondCameraCenter = cameraCenter / players.Length;
		Vector3 localFocusCameraCenter = focusCenter/currentZoneSizer.focusPoints.Length;

		Vector3 FinalCameraCenter = secondCameraCenter;
		if(currentZoneSizer.focusPoints.Length!=0){
			FinalCameraCenter = (secondCameraCenter+localFocusCameraCenter)/2;
		}

		newCameraFocus.transform.position = secondCameraCenter;
		//Rotates and Positions camera around a point

		rot = Quaternion.Euler(angles);
		pos = rot * new Vector3(0f, camDist, 0) + FinalCameraCenter; 

		transform.rotation = rot;

		lerpSpeed += Time.deltaTime*camSpeed;
		transform.position = Vector3.Lerp(transform.position, pos, lerpSpeed);

		finalLookAt = Vector3.Lerp (finalLookAt, FinalCameraCenter, lerpSpeed);
		
		transform.LookAt(finalLookAt);
		
		//Size
//		float sizeX = maxX - minX + cameraBuffer.x;
//		float sizeZ = maxZ - minZ + cameraBuffer.z;
		lerpSpeed2 += LerpConst*Time.deltaTime;
		sizeX = Mathf.Lerp(sizeX, maxX - minX + cameraBuffer.x, lerpSpeed2);
		sizeZ = Mathf.Lerp(sizeZ, maxZ - minZ + cameraBuffer.z, lerpSpeed2);


		//condition ? first_expression : second_expression;
		//if condition is true, first_expression becomes the result
		//if condition is false, second_expression becomes the result
		camSize = (sizeX > sizeZ ? sizeX : sizeZ);
		
		GetComponent<Camera>().orthographicSize = camSize * 0.5f;
	}

	IEnumerator MoveObject (Transform thisTransform, Vector3 startPos, Vector3 endPos, float time){
		float i = 0;
		float rate = 1/time;
		while (i < 1) {
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp(startPos, endPos, i);
			yield return null; 
		}
	}

	IEnumerator MoveVector (Vector3 thisVector3, Vector3 startPos, Vector3 endPos, float time){
		float i = 0;
		float rate = 1/time;
		while (i < 1) {
			i += Time.deltaTime * rate;
			thisVector3 = Vector3.Lerp(startPos, endPos, i);
			yield return null; 
		}
	}

	IEnumerator MoveFloat (float thisFloat, float startPos, float endPos, float time){
		float i = 0;
		float rate = 1/time;
		while (i < 1) {
			i += Time.deltaTime * rate;
			thisFloat = Mathf.Lerp (startPos, endPos, i);
			yield return null; 
		}
	}

}