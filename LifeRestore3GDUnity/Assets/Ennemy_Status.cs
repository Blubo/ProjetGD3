using UnityEngine;
using System.Collections;

public class Ennemy_Status : MonoBehaviour {
    //Nombre de points de vie restants
    public int PV;

    void Awake() { 
        
    }

    void Update() { 
        if(PV <= 0){
            Destroy(this);
        }
    }
}
