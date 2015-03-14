using UnityEngine;
using System.Collections;

public class Boss_BasicIA : MonoBehaviour {

    //Target, les ennemis vont se déplacer vers cet cible
    public GameObject _Target;
    //La vitesse à laquelle ils se déplacent
    [SerializeField]
    private float _Speed, _dashSpeed;

	[SerializeField]
	private float _cooldownBetweenAttacks, _attackDuration;
	private float _timer,_attackTimer;
    private Ennemy_Status _Status;

	private bool attack = false;

	void Awake () {
		_timer=_cooldownBetweenAttacks;
        _Status = GetComponent<Ennemy_Status>();
	}
	
	void Update () {
//		Debug.Log(attack);
		gameObject.transform.LookAt(new Vector3(_Target.transform.position.x, gameObject.transform.position.y, _Target.transform.position.z));
		if(_timer<=_cooldownBetweenAttacks){
			_timer+=Time.deltaTime;
		}else{
			attack =true;
		}

		if(attack==false){
	        //déplacement vers la cible
	        float _step = _Speed * Time.deltaTime;
	        transform.position = Vector3.MoveTowards(transform.position, _Target.transform.position, _step);
		}else{
			Attack();
		}
	}

	void Attack(){
		gameObject.GetComponent<Rigidbody>().AddForce ((_Target.transform.position-gameObject.transform.position).normalized * _dashSpeed, ForceMode.Impulse);
		
		_attackTimer += 1.0f * Time.deltaTime;
		if(_attackTimer>= _attackDuration){
			gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
			_timer=0;
			attack = false;
			_attackTimer = 0.0f;
		}
	}

	//this is old and unused
	//actual death is in BreakableObjectByCube
	void OnCollisionEnter(Collision _coll) {
		if (_coll.gameObject.tag == "Weapon") {
			_Status.PV -= 1;
		}
	}
}
