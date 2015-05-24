using UnityEngine;
using System.Collections;

public class TurretConeVision : MonoBehaviour {
	
	private LineRenderer myLineRenderer1;
	public Material myMat;
	
	private int lengthOfLineRenderer = 3;
	private AimingCanonAI myAimingCanonAI;
	private Vector3 initForward, initPos;
	//en rajouter pour pouvoir faire des pointillés
	
	// Use this for initialization
	void Start () {
		initPos = gameObject.transform.position;
		initForward = transform.forward;
		myAimingCanonAI = gameObject.GetComponent<AimingCanonAI>();
		myLineRenderer1 = gameObject.AddComponent<LineRenderer>();
		myLineRenderer1.material = myMat;		
		myLineRenderer1.SetWidth(0.3F, 0.3F);
		myLineRenderer1.SetColors(Color.white, Color.white);
		myLineRenderer1.SetVertexCount(lengthOfLineRenderer);
	}
	
	// Update is called once per frame
	void Update () {
//
//		Vector3 left = initPos.transform.TransformPoint( initForward*myAimingCanonAI.viewRange);
//		Vector3 right = initPos.transform.TransformPoint( initForward*myAimingCanonAI.viewRange);
		Vector3 left= initForward*myAimingCanonAI.viewRange;

		Vector3 right = initForward*myAimingCanonAI.viewRange;

//		Vector3 left = myAimingCanonAI.initForward*myAimingCanonAI.viewRange;
//		Vector3 right = myAimingCanonAI.initForward*myAimingCanonAI.viewRange;

//		Vector3 left = transform.forward*myAimingCanonAI.viewRange;
//		Vector3 right = transform.forward*myAimingCanonAI.viewRange;
//		
		left = Quaternion.Euler(0, -myAimingCanonAI.viewAngle*0.5f,0)*left;
		right = Quaternion.Euler(0, myAimingCanonAI.viewAngle*0.5f,0)*right;


		AimAtTarget(myLineRenderer1, initPos+left, initPos+right);
	}
	
	public void AimAtTarget(LineRenderer lineRend, Vector3 aimedTarget1, Vector3 aimedTarget2){
		Vector3 position1 =  new Vector3(aimedTarget1.x, aimedTarget1.y, aimedTarget1.z);
//		Vector3 position2 = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
		Vector3 position2 = initPos;

		Vector3 position3 = new Vector3(aimedTarget2.x, aimedTarget2.y, aimedTarget2.z);
		lineRend.SetPosition(0, position1);
		lineRend.SetPosition(1, position2);
		lineRend.SetPosition(2, position3);

//		myMat.SetTextureScale("_MainTex", new Vector2(lineRend.bounds.size.magnitude/2, 1));
		lineRend.material = myMat;
		lineRend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

	}
	
	public void HideHelper(){
		myLineRenderer1.enabled=false;
	}
}
