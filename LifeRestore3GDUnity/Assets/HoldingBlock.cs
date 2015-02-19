using UnityEngine;
using System.Collections;

public class HoldingBlock : MonoBehaviour {

    private Sticky _blocs;
    [SerializeField]
    private float _Timer;

	// Use this for initialization
	void Start () {
        _blocs = GetComponent<Sticky>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(_blocs.v_numberOfLinks >= 4){
            _Timer -= 1.0f * Time.deltaTime;
        }

        if(_Timer <= 0 ){
            Debug.Log("Stuff happens!");
            gameObject.renderer.material.color = Color.green;
        }
	}
}
