using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MovingBlock : MonoBehaviour {
	
	public List<GameObject> _WaypointsList = new List<GameObject>();
	private int _currentWaypoint = 0;
	[SerializeField]
	private float v_moveSpeed;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 target = _WaypointsList[_currentWaypoint].transform.position;
		target.y = transform.position.y;
		Vector3 moveDirection = target - transform.position;
		
		if(moveDirection.magnitude < 0.1f){
			_currentWaypoint++;
			if(_currentWaypoint >= _WaypointsList.Count){
				_currentWaypoint=0;
			}
		}else{        
			//			var rotation = Quaternion.LookRotation(target - transform.position);
			//			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime*6);
			//			gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, target, 0.1f);
			
			gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target, v_moveSpeed);
			gameObject.transform.LookAt(target);
		}   
	}
}
