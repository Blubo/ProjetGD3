using UnityEngine;
using System.Collections;

public class Block_MovableObstacle : MonoBehaviour {

    [SerializeField]
    private Transform OriginalPlacement;

	void Start () {
	
	}
	
	void Update () {
	    //1*
      //  transform.position = Vector3.MoveTowards(transform.position, OriginalPlacement.position, 0.01f);
        //2*
        Rigidbody rb = GetComponent<Rigidbody>();
        float Distance = Vector2.Distance(transform.position, OriginalPlacement.position);
        if(Distance >0.5f){
            rb.AddForce( (OriginalPlacement.position-transform.position)*500.0f*Time.deltaTime);
        }else{
            rb.velocity = Vector3.zero;
        }
	}
}
