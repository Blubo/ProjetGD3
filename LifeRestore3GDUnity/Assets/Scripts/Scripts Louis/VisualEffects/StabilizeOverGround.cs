using UnityEngine;
using System.Collections;

public class StabilizeOverGround : MonoBehaviour {

	[SerializeField]
	private float allowedheight, stabilizedHeight;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.transform.position.y <= allowedheight){
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, stabilizedHeight, gameObject.transform.position.z);
		}
	}
}
