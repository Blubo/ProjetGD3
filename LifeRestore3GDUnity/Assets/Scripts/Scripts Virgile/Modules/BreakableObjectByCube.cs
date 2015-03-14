using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BreakableObjectByCube : MonoBehaviour {

    [SerializeField]
    private int _WillBreakIn;

    [SerializeField]
    private bool _DestroyOnlyBySpecial;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    if (_WillBreakIn<=0 ){
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter(Collision _collision)
    {
        if(_collision.gameObject.layer == LayerMask.NameToLayer("CanBreak")){
            if(_DestroyOnlyBySpecial){
               if (_collision.gameObject.tag == "Special"){
                   _WillBreakIn -= 1;
                   Destroy(_collision.gameObject);
               }
            }
            else if (!_DestroyOnlyBySpecial) { _WillBreakIn -= 1; }
        }
    }
}
