using UnityEngine;
using System.Collections;

public class TurretConeOneBranch : MonoBehaviour {
	
	private LineRenderer myLineRenderer1;
	public Material myMat;
	
	private int lengthOfLineRenderer = 2;
	public AimingCanonAI myAimingCanonAI;
	private Vector3 initForward, initPos;

	[Tooltip("ONLY -1 OR 1")]
	[Range(-1,1)]
	public int angleDirector;
	//en rajouter pour pouvoir faire des pointillés
	
	// Use this for initialization
	void Start () {
		initPos = gameObject.transform.position;
		initForward = transform.forward;
//		myAimingCanonAI = gameObject.GetComponent<AimingCanonAI>();
		myLineRenderer1 = gameObject.AddComponent<LineRenderer>();
		myLineRenderer1.material = myMat;		
		myLineRenderer1.SetWidth(0.3F, 0.3F);
		myLineRenderer1.SetColors(Color.white, Color.white);
		myLineRenderer1.SetVertexCount(lengthOfLineRenderer);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 vector= initForward*myAimingCanonAI.viewRange;
		vector = Quaternion.Euler(0, angleDirector*myAimingCanonAI.viewAngle*0.5f,0)*vector;
		AimAtTarget(myLineRenderer1, initPos+vector);
	}
	
	public void AimAtTarget(LineRenderer lineRend, Vector3 aimedTarget1){
		Vector3 position1 =  new Vector3(aimedTarget1.x, aimedTarget1.y, aimedTarget1.z);
		Vector3 position2 = initPos;
		
		lineRend.SetPosition(0, position1);
		lineRend.SetPosition(1, position2);

//		myMat.SetTextureScale("_MainTex", new Vector2(lineRend.bounds.size.magnitude, 1));
		myMat.SetTextureScale("_MainTex", new Vector2(6.4f, 1));

		lineRend.material = myMat;
		lineRend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		
	}
	
	public void HideHelper(){
		myLineRenderer1.enabled=false;
	}
}
