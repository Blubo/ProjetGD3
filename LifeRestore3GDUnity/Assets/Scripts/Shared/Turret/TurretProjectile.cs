using UnityEngine;
using System.Collections;

public class TurretProjectile : MonoBehaviour {

	[SerializeField]
	private float _authorizedLifeTime;
	private float _lifeTime;

	[HideInInspector]
	public GameObject v_CanonWhoShotMe;
	[HideInInspector]
	public GameObject _playerWhoShotMe;
	private Rigidbody myRB;
	private Vector3 lastFrameVelocity;

	[SerializeField]
	private GameObject particuleDestruction;

	void Start(){
		if(gameObject.GetComponent<Rigidbody>()!=null) myRB=gameObject.GetComponent<Rigidbody>();
    if (gameObject.GetComponent<Rigidbody>() != null && v_CanonWhoShotMe.GetComponent<Rigidbody>() != null) Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), v_CanonWhoShotMe.GetComponent<Collider>());
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
		if(collision.gameObject != v_CanonWhoShotMe){
//			if(collision.gameObject.GetComponent<Sticky>()==null){
			if(collision.gameObject.tag.Equals("Static")==true
			   || collision.gameObject.tag.Equals("UnlinkableDestructible")
			   || collision.gameObject.tag.Equals("Canon")
			   || collision.gameObject.tag.Equals("Weapon")
			   || collision.gameObject.tag.Equals("Idole")
			   || collision.gameObject.tag.Equals("Player")
			   || collision.gameObject.name.Equals("Enemy_Boss")){
				Instantiate(particuleDestruction, gameObject.transform.position, Quaternion.identity);
				Destroy(gameObject);
			}
		}
	}
}
