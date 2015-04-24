using UnityEngine;
using System.Collections;

public class Ennemy_SecondIA : MonoBehaviour {

    //Target, les ennemis vont se déplacer vers cet cible
    public GameObject _Target;
    //La vitesse à laquelle ils se déplacent
    [SerializeField]
    private float _Speed;

    private Ennemy_Status _Status;

	void Awake () {
        _Status = GetComponent<Ennemy_Status>();
	}

    void OnCollisionEnter(Collision _coll) {
        if (_coll.gameObject.tag == "Weapon") {
            _Status.PV -= 1;
        }
    }
}
