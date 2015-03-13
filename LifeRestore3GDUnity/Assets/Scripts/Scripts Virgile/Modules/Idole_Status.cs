using UnityEngine;
using System.Collections;

public class Idole_Status : MonoBehaviour {

    [SerializeField]
    private int _Life;

    private Sticky _LinkOnit;

	void Start () {
	    _LinkOnit = GetComponent<Sticky>();
	}
	
	void Update () {
        if (_Life <= 0){
            Death();
        }
	}

    void OnTriggerStay(Collider _other) {

        if(_LinkOnit.v_numberOfLinks >= 1){
            //Si l'objet dans la zone de l'idole peut interagir avec, alors elle le fait
            if (_other.gameObject.layer == LayerMask.NameToLayer("Usable"))
            {
                _other.SendMessage("InAura");
            }
        }
    }

    void Death() { 
        //si les points de vies de l'aura tombent à zero on lance le game Over
    }
}
