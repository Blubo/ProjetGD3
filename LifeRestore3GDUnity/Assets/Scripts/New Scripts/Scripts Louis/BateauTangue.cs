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
	private bool gotFirstInclination;

	void Start(){
		_timer=0f;
		gotFirstInclination=false;
	}

	// Update is called once per frame
	void Update () {
		_timer+=Time.deltaTime;
		Debug.Log("angle is "+v_angle);

		if(gotFirstInclination==false){
			if(_timer>=v_tangueSpeed){
//				_timer=0f;
				gotFirstInclination=true;
				v_angle*=-1;
			}
			if(v_x==true){
				gameObject.transform.Rotate(new Vector3(v_angle*Time.deltaTime/v_tangueSpeed,0,0));
			}
			
			if(v_y==true){
				gameObject.transform.Rotate(new Vector3(0,v_angle*Time.deltaTime/v_tangueSpeed,0));
			}
			
			if(v_z==true){
				gameObject.transform.Rotate(new Vector3(0,0,v_angle*Time.deltaTime/v_tangueSpeed));
			}
		}else{
			if(_timer>=v_tangueSpeed){
				_timer=0f;
				v_angle*=-1;
			}

			if(v_x==true){
				gameObject.transform.Rotate(new Vector3(-v_angle*2*Time.deltaTime/v_tangueSpeed,0,0));
			}
			
			if(v_y==true){
				gameObject.transform.Rotate(new Vector3(0,-v_angle*2*Time.deltaTime/v_tangueSpeed,0));
			}
			
			if(v_z==true){
				gameObject.transform.Rotate(new Vector3(0,0,-v_angle*2*v_tangueSpeed*Time.deltaTime/v_tangueSpeed));
			}
		}
	}
}
