using UnityEngine;
using System.Collections;

public class DeactivatedGoInground : MonoBehaviour {

	[Tooltip("This is the end position of this appearing cube")]
	[SerializeField]
	private GameObject v_target;

	private bool receivedOrder = false;
	private Vector3 initPos;

	// Use this for initialization
	void Start () {
		initPos = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(receivedOrder==true){
			if(gameObject.transform.Find("ReceptacleCollider")!=null){
				gameObject.transform.Find("ReceptacleCollider").GetComponent<ReceptacleKey>().enabled=false;
			}
			gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, v_target.transform.position, 0.1f);
		}

		if(gameObject.transform.position == v_target.transform.position){
			receivedOrder=false;
	
		}
	}

	void Deactivated(){
		receivedOrder=true;
	}

}
