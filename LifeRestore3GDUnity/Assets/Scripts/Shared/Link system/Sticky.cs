using UnityEngine;
using System.Collections;

public class Sticky : MonoBehaviour {

	//soit le mettre dans l'inspecteur de chaque objet auquel on souhaite pouvoir se connecter
	//soit le rajouter procéduralement depuis une collision avec HookHead (ou dans shoot), à chaque collision avec chaque item de tel tag
	//(et si le script existe déjà, alors juste v_number+1)
	[HideInInspector]
	public int v_numberOfLinks;
	private float _myInitMass;

	[HideInInspector]
	public float _Velocity;

	//la vitesse nécessaire pour péter un fdp
	[SerializeField]
	private float _necessaryVelocity, _maxVelocity;

//	[SerializeField]
//	private float _MaxTimerMass;
//	private float _TimerMass;

	public bool destructible, fronde;

	private ObjectStats myStats;
	private Rigidbody myRB;

	private float internalTimer;
	[HideInInspector]
	public bool wasLinkedNotLongAgo, linked = false, linkedLastFrame = false;

	// Use this for initialization
	void Start () {
		if(gameObject.GetComponent<Rigidbody>()!=null) myRB = gameObject.GetComponent<Rigidbody>();
		if(gameObject.GetComponent<ObjectStats>()!= null) myStats = gameObject.GetComponent<ObjectStats>();

		v_numberOfLinks=0;
	    if (gameObject.GetComponent<Rigidbody>() != null)
	    {
       		_myInitMass = gameObject.GetComponent<Rigidbody>().mass;
	    }
	}
	
	// Update is called once per frame
	void Update () {
		_Velocity = myRB.velocity.magnitude;

		if(_Velocity>=_maxVelocity){
			Vector3 actualVelocity = myRB.velocity;
			myRB.velocity = actualVelocity.normalized*_maxVelocity;
		
		}
		if(v_numberOfLinks != 0){
			linked =true;
		}else{
			linked = false;
		}

		if(linked == false && linkedLastFrame == true){
			internalTimer += Time.deltaTime;
			wasLinkedNotLongAgo = true;

			if(internalTimer>=1){
				wasLinkedNotLongAgo=false;
				internalTimer=0f;
			}
		}else{
			wasLinkedNotLongAgo=false;
			linkedLastFrame = linked;
		}

		if(Input.GetKeyUp(KeyCode.A)){
			Debug.Log("name is "+ gameObject.name);
		}

		if(Input.GetKeyUp(KeyCode.Space)  && gameObject.name.Equals("FrondeEx")){
			Debug.Log("velocity magnitude is " + _Velocity);
		}

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
			//that lign below won't do, need to add something like "linked in the last few seconds"
			if(stickyCollided.v_numberOfLinks!=0 || v_numberOfLinks!=0||wasLinkedNotLongAgo==true){
				if(stickyCollided._Velocity >= _necessaryVelocity || _Velocity >=_necessaryVelocity ){
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
    if (fronde)
    {
      if (col.gameObject.tag == "Ennemy" && linked && gameObject.GetComponent<Rigidbody>().velocity.magnitude > 2.0f)
      {
        Debug.Log(gameObject.GetComponent<Rigidbody>().velocity.magnitude);
        //Faiblar
        if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyA_AI)
        {
          col.gameObject.GetComponent<BasicEnnemy>().Health -= 1;
        }
        //barak
        if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyB_AI)
        {
          col.gameObject.GetComponent<BasicEnnemy>().Health -= 1;
          col.gameObject.GetComponent<Rigidbody>().AddForce(-col.transform.forward * 5000.0f);
        }
        //Coloss
        if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyC_Ai)
        {
          col.gameObject.GetComponent<BasicEnnemy>().Health -= 2;
        }
        //Ingénieur
        if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyD_AI)
        {
          col.gameObject.GetComponent<BasicEnnemy>().Health -= 1;
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
