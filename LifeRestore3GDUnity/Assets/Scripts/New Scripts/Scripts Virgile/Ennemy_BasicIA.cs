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
	}
	
	void Update () {
        //déplacement vers la cible
        float _step = _Speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _Target.transform.position, _step);
	}

    void OnCollisionEnter(Collision _coll) {
        if (_coll.gameObject.tag == "Weapon") {
            _Status.PV -= 1;
        }
    }
}
