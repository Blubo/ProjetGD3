using UnityEngine;
using System.Collections;

public class CameaZoneResizer : MonoBehaviour {

	private FirstInternetCameraScript cameraScript;
	[SerializeField]
	public Vector3 cameraBuffer;
	public GameObject[] focusPoints;
	public float smoothTime = 0.3f;

	private GameObject newCameraFocus;
	private Vector3 velocity = Vector3.zero;

	// Use this for initialization
	void Start () {
		cameraScript = Camera.main.GetComponent<FirstInternetCameraScript>();
		newCameraFocus = cameraScript.newCameraFocus;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

//	void OnTriggerEnter(Collider col){
////		if(col.gameObject.tag.Equals("Player")==true){
////			Debug.Log("detected player");
////			cameraScript.cameraBuffer = cameraBuffer;
////		}
//
////		if(col.gameObject == Camera.main.gameObject){
////			Debug.Log("detected camera");
////			cameraScript.cameraBuffer = cameraBuffer;
////		}
//
//		if(col.gameObject == newCameraFocus){
//			Debug.Log("detected barycenter");
//			cameraScript.cameraBuffer = cameraBuffer;
////			StartCoroutine(MoveVector(cameraScript.cameraBuffer, cameraScript.cameraBuffer, cameraBuffer, 5));
//			cameraScript.currentZone = gameObject;
//		}
//	}

	void OnTriggerStay(Collider col){
		//		if(col.gameObject.tag.Equals("Player")==true){
		//			Debug.Log("detected player");
		//			cameraScript.cameraBuffer = cameraBuffer;
		//		}
		
		//		if(col.gameObject == Camera.main.gameObject){
		//			Debug.Log("detected camera");
		//			cameraScript.cameraBuffer = cameraBuffer;
		//		}

		if(col.gameObject == newCameraFocus){
			Debug.Log("detected barycenter");
//			cameraScript.cameraBuffer = Vector3.Lerp(cameraScript.cameraBuffer, cameraBuffer, Time.deltaTime);
			cameraScript.cameraBuffer = Vector3.SmoothDamp(cameraScript.cameraBuffer, cameraBuffer, ref velocity, smoothTime);

			//			StartCoroutine(MoveVector(cameraScript.cameraBuffer, cameraScript.cameraBuffer, cameraBuffer, 5));
			cameraScript.currentZone = gameObject;
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
}
