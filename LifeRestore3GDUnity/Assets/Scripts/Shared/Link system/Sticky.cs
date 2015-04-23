using UnityEngine;
using System.Collections;

public class Sticky : MonoBehaviour {

	//soit le mettre dans l'inspecteur de chaque objet auquel on souhaite pouvoir se connecter
	//soit le rajouter procéduralement depuis une collision avec HookHead (ou dans shoot), à chaque collision avec chaque item de tel tag
	//(et si le script existe déjà, alors juste v_number+1)
	[HideInInspector]
	public int v_numberOfLinks;
	private float _myInitMass, _Velocity;
//	[SerializeField]
//	private float _MaxTimerMass;
//	private float _TimerMass;

	public bool destructible, fronde;

	private ObjectStats myStats;

	// Use this for initialization
	void Start () {
		if(gameObject.GetComponent<ObjectStats>()!= null) myStats = gameObject.GetComponent<ObjectStats>();

		v_numberOfLinks=0;
	    if (gameObject.GetComponent<Rigidbody>() != null)
	    {
       		_myInitMass = gameObject.GetComponent<Rigidbody>().mass;
	    }
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	//si je suis lié
	//tous les éléments possèdant sticky qui se trouvent dans mon trigger
	//voient leur poids changer (2nd test: divisé par 10)
	//selon leur type?
	void OnTriggerEnter(Collider col){
		//si lien!=0
		//tester si le block a une force
    if (v_numberOfLinks != 0){
	  	if(col.gameObject.GetComponent<Sticky>()!=null){
				col.gameObject.GetComponent<Rigidbody>().mass = col.gameObject.GetComponent<Sticky>()._myInitMass*0.1f;
			}
		}
	}
	
	void OnTriggerExit(Collider col) {
		if (v_numberOfLinks != 0){
			if (col.gameObject.GetComponent<Sticky>() != null){
				col.gameObject.GetComponent<Rigidbody>().mass = col.gameObject.GetComponent<Sticky>()._myInitMass;
		    }
		}
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.GetComponent<Sticky>()!=null){
			Sticky stickyCollided = col.gameObject.GetComponent<Sticky>();

			//si l'objet collidé est lié par qqch ou si je le suis?
			if(stickyCollided.v_numberOfLinks!=0 || v_numberOfLinks!=0){
				//si l'autre que je collide est une fronde alors
				if(stickyCollided.fronde == true){
					//rajouter une condition de vitesse sur la fronde?
					//différencier la valeur de ce takeDamage, en fonction de quoi?
					if(myStats!=null){
						if(destructible == true){
							myStats.TakeDamage(1f);
						}
					}
				}
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
