using UnityEngine;
using System.Collections;

public class LinkInTheMiddle : MonoBehaviour {

	//v_C=mon parent
	public GameObject v_A, v_C;
	private Vector3 _scale;

	[HideInInspector]
	public Vector3 _whereIsItShot;
	private Color _myColor;
	private float _blinkTimer;

//	public float v_sizeRatio;

	// Use this for initialization
	void Start () {
//		v_A=v_C.GetComponent<GrappleComebackLouis5Janv>()._myShooter;
//		v_A=v_C.GetComponent<NewHookHead>()._myShooter;
		v_A=v_C.GetComponent<HookHeadF>()._myShooter;
		_myColor = gameObject.GetComponent<Renderer>().material.color;
		_blinkTimer=0f;

	}
	
	// Update is called once per frame
	void Update () {
		//anchorOnCircle est l'intersection du cercle de l'avatar (le rayon 5) et du vecteur v_A;v_C
		Vector3 anchorOnCircle = v_C.transform.position-v_A.transform.position;
		anchorOnCircle.Normalize();

//		anchorOnCircle = v_A.transform.position + anchorOnCircle*v_sizeRatio;

		anchorOnCircle = v_A.transform.position + anchorOnCircle*v_C.GetComponent<HookHeadF>()._myShooter.GetComponent<ShootF>().v_sizeRatio;
//		Debug.Log("v_C.GetComponent<HookHeadF>()._myShooter.GetComponent<ShootF>().v_sizeRatio is "+v_C.GetComponent<HookHeadF>()._myShooter.GetComponent<ShootF>().v_sizeRatio);
		_whereIsItShot=anchorOnCircle;

		Debug.DrawRay(anchorOnCircle, Vector3.up);

		//SWITCH THE COMMENTS ON THE TWO NEXT LINES FOR MANAGING THE WIDTH OF THE AVATAR
//		gameObject.transform.position = (v_A.transform.position+ v_C.transform.position)/2;
		gameObject.transform.position = (anchorOnCircle+ v_C.transform.position)/2;

		gameObject.transform.LookAt(v_C.transform);

		//SWITCH THE COMMENTS ON THE TWO NEXT LINES FOR MANAGING THE WIDTH OF THE AVATAR
//		_scale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, Vector3.Distance(v_A.transform.position, v_C.transform.position)/2);
		_scale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, Vector3.Distance(anchorOnCircle, v_C.transform.position)*2);

		gameObject.transform.localScale=_scale;

		if(v_C.GetComponent<HookHeadF>().GrappedTo!=null){
//			if(Vector3.Distance(v_A.transform.position, v_C.transform.position)>=0.85f*Vector3.Distance(v_A.transform.position, v_C.GetComponent<HookHeadF>().GrappedTo.transform.position)){
			if(gameObject.transform.parent.GetComponent<HookHeadF>().newLinkSystem == false){
				if(Vector3.Distance(v_A.transform.position, v_C.transform.position)>=gameObject.transform.parent.GetComponent<HookHeadF>()._myShooter.GetComponent<ElasticScript>().v_tensionLessDistanceRatio*v_C.GetComponent<HookHeadF>().v_BreakDistance){
					_blinkTimer+=Time.deltaTime;
					if(_blinkTimer>0.2f){
						gameObject.GetComponent<Renderer>().material.color=Color.white;
						_blinkTimer=0f;
					}else{
						gameObject.GetComponent<Renderer>().material.color=_myColor;
					}
				}else{
					gameObject.GetComponent<Renderer>().material.color=_myColor;
				}
			}else{
				if(Vector3.Distance(v_A.transform.position, v_C.transform.position)>=gameObject.transform.parent.GetComponent<HookHeadF>().newTensionLessDistance){
					_blinkTimer+=Time.deltaTime;
					if(_blinkTimer>0.2f){
						gameObject.GetComponent<Renderer>().material.color=Color.white;
						_blinkTimer=0f;
					}else{
						gameObject.GetComponent<Renderer>().material.color=_myColor;
					}
				}else{
					gameObject.GetComponent<Renderer>().material.color=_myColor;
				}
			}
		}
	}
}
