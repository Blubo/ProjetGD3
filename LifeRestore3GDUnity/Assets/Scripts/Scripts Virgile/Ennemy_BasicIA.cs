using UnityEngine;
using System.Collections;

public class Ennemy_BasicIA : MonoBehaviour {

    //Target, les ennemis vont se déplacer vers cet cible
    public GameObject _Target;
    //La vitesse à laquelle ils se déplacent
    [SerializeField]
    private float _Speed;

    private Ennemy_Status _Status;

	void Awake () {
        _Status = GetComponent<Ennemy_Status>();
		_Target = GameObject.Find("Idole");
	}
	
	void Update () {
        //déplacement vers la cible
        float _step = _Speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _Target.transform.position, _step);
	}

	//this is old and unused
	//actual death is in BreakableObjectByCube
    void OnCollisionEnter(Collision _coll) {
        if (_coll.gameObject.tag == "Weapon") {
            _Status.PV -= 1;
        }
        //Atk sur le joueur
        if (_coll.gameObject.tag == "Player")
        {
            _coll.gameObject.GetComponent<Player_Status>()._Life -= _Status.ATK;
        }
        //Atk sur l'idole
        if (_coll.gameObject.tag == "Idole")
        {
            _coll.gameObject.GetComponent<Idole_Status>()._Life -=(int) _Status.ATK;
        }
    }
}
