using UnityEngine;
using System.Collections;

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
            gameObject.AddComponent<Rigidbody>();
            gameObject.layer = LayerMask.NameToLayer("CanBreak");

        }
    }
}
