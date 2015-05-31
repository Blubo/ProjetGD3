﻿using UnityEngine;
using System.Collections;

public class FatPlayerScript : MonoBehaviour {

	[SerializeField]
	private float timeForGrowth, maxSizeRelativeToBeginning;

	private float initScaleX, initScaleY, initScaleZ;
	private Vector3 initScale, minScale, maxScale, myScale;

	private Player_Status myPlayerStatus;
	[SerializeField]
	private int maxScoreForSize = 1000;
	private float sizeRatio, sizeRatioPercent;

	// Use this for initialization
	void Start () {
		myPlayerStatus = GetComponent<Player_Status>();
		initScale = gameObject.transform.localScale;
		myScale = initScale;
		minScale = initScale;
		initScaleX = initScale.x;
		initScaleY = initScale.y;
		initScaleZ = initScale.z;
		maxScale = minScale*maxSizeRelativeToBeginning;
	}
	
	// Update is called once per frame
	void Update () {
		//NEW
		sizeRatio = (float) myPlayerStatus.myScore/maxScoreForSize;
//		sizeRatioPercent = sizeRatio * 100;
//		Debug.Log(myPlayerStatus.myScore);
//		Debug.Log(sizeRatio);
//				Debug.Log("localscale "+gameObject.transform.localScale);

//		Debug.Log("localscale mag "+gameObject.transform.localScale.magnitude);
//		Debug.Log("initscale "+initScale.magnitude);

//		gameObject.transform.localScale = maxScale * sizeRatioPercent/100;
		gameObject.transform.localScale = maxScale * sizeRatio + initScale;

		gameObject.transform.localScale = Vector3.ClampMagnitude(transform.localScale, maxScale.magnitude);

	
		if(gameObject.transform.localScale.magnitude<initScale.magnitude){
			gameObject.transform.localScale = initScale;
		}

//		myScale = Vector3.ClampMagnitude(transform.localScale, maxScale.magnitude);
//		if(myScale.magnitude<initScale.magnitude){
//			myScale = initScale;
//		}
//		gameObject.transform.localScale = myScale;
	}
	
//	public void ChangeSize(float value){
//		Vector3 startScale = transform.localScale;
//		Vector3 growthScale = startScale * value;
//
//		//		if(transform.localScale.magnitude<maxScale.magnitude && transform.localScale.magnitude >= minScale.magnitude){
//		if(growthScale.magnitude<maxScale.magnitude && growthScale.magnitude >= minScale.magnitude){
//			//		if(transform.localScale.magnitude<maxScale.magnitude){
//			StartCoroutine(ScaleLinear(transform, startScale, growthScale, timeForGrowth)); 
//		}
////		else if(growthScale.magnitude>maxScale.magnitude){
////			StartCoroutine(ScaleLinear(transform, startScale, maxScale, timeForGrowth)); 
////		}else if(growthScale.magnitude <= minScale.magnitude){
////			StartCoroutine(ScaleLinear(transform, startScale, minScale, timeForGrowth)); 
////		}
//	}
//	
//	IEnumerator ScaleLinear (Transform myScale, Vector3 startScale, Vector3 endScale, float time){
//		float i = 0;
//		float rate = 1/time;
//		while (i < 1) {
//			i += Time.deltaTime * rate;
//			myScale.localScale = Vector3.Lerp(startScale, endScale, i);
//			yield return null; 
//		}
//	}
}
