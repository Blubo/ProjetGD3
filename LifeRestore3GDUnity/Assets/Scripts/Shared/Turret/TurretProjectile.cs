using UnityEngine;
using System.Collections;

public class TurretProjectile : MonoBehaviour {

	[SerializeField]
	private float _authorizedLifeTime;
	private float _lifeTime;

	[HideInInspector]
	public GameObject v_whoShotMe;

	private Rigidbody myRB;
	private Vector3 lastFrameVelocity;

	[SerializeField]
	private GameObject particuleDestruction;

	void Start(){
		if(gameObject.GetComponent<Rigidbody>()!=null) myRB=gameObject.GetComponent<Rigidbody>();
		Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), v_whoShotMe.GetComponent<Collider>());
	}

	// Update is called once per frame
	void Update () {
		_lifeTime+=Time.deltaTime;
		if(_lifeTime>=_authorizedLifeTime){
			Destroy(gameObject);
		}
		if(lastFrameVelocity!=Vector3.zero) myRB.velocity = lastFrameVelocity;
		lastFrameVelocity = myRB.velocity;

	}

	void OnCollisionEnter(Collision collision){
		if(collision.gameObject != v_whoShotMe){
//			if(collision.gameObject.GetComponent<Sticky>()==null){
			if(collision.gameObject.tag.Equals("Static")==true){
				Instantiate(particuleDestruction, gameObject.transform.position, Quaternion.identity);
				Destroy(gameObject);
			}
		}
	}
}
