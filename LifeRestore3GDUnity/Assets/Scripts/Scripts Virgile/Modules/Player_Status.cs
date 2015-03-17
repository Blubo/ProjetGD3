using UnityEngine;
using System.Collections;

public class Player_Status : MonoBehaviour {

    [SerializeField]
    public float _Life;
    public GameObject _Idole;
    private int _LifeInt;

    [SerializeField]
    private float _TimerDeath, _Maxlife, _OriginalMoveSpeed, _MaxTimerDeath;

    private bool _Desactivated;

	void Start () {
        _Desactivated = false;
        _OriginalMoveSpeed = gameObject.GetComponent<MovementScript5Janv>().v_movementSpeed;
	}
	

	void Update () {
        //Passage de la vie en Int
        _LifeInt = (int)_Life;

        //Si la vie tombe à zero 
	    if(_LifeInt<= 0){
            Deactivate();
        }
        else if (_LifeInt > 0 && _Desactivated ==true)
        {
            Activate();
        }


	}

    public void InAura() {
        _Life += 1.0f * Time.deltaTime;
    }

    void Deactivate() {
        //
        _Desactivated = true;
        //L'avatar ne peut pas bouger mais peut quand même tirer des liens (?)
        gameObject.GetComponent<MovementScript5Janv>().v_movementSpeed = 0.0f;
        //Pas de dash non plus
        gameObject.GetComponent<Dash>().enabled = false;
        //Décompte jusqu'à la mort 
        _TimerDeath -= 1.0f * Time.deltaTime;
        if(_TimerDeath <=0.0f){
            Respawn();
        }
        //Mort direct si le joueur appuie sur une touche
    }

    void Activate(){
        gameObject.GetComponent<MovementScript5Janv>().v_movementSpeed = _OriginalMoveSpeed;
        gameObject.GetComponent<Dash>().enabled = true;
        gameObject.GetComponentInChildren<Renderer>().enabled = true;
        _Desactivated = false;
    }

    void Respawn() { 
        //Disparition du joueur 
        gameObject.GetComponentInChildren<Renderer>().enabled = false;
        //Replacement à coté de l'idole
        gameObject.transform.position = _Idole.transform.position + new Vector3(1.0f, 0.0f, 0.0f);
        //Remise à niveau des stats du joueur
        _Life = _Maxlife;
        _TimerDeath = _MaxTimerDeath;
        //Perte de vie des joueurs en général
        //Clignotement + invincibilité ? 
    }
}
