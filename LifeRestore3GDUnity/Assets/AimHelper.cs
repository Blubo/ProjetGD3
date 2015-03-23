using UnityEngine;
using System.Collections;

public class AimHelper : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Renderer[] childrenRenderers = gameObject.transform.GetComponentsInChildren<Renderer>();

		if(gameObject.transform.parent.GetComponent<ShootF>()._myHook != null){
			if(gameObject.transform.parent.GetComponent<ShootF>()._myHook.GetComponent<HookHeadF>().GrappedTo!=null){
				foreach (Renderer renderer in childrenRenderers) {
					if(gameObject.transform.parent.GetComponent<ShootF>().playerIndex==XInputDotNetPure.PlayerIndex.One){
						renderer.material.color = new Color32(255, 255, 0, 50);
					}
					if(gameObject.transform.parent.GetComponent<ShootF>().playerIndex==XInputDotNetPure.PlayerIndex.Two){
						renderer.material.color = new Color32(255, 0, 0, 50);
					}
					if(gameObject.transform.parent.GetComponent<ShootF>().playerIndex==XInputDotNetPure.PlayerIndex.Three){
						renderer.material.color = new Color32(107, 142, 35, 50);
					}
					if(gameObject.transform.parent.GetComponent<ShootF>().playerIndex==XInputDotNetPure.PlayerIndex.Four){
						renderer.material.color = new Color32(0, 0, 205, 50);
					}
				}
			}
		}else{
			foreach (Renderer renderer in childrenRenderers) {
				if(gameObject.transform.parent.GetComponent<ShootF>().playerIndex==XInputDotNetPure.PlayerIndex.One){
					renderer.material.color = new Color32(255, 255, 0, 180);
				}
				if(gameObject.transform.parent.GetComponent<ShootF>().playerIndex==XInputDotNetPure.PlayerIndex.Two){
					renderer.material.color = new Color32(255, 0, 0, 180);
				}
				if(gameObject.transform.parent.GetComponent<ShootF>().playerIndex==XInputDotNetPure.PlayerIndex.Three){
					renderer.material.color = new Color32(107, 142, 35, 180);
				}
				if(gameObject.transform.parent.GetComponent<ShootF>().playerIndex==XInputDotNetPure.PlayerIndex.Four){
					renderer.material.color = new Color32(0, 0, 205, 180);
				}
			}
		}
		   

	}
}
