using UnityEngine;
using System.Collections;

public class Ennemy_Status : MonoBehaviour {
    //Nombre de points de vie restants
    public int PV;
    //Puissance de l'ATK si ils touchent un joueur ou l'idole
    public float ATK;

    void Awake() { 
        
    }

    void Update() { 
        if(PV <= 0){
            Destroy(gameObject);
        }
    }
}
