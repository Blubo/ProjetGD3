using UnityEngine;
using System.Collections;

public class TurretShooting : MonoBehaviour {

	[Header("Standard turret system")]
	[SerializeField]
	private float _shootCooldown,_shootForce, _shootBombForce;
	private float _shootTimer;

	[SerializeField]
	private GameObject _projectile, _instantiateur;

	[Space(15f)]

	[Header("Canon system")]
	[Tooltip("Check to make it a canon")]
	public bool isCanon;

	[SerializeField]
	[Tooltip("The canon/tourelle is")]
	private GameObject machine;

	[SerializeField]
	private GameObject particuleEffect;

	public bool automate;
	[HideInInspector]
	public GameObject _playerWhoShot;

	// Use this for initialization
	void Start () {
		_shootTimer=_shootCooldown;
	}

  void Activated()
  {
    this.enabled = true;
  }

	// Update is called once per frame
	void Update () {
		if(isCanon==false){
			_shootTimer+=Time.deltaTime;
			if(automate==true){
				if(_shootTimer>=_shootCooldown){
					Shoot();
				}
			}
		}
	}

	void Shoot(){
    if (!isCanon)
    {
      Camera.main.GetComponent<SoundManagerHeritTest>().PlaySoundOneShot("Tourelle tir");
    }
    else
    {
      Camera.main.GetComponent<SoundManagerHeritTest>().PlaySoundOneShot("Canon tir");
    }


		GameObject particule = Instantiate(particuleEffect, _instantiateur.transform.position, Quaternion.identity )as GameObject;
		particule.transform.forward = gameObject.transform.right;

//		GameObject newProj = Instantiate(_projectile, _instantiateur.transform.position, Quaternion.identity) as GameObject;
		GameObject newProj = Instantiate(_projectile, _instantiateur.transform.position, Quaternion.identity) as GameObject;
		newProj.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-1000,1000), Random.Range(-1000,1000), Random.Range(-1000,1000)));
//		Debug.Log("player who shot "+_playerWhoShot.name);
//		if(newProj.GetComponent<TurretProjectile>() != null) newProj.GetComponent<TurretProjectile>().v_CanonWhoShotMe = machine;
//		if(newProj.GetComponent<TurretProjectile>() != null) newProj.GetComponent<TurretProjectile>()._playerWhoShotMe = _playerWhoShot;
		_shootTimer=0f;

		if(newProj.GetComponent<TurretProjectile>()== null){
			newProj.GetComponent<DontCollideBombeWCanon>().v_CanonWhoShotMe = machine;
			newProj.GetComponent<Rigidbody>().AddForce(_instantiateur.transform.forward*_shootBombForce);
			newProj.GetComponent<BombBehavior>().TakeDamage(false);

			return;
		}
		newProj.GetComponent<Rigidbody>().AddForce(_instantiateur.transform.forward*_shootForce);



		newProj.GetComponent<TurretProjectile>().v_CanonWhoShotMe = machine;
		if(isCanon == true){
			newProj.GetComponent<TurretProjectile>()._playerWhoShotMe = _playerWhoShot;
		}else{
			if(gameObject.transform.parent.transform.parent.GetComponent<Sticky>().myHolderPlayer != null){
//				Debug.Log("gnnnnééé "+gameObject.transform.parent.transform.parent.GetComponent<Sticky>().myHolderPlayer.name);

				newProj.GetComponent<TurretProjectile>()._playerWhoShotMe = gameObject.transform.parent.transform.parent.GetComponent<Sticky>().myHolderPlayer;
			}
		}
	}
}
