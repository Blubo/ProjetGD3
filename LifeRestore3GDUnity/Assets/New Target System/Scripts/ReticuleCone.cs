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

	//REMOVE
	private LineRenderer myLineRenderer;
	public Material myMat;
	//END REMOVE

	// Use this for initialization
	void Start () {
		myShootF = gameObject.GetComponent<ShootF>();
		HelpScript = gameObject.transform.Find("Pointillés").gameObject.GetComponent<AimHelperReticule>();

		//REMOVE
		if(numberOfThisPlayer == 1){
			myLineRenderer = gameObject.AddComponent<LineRenderer>();
			myLineRenderer.material = myMat;
			myLineRenderer.SetColors(Color.yellow, Color.yellow);
			myLineRenderer.SetWidth(0.2F, 0.2F);
			myLineRenderer.SetVertexCount(3);
	
		}
		//END REMOVE
	}
	
	// Update is called once per frame
	void Update () {

		//REMOVE

		if (numberOfThisPlayer == 1) {
			Vector3 left = transform.forward * viewRange;
			Vector3 right = transform.forward * viewRange;

			left = Quaternion.Euler (0, -viewAngle * 0.5f, 0) * left;
			right = Quaternion.Euler (0, viewAngle * 0.5f, 0) * right;

			myLineRenderer.enabled = true;

			Vector3 position1 = gameObject.transform.position+left;
			Vector3 position2 = new Vector3 (transform.position.x, transform.position.y + 1, transform.position.z);
			Vector3 position3 = gameObject.transform.position+right;
			myLineRenderer.SetPosition (0, position1);
			myLineRenderer.SetPosition (1, position2);
			myLineRenderer.SetPosition (2, position3);

			myMat.SetTextureScale ("_MainTex", new Vector2 (myLineRenderer.bounds.size.magnitude, 1));
			myLineRenderer.material = myMat;
			myLineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		}

		//END REMOVE


		Debug.DrawRay(transform.position, transform.forward.normalized*viewRange);

		if(lastSeen!=null){
			Debug.DrawRay(lastSeen.transform.position, Vector3.up*100);
			if(lastSeen != Vision() || Vision()==null){
				if(lastSeen.GetComponent<ReticuleTarget>()!=null){
//					if(myShootF._myHook == null || myShootF._myHook.GetComponent<HookHeadF>().GrappedTo==null){
						ReticuleTarget lastSeenTargetScript = lastSeen.GetComponent<ReticuleTarget>();
						
						switch (numberOfThisPlayer) {
						case 1:
							lastSeenTargetScript.TurnReticuleOff(lastSeenTargetScript.YRend);
							HelpScript.HideHelper();
							break;
							
						case 2:
							lastSeenTargetScript.TurnReticuleOff(lastSeenTargetScript.RRend);
							HelpScript.HideHelper();

							break;
						case 3:
							lastSeenTargetScript.TurnReticuleOff(lastSeenTargetScript.GRend);
							HelpScript.HideHelper();

							break;
						case 4:
							lastSeenTargetScript.TurnReticuleOff(lastSeenTargetScript.BRend);
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

				switch (numberOfThisPlayer) {
				case 1:
					reticuleTarget.LightReticuleUp(reticuleTarget.YRend);
					HelpScript.AimAtTarget(Vision());
					break;

				case 2:
					reticuleTarget.LightReticuleUp(reticuleTarget.RRend);
					HelpScript.AimAtTarget(Vision());

					break;
				case 3:
					reticuleTarget.LightReticuleUp(reticuleTarget.GRend);
					HelpScript.AimAtTarget(Vision());

					break;
				case 4:
					reticuleTarget.LightReticuleUp(reticuleTarget.BRend);
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
//					GameObject target = find.gameObject;
//
//					if(target!=null){
//						float angle = Vector3.Angle(target.transform.position-transform.position, transform.forward);
//
//						if(angle<viewAngle*0.5f){
//							Ray ray = new Ray(transform.position, target.transform.position -transform.position);
//							RaycastHit info;
//
//							if(target.GetComponent<Collider>().Raycast(ray, out info, viewRange)){
//								mago = target;
//								return true;
//							}
//						}
//					}

					for (int i = 0; i < viewAngle*0.5f; i++) {
						Vector3 left = transform.forward*viewRange;
						Vector3 right = transform.forward*viewRange;
						
						left = Quaternion.Euler(0, -i,0)*left;
						right = Quaternion.Euler(0, i,0)*right;

						Ray ray1 = new Ray(transform.position, left);
						Ray ray2 = new Ray(transform.position, right);
						
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
		
		Gizmos.DrawRay(transform.position, left);
		Gizmos.DrawRay(transform.position, right);
	}
}
