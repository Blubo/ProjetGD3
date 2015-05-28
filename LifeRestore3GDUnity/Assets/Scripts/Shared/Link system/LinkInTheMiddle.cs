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
	
	// Use this for initialization
	void Start () {
		v_A=v_C.GetComponent<HookHeadF>()._myShooter.GetComponent<ShootF>().v_visuelBouche;
		_myColor = gameObject.GetComponent<Renderer>().material.color;
		_blinkTimer=0f;
		gameObject.GetComponent<Renderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(		gameObject.GetComponent<Renderer>().enabled == false) 		gameObject.GetComponent<Renderer>().enabled = true;

		//anchorOnCircle est l'intersection du cercle de l'avatar (le rayon 5) et du vecteur v_A;v_C
		Vector3 anchorOnCircle = v_C.transform.position-v_A.transform.position;
		anchorOnCircle.Normalize();

		anchorOnCircle = v_A.transform.position + anchorOnCircle*v_C.GetComponent<HookHeadF>()._myShooter.GetComponent<ShootF>().v_sizeRatio;
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
