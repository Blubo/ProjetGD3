﻿using UnityEngine;
using System.Collections;

public class Rotating : MonoBehaviour {
	

	[SerializeField]
	private float angleCoeff;

	private Vector3 initVectorUp;
	
	// Use this for initialization
	void Start () {
		initVectorUp = transform.up;
	}
	
	// Update is called once per frame
	void Update () {
		float rotateAngle = angleCoeff*Time.deltaTime;
		gameObject.transform.Rotate(initVectorUp, rotateAngle);
//		Vector3 lookAt = Camera.main.transform.position - transform.position;
//		gameObject.transform.eulerAngles = new Vector3(0,0,rotateAngle);
	}
}
