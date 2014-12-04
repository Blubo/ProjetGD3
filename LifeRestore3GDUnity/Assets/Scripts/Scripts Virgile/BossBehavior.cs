using UnityEngine;
using System.Collections;

public class BossBehavior : MonoBehaviour {

	Boss_Playstate _playstate;

	Animator _animator;
	AnimatorStateInfo _CurrentLayer;
	//NavMeshAgent _Agent;

	//Deplacement
	private float _Speed;

	// Use this for initialization
	void Start () {
		_playstate = GetComponent<Boss_Playstate> ();
		_animator = GetComponent<Animator> ();
		//_Agent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		_CurrentLayer = _animator.GetCurrentAnimatorStateInfo (0);
		//Si ? movement
		if(_CurrentLayer.IsName("Movement")){
			BossMovement();
		}
	}

	public void TakeDamage(int value){
		_playstate._Boss_Pv -= value;
		StartCoroutine(Rougissement());
	}

	void BossMovement(){
		//Déplacement aléatoire
		//transform.position += new Vector3 (Random.Range (0.0f, 10.0f), Random.Range (0.0f, 0.0f), Random.Range (0.0f, 10.0f));
		// Deplacement vers un personnage
	}

	IEnumerator Rougissement(){
		renderer.material.color =new Color(1f, 0f, 0f, 1f);
		yield return new WaitForSeconds(0.2f);
		renderer.material.color  =new Color(1f, 1f, 1f, 1f);
		yield return 0;
	}


}
