using UnityEngine;
using System.Collections;

public class ReceptacleKey : MonoBehaviour {

	[SerializeField]
	private GameObject posCenter, receptacleRotate;

	[SerializeField]
	private float allowedTime;
	private float timer = 0f;

	private bool idoleInRec = false;
	private Quaternion initReceptRot;

	// Use this for initialization
	void Start () {

		initReceptRot = receptacleRotate.transform.rotation;

	}
	
	// Update is called once per frame
	void Update () {
		if(idoleInRec == true){
			timer+=Time.deltaTime;
		}

		if(timer>allowedTime && idoleInRec == false){
			timer=0f;
		}

//		if(idoleInRec == true){
//			float angle = Mathf.Atan2 (state.ThumbSticks.Left.Y, state.ThumbSticks.Left.X) * Mathf.Rad2Deg;
//			receptacleRotate.transform.rotation = Quaternion.Euler (new Vector3(0, -angle+90, 0));
//		}

	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag.Equals("Idole")){
			idoleInRec = true;
		}

	}

	void OnTriggerStay(Collider col){
		if(col.gameObject.tag.Equals("Idole")){
			if(timer<=allowedTime){
				col.gameObject.transform.position = posCenter.transform.position;
//				receptacleRotate.transform.rotation = Quaternion.Euler (new Vector3(0, col.transform.rotation.y, 0));
//				receptacleRotate.transform.eulerAngles = new Vector3( col.transform.rotation.x, 0, 0);

				receptacleRotate.transform.rotation = Quaternion.LookRotation(col.transform.forward, Vector3.up);

//				col.gameObject.transform.position = Vector3.MoveTowards(col.gameObject.transform.position, posCenter.transform.position, 0.5f);

			}
		}
	}

	void OnTriggerExit(Collider col){
		if(col.gameObject.tag.Equals("Idole")){
			idoleInRec = false;
		}
		
	}

}
