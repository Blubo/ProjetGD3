using UnityEngine;
using System.Collections;

public class AimingCanonAI : MonoBehaviour {

	[Tooltip("VIser des valeurs faibles, genre autour de 1, ptet moins, pour lookSpeed")]
	[SerializeField]
	private float viewRange, viewAngle, lookSpeed;

	private float lerpTime;

	[SerializeField]
	private LayerMask myDetectionLayer;

	private Vector3 initForward;

	private Quaternion rotationOfCanonWhenDetection;

	private bool sawSomething;

	private TurretShooting myTurretShooting;

	// Use this for initialization
	void Start () {
		myTurretShooting = transform.Find("CanonBody/CanonSystem/Canon").GetComponent<TurretShooting>();
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
		foreach(Collider find in colliders){
			if(find.gameObject.tag.Equals("Idole")==true){
				if(GetComponent<Collider>()!=find){
					float angle = Vector3.Angle(find.gameObject.transform.position-transform.position, initForward);
					if(angle<viewAngle*0.5f){
						Ray ray = new Ray(transform.position, find.gameObject.transform.position -transform.position);
						RaycastHit info;
						if(find.gameObject.GetComponent<Collider>().Raycast(ray, out info, viewRange))
						{
							return info.collider.gameObject;
						}
					}
				}
			}else if(find.gameObject.tag.Equals("Player")==true){
				if(GetComponent<Collider>()!=find){
					float angle = Vector3.Angle(find.gameObject.transform.position-transform.position, initForward);
					if(angle<viewAngle*0.5f){
						Ray ray = new Ray(transform.position, find.gameObject.transform.position -transform.position);
						RaycastHit info;
						if(find.gameObject.GetComponent<Collider>().Raycast(ray, out info, viewRange))
						{
							return info.collider.gameObject;
						}
					}
				}
			}
		}
		return null;
	}
	
	public void OnDrawGizmos(){
		Vector3 left = initForward*viewRange;
		Vector3 right = initForward*viewRange;
		
		left = Quaternion.Euler(0, -viewAngle*0.5f,0)*left;
		right = Quaternion.Euler(0, viewAngle*0.5f,0)*right;
		
		Gizmos.DrawRay(new Vector3(transform.position.x, transform.position.y+0.2f, transform.position.z), left);
		Gizmos.DrawRay(new Vector3(transform.position.x, transform.position.y+0.2f, transform.position.z), right);
	}
}
