﻿using UnityEngine;
using System.Collections;

public class TurretShooting : MonoBehaviour {

	[Header("Standard turret system")]
	[SerializeField]
	private float _shootCooldown,_shootForce;
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

	// Use this for initialization
	void Start () {
		_shootTimer=_shootCooldown;
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
		Camera.main.GetComponent<SoundManagerHeritTest>().PlaySoundOneShot("Canon tir");

		GameObject particule = Instantiate(particuleEffect, _instantiateur.transform.position, Quaternion.identity )as GameObject;
		particule.transform.forward = gameObject.transform.right;

//		GameObject newProj = Instantiate(_projectile, _instantiateur.transform.position, Quaternion.identity) as GameObject;
		GameObject newProj = Instantiate(_projectile, _instantiateur.transform.position, Quaternion.identity) as GameObject;
		newProj.GetComponent<Rigidbody>().AddForce(_instantiateur.transform.forward*_shootForce);
		newProj.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-1000,1000), Random.Range(-1000,1000), Random.Range(-1000,1000)));

		if(newProj.GetComponent<TurretProjectile>() != null) newProj.GetComponent<TurretProjectile>().v_whoShotMe = machine;
		_shootTimer=0f;
	}
}
