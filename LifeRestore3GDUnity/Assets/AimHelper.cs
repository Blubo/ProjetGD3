using UnityEngine;
using System.Collections;

public class AimHelper : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.transform.parent.GetComponent<ShootF>()._myHook != null){

			if(gameObject.transform.parent.GetComponent<ShootF>()._myHook.GetComponent<HookHeadF>().GrappedTo!=null){
				gameObject.transform.GetComponentInChildren<Renderer>().enabled = false;
			}else{
				gameObject.transform.GetComponentInChildren<Renderer>().enabled = true;

			}
		}
		   

	}
}
