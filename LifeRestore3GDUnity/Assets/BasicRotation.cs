using UnityEngine;
using System.Collections;

public class BasicRotation : MonoBehaviour {

	[Header("Gestion rotation")]
	[Tooltip("angle=angle par seconde")]
	[Range(-360,360)]
	public int angle;
	[Space(2)]
	public bool v_x, v_y, v_z;

	// Update is called once per frame
	void Update () {
		if(v_x==true){
			gameObject.transform.Rotate(new Vector3(angle*Time.deltaTime,0,0));
		}

		if(v_y==true){
			gameObject.transform.Rotate(new Vector3(0,angle*Time.deltaTime,0));
		}

		if(v_z==true){
			gameObject.transform.Rotate(new Vector3(0,0,angle*Time.deltaTime));
		}
	}
}
