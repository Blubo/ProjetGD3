using UnityEngine;
using System.Collections;

public class ElasticScript : MonoBehaviour {

	//plus ce nombre est grand, plus la tension est forte/ce sera dur de casser le lien
	[Range(0,1)]
	public float v_tensionRatio;

	public float v_tensionlessDistance, v_expulsionStrenght;
	private float _tensionStrenght, _howDeep1, _howDeep2;

	//gère les booléennes "break"
	//ne peut casser un lien que s'il n'est pas déjà cassé (pour empecher que ElasticBreak ne soit appellé 2 fois
	private float _timer1, _timer2;
	[HideInInspector]
	public bool _breaking1, _breaking2;

	private Vector3 _direction1, _direction2;

	private GameObject _hook1, _hook2;

	// Use this for initialization
	void Start () {
		_breaking1=false;
		_breaking2=false;
		_timer1 =0f;
		_timer2 =0f;
	}
	
	// Update is called once per frame
	void Update () {

		if(_breaking1==true){
			_timer1+=Time.deltaTime;
			if(_timer1>=gameObject.GetComponent<ShootF>().v_coolDown){
				_breaking1=false;
				_timer1=0f;
			}
		}

		if(_breaking2==true){
			_timer2+=Time.deltaTime;
			if(_timer2>=gameObject.GetComponent<ShootF>().v_coolDown){
				_breaking2=false;
				_timer2=0f;
			}
		}

//		Debug.Log("_howDeep is "+_howDeep);
//		Debug.Log("_howDeep 1 is "+_howDeep1);

		//si le premier grappin existe
		if(gameObject.GetComponent<ShootF>()._myHook!=null){
			_hook1=gameObject.GetComponent<ShootF>()._myHook;
			//l'objet auquel la tete 1 est fixée
			if(_hook1.GetComponent<HookHeadF>().GrappedTo!=null){
				_direction1=(_hook1.GetComponent<HookHeadF>().GrappedTo.transform.position-gameObject.transform.position);
				_direction1.Normalize();

				//on donne comme tension la force max de déplacement pour l'instant
				_tensionStrenght = gameObject.GetComponent<MovementScript5Janv>().v_movementSpeed*v_tensionRatio;

				//on cherche à quel point le gars est dans le kk
				//à savoir le ratio entre son écartement à sa cible divisé par la distance max autorisée
				_howDeep1=Vector3.Distance(gameObject.transform.position, _hook1.GetComponent<HookHeadF>().GrappedTo.transform.position)/_hook1.GetComponent<HookHeadF>().v_BreakDistance;

				if(Vector3.Distance(gameObject.transform.position, _hook1.GetComponent<HookHeadF>().GrappedTo.transform.position)>=v_tensionlessDistance){
					gameObject.rigidbody.AddForce(_direction1*_tensionStrenght*_howDeep1);
					if(Vector3.Distance(gameObject.transform.position, _hook1.GetComponent<HookHeadF>().GrappedTo.transform.position)>=_hook1.GetComponent<HookHeadF>().v_BreakDistance){
						if(_breaking1==false){
							ElasticBreak(_direction1, _breaking1);
						}
					}
				}
			}
		}

		if(gameObject.GetComponent<ShootF>()._myHook1!=null){
			_hook2=gameObject.GetComponent<ShootF>()._myHook1;
			//l'objet auquel la tete 1 est fixée
			if(gameObject.GetComponent<ShootF>()._myHook1.GetComponent<HookHeadF>().GrappedTo!=null){
				_direction2=(gameObject.GetComponent<ShootF>()._myHook1.GetComponent<HookHeadF>().GrappedTo.transform.position-gameObject.transform.position);
				_direction2.Normalize();
				
				//on donne comme tension la force max de déplacement pour l'instant
				_tensionStrenght = gameObject.GetComponent<MovementScript5Janv>().v_movementSpeed*v_tensionRatio;
				
				//on cherche à quel point le gars est dans le kk
				//à savoir le ratio entre son écartement à sa cible divisé par la distance max autorisée
				_howDeep2=Vector3.Distance(gameObject.transform.position, gameObject.GetComponent<ShootF>()._myHook1.GetComponent<HookHeadF>().GrappedTo.transform.position)/gameObject.GetComponent<ShootF>()._myHook1.GetComponent<HookHeadF>().v_BreakDistance;

				if(Vector3.Distance(gameObject.transform.position, gameObject.GetComponent<ShootF>()._myHook1.GetComponent<HookHeadF>().GrappedTo.transform.position)>=v_tensionlessDistance){
					gameObject.rigidbody.AddForce(_direction2*_tensionStrenght*_howDeep2);
					if(Vector3.Distance(gameObject.transform.position, _hook2.GetComponent<HookHeadF>().GrappedTo.transform.position)>=_hook2.GetComponent<HookHeadF>().v_BreakDistance){
						if(_breaking1==false){
							ElasticBreak(_direction2, _breaking1);
						}
					}
				}
			}
		}
	}

	public void ElasticBreak(Vector3 direction, bool breaking){
		breaking=true;
		rigidbody.AddForce(-direction*v_expulsionStrenght);
	}

//	void OnDrawGizmosSelected() {
//		Gizmos.color = new Color32(107,142,35,200);
//		if(gameObject.GetComponent<ShootF>()._myHook!=null){
//			//l'objet auquel la tete 1 est fixée
//			if(gameObject.GetComponent<ShootF>()._myHook.GetComponent<HookHeadF>().GrappedTo!=null){
//				Gizmos.DrawSphere(gameObject.GetComponent<ShootF>()._myHook.GetComponent<HookHeadF>().GrappedTo.transform.position, v_tensionlessDistance);
//				Gizmos.DrawSphere(gameObject.GetComponent<ShootF>()._myHook.GetComponent<HookHeadF>().GrappedTo.transform.position, gameObject.GetComponent<ShootF>()._myHook.GetComponent<HookHeadF>().v_BreakDistance);
//			}
//		}
//
//		if(gameObject.GetComponent<ShootF>()._myHook1!=null){
//			//l'objet auquel la tete 2 est fixée
//			if(gameObject.GetComponent<ShootF>()._myHook1.GetComponent<HookHeadF>().GrappedTo!=null){
//				Gizmos.DrawSphere(gameObject.GetComponent<ShootF>()._myHook1.GetComponent<HookHeadF>().GrappedTo.transform.position, v_tensionlessDistance);
//				Gizmos.DrawSphere(gameObject.GetComponent<ShootF>()._myHook1.GetComponent<HookHeadF>().GrappedTo.transform.position, gameObject.GetComponent<ShootF>()._myHook1.GetComponent<HookHeadF>().v_BreakDistance);
//			}
//		}
//	}
}
