using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ShootHookLouis : MonoBehaviour {

	private float _timer = 1.5f, necessaryBullets;
	//taille du projectile
	private float bulletSize=1f;
	public float _SpeedBullet, distanceRay;
	public GameObject v_Bullet, _HookHead;
	private GameObject _target;
	private List<GameObject> maillons;

	// Use this for initialization
	void Start () {
		maillons = new List<GameObject>();
		if(_HookHead.GetComponent<PelletScript>().enabled!=null){

			_HookHead.GetComponent<PelletScript>().enabled=false;
		}
	}
	
	// Update is called once per frame
	void Update () {

		_timer += 1 *Time.deltaTime;
		if(_timer>=1.5f){
//			if(Input.GetMouseButton(0)){
			if(Input.GetKey(KeyCode.Space)){

//				RaycastHit _hit;
//				if (Physics.Raycast (transform.position, gameObject.transform.forward, out _hit, distanceRay)) {
//					_target = Instantiate(_HookHead, _hit.point, Quaternion.identity)as GameObject;
//				}else{
//					_target = Instantiate(_HookHead, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z+distanceRay), Quaternion.identity) as GameObject;
//				}
//				float linkSize = Vector3.Distance(gameObject.transform.position, _target.transform.position);
//				necessaryBullets = linkSize/bulletSize;

				DoShoot();
			}
		}
	}

	void DoShoot(){
		GameObject newBullet = Instantiate(v_Bullet, transform.TransformPoint(0f,0f,0f)  , transform.rotation) as GameObject;
		Rigidbody rb = newBullet.GetComponent<Rigidbody>();
		if (rb != null)	rb.AddForce(gameObject.transform.forward* _SpeedBullet);
		newBullet.GetComponent<GrappleComebackLouis>()._myShooter=gameObject;

//		float distanceBulletPerso = Vector3.Distance(gameObject.transform.position, newBullet.transform.position); //always equal to 0
//		if(distanceBulletPerso>bulletSize){
//			//is never called
//		}
		_timer = 0;
	}
}
