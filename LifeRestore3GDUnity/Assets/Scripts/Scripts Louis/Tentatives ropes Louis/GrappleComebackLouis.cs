using UnityEngine;
using System.Collections;

public class GrappleComebackLouis : MonoBehaviour {

	[HideInInspector]
	public GameObject _myShooter;
	private Vector3 _myShooterInitPos, _myShooterPos;
	public float v_returnDistance;
	private bool shouldIReturn=false;

	// Use this for initialization
	void Start () {

		if(_myShooter!=null){
			_myShooterInitPos=_myShooter.transform.position;

		}
		//gameObject.transform.Find("Maillon8").GetComponent<SpringJoint>().connectedBody=_myShooter.rigidbody;

	}
	
	// Update is called once per frame
	void Update () {
		if(_myShooter!=null){

			_myShooterPos=_myShooter.transform.position;
			
			if(Vector3.Distance(gameObject.transform.position, _myShooterInitPos)>=v_returnDistance){
				shouldIReturn=true;
			}

			if(shouldIReturn==true){
	//			shouldIReturn=false;
				gameObject.rigidbody.velocity = Vector3.zero;
				gameObject.rigidbody.AddForce((_myShooterPos-gameObject.transform.position)*_myShooter.GetComponent<ShootHookLouis>()._SpeedBullet);
			}

	//		if(gameObject.transform.position==_myShooterPos && shouldIReturn==true){
	//			Debug.Log("im here");
	//			Destroy(gameObject);
	//		}

			if(Vector3.Distance(gameObject.transform.position, _myShooterPos)<=0.2f && shouldIReturn==true){
				Destroy(gameObject.transform.Find("B"));
				Destroy(gameObject);
			}
		}
	}

//	void OnCollisionEnter(Collision collision){
//		shouldIReturn=true;
////		if(collision.gameObject==_myShooter){
////			Destroy(gameObject);
////		}
//	}

//	void OnTriggerEnter(Collider collision){
//		if(collision.gameObject!=_myShooter){
//			Debug.Log("heyyy");
//			shouldIReturn=true;
//		}
//	}

	void OnCollisionEnter(Collision collision){
		if(collision.gameObject==_myShooter){
			if(shouldIReturn==true){
				Destroy(gameObject.transform.Find("B"));
				Destroy(gameObject);
				Debug.Log("kill that motherfucker");
			}
		}
	}
}
