using UnityEngine;
using System.Collections;

public class ReticuleCone : MonoBehaviour {
	
	public float viewRange, viewAngle;

	[SerializeField]
	private LayerMask myDetectionLayer;
	
	public int numberOfThisPlayer;

	private ShootF myShootF;

	private GameObject lastSeen;
	private AimHelperReticule HelpScript;

	// Use this for initialization
	void Start () {
		myShootF = gameObject.GetComponent<ShootF>();
		HelpScript = gameObject.transform.Find("Pointillés").gameObject.GetComponent<AimHelperReticule>();
	}
	
	// Update is called once per frame
	void Update () {

//		Debug.DrawRay(transform.position, transform.forward.normalized*viewRange);

		if(lastSeen!=null){
//			Debug.DrawRay(lastSeen.transform.position, Vector3.up*100);
			if(lastSeen != Vision() || Vision()==null){
				if(lastSeen.GetComponent<ReticuleTarget>()!=null){
//					if(myShootF._myHook == null || myShootF._myHook.GetComponent<HookHeadF>().GrappedTo==null){
						ReticuleTarget lastSeenTargetScript = lastSeen.GetComponent<ReticuleTarget>();
						
//						if(lastSeen.tag.Equals("Unlinkable")){
//							lastSeenTargetScript.TurnReticuleOff(lastSeenTargetScript.GREYRend);
//							HelpScript.HideHelper();
//							lastSeen = null;
//							return;
//						}

						switch (numberOfThisPlayer) {
						case 1:
							lastSeenTargetScript.TurnReticuleOff(lastSeenTargetScript.GRend);
							HelpScript.HideHelper();
							break;
							
						case 2:
							lastSeenTargetScript.TurnReticuleOff(lastSeenTargetScript.RRend);
							HelpScript.HideHelper();

							break;
						case 3:
							lastSeenTargetScript.TurnReticuleOff(lastSeenTargetScript.BRend);
							HelpScript.HideHelper();

							break;
						case 4:
							lastSeenTargetScript.TurnReticuleOff(lastSeenTargetScript.YRend);
							HelpScript.HideHelper();

							break;
							
						default:
							break;
						}
//					}
				}
			}
			lastSeen=null;
		}

		if(Vision()!=null){
			if(Vision().GetComponent<ReticuleTarget>()!=null){
				lastSeen = Vision();
				GameObject target = Vision();
				ReticuleTarget reticuleTarget = target.GetComponent<ReticuleTarget>();
//				Debug.Log(Vision().name);

//				if(Vision().tag.Equals("Unlinkable")){
//					reticuleTarget.LightReticuleUp(reticuleTarget.GREYRend);
//					HelpScript.AimAtTarget(Vision());
//					return;
//				}

				switch (numberOfThisPlayer) {
				case 1:
					reticuleTarget.LightReticuleUp(reticuleTarget.GRend);
					HelpScript.AimAtTarget(Vision());
					break;

				case 2:
					reticuleTarget.LightReticuleUp(reticuleTarget.RRend);
					HelpScript.AimAtTarget(Vision());

					break;
				case 3:
					reticuleTarget.LightReticuleUp(reticuleTarget.BRend);
					HelpScript.AimAtTarget(Vision());

					break;
				case 4:
					reticuleTarget.LightReticuleUp(reticuleTarget.YRend);
					HelpScript.AimAtTarget(Vision());

					break;

				default:
				break;
				}
			}
		}
	}

	public GameObject Vision(){

		Collider[] colliders = Physics.OverlapSphere(transform.position, viewRange, myDetectionLayer);

		foreach(Collider find in colliders){

			if(find.gameObject.tag.Equals("Unlinkable")==false){

				if(GetComponent<Collider>()!=find){
					for (int i = 0; i < viewAngle*0.5f; i++) {
						Vector3 left = transform.forward*viewRange;
						Vector3 right = transform.forward*viewRange;
						
						left = Quaternion.Euler(0, -i,0)*left;
						right = Quaternion.Euler(0, i,0)*right;

						Ray ray1 = new Ray(new Vector3(transform.position.x, transform.position.y+0.2f, transform.position.z), left);
						Ray ray2 = new Ray(new Vector3(transform.position.x, transform.position.y+0.2f, transform.position.z), right);
						
						RaycastHit info;
						if(Physics.Raycast(ray1, out info, viewRange, myDetectionLayer) ||Physics.Raycast(ray2, out info, viewRange, myDetectionLayer)){
							return info.collider.gameObject;
						}
					}
				}
			}
		}
		return null;
	}

	public void OnDrawGizmos(){
		Vector3 left = transform.forward*viewRange;
		Vector3 right = transform.forward*viewRange;
		
		left = Quaternion.Euler(0, -viewAngle*0.5f,0)*left;
		right = Quaternion.Euler(0, viewAngle*0.5f,0)*right;
		
		Gizmos.DrawRay(new Vector3(transform.position.x, transform.position.y+0.2f, transform.position.z), left);
		Gizmos.DrawRay(new Vector3(transform.position.x, transform.position.y+0.2f, transform.position.z), right);
	}
}
