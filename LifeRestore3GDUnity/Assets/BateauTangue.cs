using UnityEngine;
using System.Collections;

public class BateauTangue : MonoBehaviour {

	[Header("Gestion rotation")]
	[Tooltip("angle=angle par seconde")]
	[Range(-360,360)]
	public int v_angle;
	[Space(1)]
	public bool v_x, v_y, v_z;

	public float v_tangueSpeed;
	private float _timer;

	void Start(){
		_timer=0f;
	}

	// Update is called once per frame
	void Update () {
		_timer+=Time.deltaTime;

		if(_timer>=v_tangueSpeed){
			v_angle*=-1;
			_timer=0f;


		}
//		if(v_x==true){
//			gameObject.transform.Rotate(new Vector3(v_angle*Time.deltaTime,0,0));
//		}
//		
//		if(v_y==true){
//			gameObject.transform.Rotate(new Vector3(0,v_angle*Time.deltaTime,0));
//		}
//		
//		if(v_z==true){
//			gameObject.transform.Rotate(new Vector3(0,0,v_angle*Time.deltaTime));
//		}
	}
}
