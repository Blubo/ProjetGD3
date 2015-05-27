using UnityEngine;
using System.Collections;

public class ChangeSizeOnTime : MonoBehaviour {

//	private float timer;
//	private int coeff;
//	
//	[SerializeField]
//	[Range(0,2)]
//	private float sizeCoeff;

	public float targetScale = 0.1f;
	public float shrinkSpeed = 0.1f;

	// Use this for initialization
	void Start () {
//		timer=0f;
//		coeff = 1;
	}
	
	// Update is called once per frame
	void Update () {
//		timer+=Time.deltaTime;
//		Vector3 grossissement = new Vector3(1f, 1f, 1f)*Time.deltaTime;
//		
//		gameObject.transform.localScale+=coeff*sizeCoeff*grossissement;
//		if(timer>0.5f){
//			coeff*=-1;
//			timer=0f;
//		}

		transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(targetScale, targetScale, targetScale), Time.deltaTime*shrinkSpeed);

	}
}
