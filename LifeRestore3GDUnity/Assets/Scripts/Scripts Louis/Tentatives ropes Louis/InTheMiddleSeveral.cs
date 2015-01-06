using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InTheMiddleSeveral : MonoBehaviour {

	public GameObject v_projo;
	public List<GameObject> maillons;
	private Vector3 _scale;
	private int _nombreMaillons;

	// Use this for initialization
	void Start () {
		_nombreMaillons = maillons.Count;
//		float _distance = Vector3.Distance(gameObject.transform.position, v_projo.transform.position);
//		
//		_scale = new Vector3(maillons[0].gameObject.transform.localScale.x-0.5f, maillons[0].gameObject.transform.localScale.y, _distance/_nombreMaillons);
	
	}
	
	// Update is called once per frame
	void Update () {
		//(╯°□°)╯


//		for (int i = 0; i < _nombreMaillons; i++) {
////			maillons[i].gameObject.transform.position = ((v_projo.transform.position + gameObject.transform.position)*i)/(_nombreMaillons+1);
////			maillons[i].gameObject.transform.position = ((v_projo.transform.position - gameObject.transform.position)*i)/(_nombreMaillons+1);
////			maillons[i].gameObject.transform.position = ((v_projo.transform.position - gameObject.transform.position)*(i+1))/(_nombreMaillons+1);
//
//			//maillons[i].gameObject.transform.position = ((v_projo.transform.position + gameObject.transform.position)*(i+1))/(_nombreMaillons+1);
//
//			maillons[i].gameObject.transform.position = transform.TransformPoint( ((v_projo.transform.position - gameObject.transform.position)*(i+1))/(_nombreMaillons+1));
//
////			maillons[i].gameObject.transform.LookAt(v_projo.transform);
//			maillons[i].gameObject.transform.forward = v_projo.transform.position - gameObject.transform.position;
//			float _distance = Vector3.Distance(gameObject.transform.position, v_projo.transform.position);
//			
//			_scale = new Vector3(maillons[0].gameObject.transform.localScale.x, maillons[0].gameObject.transform.localScale.y, _distance/_nombreMaillons);
//			maillons[i].gameObject.transform.localScale=_scale;
//		}

		for (int i = 1; i <= _nombreMaillons; i++) {
			//			maillons[i].gameObject.transform.position = ((v_projo.transform.position + gameObject.transform.position)*i)/(_nombreMaillons+1);
			//			maillons[i].gameObject.transform.position = ((v_projo.transform.position - gameObject.transform.position)*i)/(_nombreMaillons+1);
			//			maillons[i].gameObject.transform.position = ((v_projo.transform.position - gameObject.transform.position)*(i+1))/(_nombreMaillons+1);
			
			//maillons[i].gameObject.transform.position = ((v_projo.transform.position + gameObject.transform.position)*(i+1))/(_nombreMaillons+1);
			
			maillons[i].gameObject.transform.position = transform.TransformPoint( ((v_projo.transform.position - gameObject.transform.position)*(i))/(_nombreMaillons+1));
			
			//			maillons[i].gameObject.transform.LookAt(v_projo.transform);
			maillons[i].gameObject.transform.forward = v_projo.transform.position - gameObject.transform.position;
			float _distance = Vector3.Distance(gameObject.transform.position, v_projo.transform.position);
			
			_scale = new Vector3(maillons[1].gameObject.transform.localScale.x, maillons[1].gameObject.transform.localScale.y, _distance/_nombreMaillons);
			maillons[i].gameObject.transform.localScale=_scale;
		}
	}
}
