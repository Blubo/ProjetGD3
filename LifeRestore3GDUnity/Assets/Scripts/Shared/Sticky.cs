using UnityEngine;
using System.Collections;

public class Sticky : MonoBehaviour {

	//soit le mettre dans l'inspecteur de chaque objet auquel on souhaite pouvoir se connecter
	//soit le rajouter procéduralement depuis une collision avec HookHead (ou dans shoot), à chaque collision avec chaque item de tel tag
	//(et si le script existe déjà, alors juste v_number+1)
	[HideInInspector]
	public int v_numberOfLinks;
	private float _myInitMass;
  [SerializeField]
  private float _MaxTimerMass;

  private float _TimerMass;

	// Use this for initialization
	void Start () {
    _TimerMass = _MaxTimerMass;
		v_numberOfLinks=0;
    if (gameObject.GetComponent<Rigidbody>() != null)
    {
      _myInitMass = gameObject.GetComponent<Rigidbody>().mass;
    }
	}
	
	// Update is called once per frame
	void Update () {
    /*
    if(v_numberOfLinks ==0){
      if (_TimerMass <= 0.0f)
      {
        gameObject.GetComponent<Rigidbody>().mass = _myInitMass / 3.0f;
      }else if (_TimerMass > 0.0f){
        _TimerMass -= Time.deltaTime;
      }

    }
    else if (v_numberOfLinks >= 1.0f)
    {
       gameObject.GetComponent<Rigidbody>().mass = _myInitMass;
       _TimerMass = _MaxTimerMass;
    }*/
	}

  
  void OnTriggerEnter(Collider col){
    if (v_numberOfLinks != 0){
	  	if(col.gameObject.GetComponent<Sticky>()!=null){
				col.gameObject.GetComponent<Rigidbody>().mass =1f;
			}
		}
	}

  void OnTriggerExit(Collider col) {
    if (v_numberOfLinks != 0){
      if (col.gameObject.GetComponent<Sticky>() != null){
        col.gameObject.GetComponent<Rigidbody>().mass = 20f;
      }
    }
  }
   

//	void OnCollisionEnter(Collision col){
//		if(col.gameObject.GetComponent<Sticky>()!=null){
//			if(col.gameObject.GetComponent<Sticky>().v_numberOfLinks!=0 ){
//				Debug.Log("hit by a linked object");
//				gameObject.GetComponent<Rigidbody>().mass =0.1f;
//			}
//		}
//	}
//
//	void OnCollisionExit(Collision col){
//		if(col.gameObject.GetComponent<Sticky>()!=null){
//			if(col.gameObject.GetComponent<Sticky>().v_numberOfLinks!=0 ){
//				Debug.Log("hit by a linked object");
//				gameObject.GetComponent<Rigidbody>().mass = _myInitMass;
//			}
//		}
//	}
}
