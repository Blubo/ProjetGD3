using UnityEngine;
using System.Collections;

public class Player_Status : MonoBehaviour {

    [SerializeField]
    private float _Life;

	void Start () {
	
	}
	

	void Update () {
        //Si la vie tombe à zero 
	    if(_Life<= 0.0f){
            Deactivate();
        }
	}

    public void InAura() {
        _Life += 1.0f * Time.deltaTime;
    }

    void Deactivate() {
        //L'avatar ne peut pas bouger mais peut quand même tirer des liens (?)
        //Décompte jusqu'à la mort 
        //Mort direct si le joueur appuie sur une touche
    }
}
