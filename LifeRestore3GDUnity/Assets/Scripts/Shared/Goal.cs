using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Goal : MonoBehaviour {

    private Color _BaseColor = Color.green;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider _coll) {
        StartCoroutine("ChangeColor");
    }

    IEnumerator ChangeColor() {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.3f);

        gameObject.GetComponent<Renderer>().material.color = _BaseColor;
    }
}
