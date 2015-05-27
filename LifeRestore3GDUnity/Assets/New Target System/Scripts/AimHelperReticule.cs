using UnityEngine;
using System.Collections;

public class AimHelperReticule : MonoBehaviour {

	private ReticuleTarget myReticuleTarget;
	private LineRenderer myLineRenderer;
	private GameObject myPlayer;
	public Material myMat;

	private int lengthOfLineRenderer = 2;
	//en rajouter pour pouvoir faire des pointillés

	// Use this for initialization
	void Start () {
		myPlayer=gameObject.transform.parent.gameObject.transform.Find("Avatar").gameObject;
		myLineRenderer = gameObject.AddComponent<LineRenderer>();

//		myLineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		myLineRenderer.material = myMat;

		if(myPlayer.transform.parent.GetComponent<ReticuleCone>().numberOfThisPlayer == 1){
			myLineRenderer.SetColors(Color.green, Color.green);

		}else if(myPlayer.transform.parent.GetComponent<ReticuleCone>().numberOfThisPlayer == 2){
			myLineRenderer.SetColors(Color.red, Color.red);

		}else if(myPlayer.transform.parent.GetComponent<ReticuleCone>().numberOfThisPlayer == 3){
			myLineRenderer.SetColors(Color.blue, Color.blue);

		}else if(myPlayer.transform.parent.GetComponent<ReticuleCone>().numberOfThisPlayer == 4){
			myLineRenderer.SetColors(Color.yellow, Color.yellow);
		}

		myLineRenderer.SetWidth(0.2F, 0.2F);
		myLineRenderer.SetVertexCount(lengthOfLineRenderer);
	}
	
	// Update is called once per frame
	void Update () {
//		Renderer[] childrenRenderers = gameObject.transform.GetComponentsInChildren<Renderer>();
//
//		if(gameObject.transform.parent.GetComponent<ShootF>()._myHook != null){
//			if(gameObject.transform.parent.GetComponent<ShootF>()._myHook.GetComponent<HookHeadF>().GrappedTo!=null){
//				foreach (Renderer renderer in childrenRenderers) {
//					if(gameObject.transform.parent.GetComponent<ShootF>().playerIndex==XInputDotNetPure.PlayerIndex.One){
//						renderer.material.color = new Color32(255, 255, 0, 50);
//					}
//					if(gameObject.transform.parent.GetComponent<ShootF>().playerIndex==XInputDotNetPure.PlayerIndex.Two){
//						renderer.material.color = new Color32(255, 0, 0, 50);
//					}
//					if(gameObject.transform.parent.GetComponent<ShootF>().playerIndex==XInputDotNetPure.PlayerIndex.Three){
//						renderer.material.color = new Color32(107, 142, 35, 50);
//					}
//					if(gameObject.transform.parent.GetComponent<ShootF>().playerIndex==XInputDotNetPure.PlayerIndex.Four){
//						renderer.material.color = new Color32(0, 0, 205, 50);
//					}
//				}
//			}
//		}
//		else{
//			foreach (Renderer renderer in childrenRenderers) {
//				if(gameObject.transform.parent.GetComponent<ShootF>().playerIndex==XInputDotNetPure.PlayerIndex.One){
//					renderer.material.color = new Color32(255, 255, 0, 180);
//				}
//				if(gameObject.transform.parent.GetComponent<ShootF>().playerIndex==XInputDotNetPure.PlayerIndex.Two){
//					renderer.material.color = new Color32(255, 0, 0, 180);
//				}
//				if(gameObject.transform.parent.GetComponent<ShootF>().playerIndex==XInputDotNetPure.PlayerIndex.Three){
//					renderer.material.color = new Color32(107, 142, 35, 180);
//				}
//				if(gameObject.transform.parent.GetComponent<ShootF>().playerIndex==XInputDotNetPure.PlayerIndex.Four){
//					renderer.material.color = new Color32(0, 0, 205, 180);
//				}
//			}
//		}
	}

	public void AimAtTarget(GameObject aimedTarget){
		myLineRenderer.enabled=true;
		Vector3 position1 =  new Vector3(aimedTarget.transform.position.x, myPlayer.transform.position.y+1, aimedTarget.transform.position.z);
		Vector3 position2 = new Vector3(myPlayer.transform.position.x, myPlayer.transform.position.y+1, myPlayer.transform.position.z);
		myLineRenderer.SetPosition(0, position1);
		myLineRenderer.SetPosition(1, position2);
		myMat.SetTextureScale("_MainTex", new Vector2(myLineRenderer.bounds.size.magnitude, 1));
		myLineRenderer.material = myMat;
		myLineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

//		Debug.DrawRay(aimedTarget.transform.position, Vector3.up*100);
//		Debug.DrawRay(myPlayer.transform.position, Vector3.up*100);
//		Vector3 center = (aimedTarget.transform.position+ myPlayer.transform.position)/2;
//
//		Debug.DrawRay(center, Vector3.up*100);
////		gameObject.transform.position = new Vector3((aimedTarget.transform.position.x+ myPlayer.transform.position.x)/2 ,myPlayer.transform.position.y, (aimedTarget.transform.position.z+ myPlayer.transform.position.z)/2);
//		gameObject.transform.position = new Vector3(center.x, myPlayer.transform.position.y, center.z);
//		gameObject.transform.LookAt(myPlayer.transform);
//		Vector3 _scale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, Vector3.Distance(aimedTarget.transform.position, myPlayer.transform.position)*2);
//		gameObject.transform.localScale=_scale;
//
//		Renderer[] childrenRenderers = gameObject.transform.GetComponentsInChildren<Renderer>();
//		
//		foreach (Renderer renderer in childrenRenderers) {
//			if(renderer.enabled==false) renderer.enabled=true;
//			if(myPlayer.transform.parent.GetComponent<ShootF>().playerIndex==XInputDotNetPure.PlayerIndex.One){
//				renderer.material.color = new Color32(255, 255, 0, 50);
//			}
//			if(myPlayer.transform.parent.GetComponent<ShootF>().playerIndex==XInputDotNetPure.PlayerIndex.Two){
//				renderer.material.color = new Color32(255, 0, 0, 50);
//			}
//			if(myPlayer.transform.parent.GetComponent<ShootF>().playerIndex==XInputDotNetPure.PlayerIndex.Three){
//				renderer.material.color = new Color32(107, 142, 35, 50);
//			}
//			if(myPlayer.transform.parent.GetComponent<ShootF>().playerIndex==XInputDotNetPure.PlayerIndex.Four){
//				renderer.material.color = new Color32(0, 0, 205, 50);
//			}
//		}
	}

	public void HideHelper(){
		if(myLineRenderer!=null) myLineRenderer.enabled=false;

//		Renderer[] childrenRenderers = gameObject.transform.GetComponentsInChildren<Renderer>();
//		
//		foreach (Renderer renderer in childrenRenderers) {
//			renderer.enabled=false;
//		}
	}
}
