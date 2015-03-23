using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public int _TeamLifes;

	// Use this for initialization
	void Start () {
	   
	}
	
	// Update is called once per frame
	void Update () {
	    if(_TeamLifes <=0){
            //gameOver
        }
	}
}
