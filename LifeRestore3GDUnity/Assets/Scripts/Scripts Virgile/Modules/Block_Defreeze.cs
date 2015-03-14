using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Block_Defreeze : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void InAura()
    {
        if (GetComponent<Rigidbody>() == null)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
            gameObject.AddComponent<Rigidbody>();
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.mass = 10.0f;

            gameObject.layer = LayerMask.NameToLayer("CanBreak");

        }
    }
}
