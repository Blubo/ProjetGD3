using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class AimingCanonAI : MonoBehaviour {

	[Tooltip("VIser des valeurs faibles, genre autour de 1, ptet moins, pour lookSpeed")]
	public float viewRange, viewAngle, lookSpeed;

	private float lerpTime;

	[SerializeField]
	private LayerMask myDetectionLayer;

	[HideInInspector]
	public Vector3 initForward;

	private Quaternion rotationOfCanonWhenDetection;

	private bool sawSomething;

	private TurretShooting myTurretShooting;
	private TurretConeVision myTurretConeVision;

	// Use this for initialization
	void Start () {
		myTurretShooting = transform.Find("CanonBody/CanonSystem/Canon").GetComponent<TurretShooting>();
		myTurretConeVision = gameObject.GetComponent<TurretConeVision>();
		sawSomething = false;
		initForward = transform.forward;
		lerpTime=0;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawRay(transform.position, transform.forward.normalized*viewRange);
		if(Vision()!=null){
			if(sawSomething == false){
			}
			sawSomething = true;

			Vector3 targetDir = Vision().transform.position - transform.position;
			if(Vision().tag.Equals("Player") || Vision().tag.Equals("Idole") ){
				targetDir = new Vector3(Vision().transform.position.x, Vision().transform.position.y+0.75f, Vision().transform.position.z) - transform.position;
			}

			float step = lookSpeed * Time.deltaTime;
			Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
			transform.rotation = Quaternion.LookRotation(newDir);
			myTurretShooting.automate = true;

//			Quaternion targetRotation = Quaternion.LookRotation(Vision().transform.position - transform.position);
//			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lookSpeed * Time.deltaTime);		
		}else{
			myTurretShooting.automate = false;

			float step = lookSpeed * Time.deltaTime;
			Vector3 newDir = Vector3.RotateTowards(transform.forward, initForward, step, 0.0F);
			transform.rotation = Quaternion.LookRotation(newDir);
		}
	}

	public GameObject Vision(){
		
		Collider[] colliders = Physics.OverlapSphere(transform.position, viewRange, myDetectionLayer);

//		if(colliders.Contains(GameObject.Find("Idole").GetComponent<Collider>())){
//		}

//		foreach(Collider find in colliders){
//			if(find.gameObject.tag.Equals("Idole")==true){
//				if(GetComponent<Collider>()!=find){
//					float angle = Vector3.Angle(find.gameObject.transform.position-transform.position, initForward);
//					if(angle<viewAngle*0.5f){
//						Ray ray = new Ray(transform.position, find.gameObject.transform.position -transform.position);
//						RaycastHit info;
//						if(find.gameObject.GetComponent<Collider>().Raycast(ray, out info, viewRange))
//						{
//							return info.collider.gameObject;
//						}
//					}
//				}
//			}else if(find.gameObject.tag.Equals("Player")==true){
//				if(GetComponent<Collider>()!=find){
//					float angle = Vector3.Angle(find.gameObject.transform.position-transform.position, initForward);
//					if(angle<viewAngle*0.5f){
//						Ray ray = new Ray(transform.position, find.gameObject.transform.position -transform.position);
//						RaycastHit info;
//						if(find.gameObject.GetComponent<Collider>().Raycast(ray, out info, viewRange))
//						{
//							return info.collider.gameObject;
//						}
//					}
//				}
//			}
//		}
		List<Collider> secondSelection = new List<Collider>();
		foreach(Collider find in colliders){
			if(GetComponent<Collider>()!=find){
				float angle = Vector3.Angle(find.gameObject.transform.position-transform.position, initForward);
				if(angle<viewAngle*0.5f){
					secondSelection.Add(find);
				}
			}
		}

		if(secondSelection.Contains(GameObject.Find("Idole").GetComponent<Collider>())){
			GameObject idole = GameObject.Find("Idole");
			Ray ray = new Ray(transform.position, idole.transform.position -transform.position);
			RaycastHit info;
			if(idole.GetComponent<Collider>().Raycast(ray, out info, viewRange))
			{
				return info.collider.gameObject;
			}
		} else {
			foreach(Collider find in secondSelection){
				if (find.gameObject.tag.Equals("Player")==true){
					Ray ray = new Ray(transform.position, find.gameObject.transform.position -transform.position);
					RaycastHit info;
					if(find.gameObject.GetComponent<Collider>().Raycast(ray, out info, viewRange))
					{
						return info.collider.gameObject;
					}
				}
			}
		}

		return null;
	}
	
//	public void OnDrawGizmos(){
//		Vector3 left = initForward*viewRange;
//		Vector3 right = initForward*viewRange;
//		
//		left = Quaternion.Euler(0, -viewAngle*0.5f,0)*left;
//		right = Quaternion.Euler(0, viewAngle*0.5f,0)*right;
//		
//		Gizmos.DrawRay(new Vector3(transform.position.x, transform.position.y+0.2f, transform.position.z), left);
//		Gizmos.DrawRay(new Vector3(transform.position.x, transform.position.y+0.2f, transform.position.z), right);
//	}
}
