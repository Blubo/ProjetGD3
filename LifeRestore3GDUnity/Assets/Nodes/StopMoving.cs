using UnityEngine;
using System.Collections;

public class StopMoving : MonoBehaviour {

	public GameObject v_daddy;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = v_daddy.transform.position;
		if(gameObject.transform.position == v_daddy.transform.position){
			float positionY = new float();
			positionY=gameObject.transform.position.y+1;
			gameObject.transform.position=new Vector3(gameObject.transform.position.x, positionY,gameObject.transform.position.z);
		}
	}
}
