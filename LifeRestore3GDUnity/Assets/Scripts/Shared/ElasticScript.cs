using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class ElasticScript : MonoBehaviour {

    //Xinput Stuff
    bool playerIndexSet = false;
    public PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

	//plus ce nombre est grand, plus la tension est forte/ce sera dur de casser le lien
	[Range(0,1)]
	public float v_tensionRatio;

	public float v_expulsionStrenght, v_blockAttractionForce;
	//cette variable en dessous désigne à quelle pourcentage de la distnce max du lien on aura l'elasticité
	[Range(0,1)]
	public float v_tensionLessDistanceRatio;
	private float _tensionStrenght, _howDeep1;

	//gère les booléennes "break"
	//ne peut casser un lien que s'il n'est pas déjà cassé (pour empecher que ElasticBreak ne soit appellé 2 fois
	private float _timer1, temp_Return, temp_Break;
	[HideInInspector]
	public bool _breaking1, _Laisse;

	private Vector3 _direction1;

	private GameObject _hook1;

	// Use this for initialization
	void Start () {
		_breaking1=false;
		_timer1 =0f;
        //La laisse est désactivé par défaut
        _Laisse = false;
	}
	
	// Update is called once per frame
	void Update () {

        //Xinput Stuff
        prevState = state;
        state = GamePad.GetState(playerIndex);

		if(_breaking1==true){
			_timer1+=Time.deltaTime;
			if(_timer1>=gameObject.GetComponent<ShootF>().v_coolDown){
				_breaking1=false;
				_timer1=0f;
			}
		}

//		Debug.Log("_howDeep is "+_howDeep);

		//si le premier grappin existe
		if(gameObject.GetComponent<ShootF>()._myHook!=null){
			_hook1=gameObject.GetComponent<ShootF>()._myHook;
			//l'objet auquel la tete 1 est fixée
			if(_hook1.GetComponent<HookHeadF>().GrappedTo!=null){
				//la direction entre l'objet choppé et le joueur
				_direction1=(_hook1.transform.position-gameObject.transform.position);
				_direction1.Normalize();

				//on donne comme tension la force max de déplacement pour l'instant
				_tensionStrenght = gameObject.GetComponent<MovementScript5Janv>().v_movementSpeed*v_tensionRatio;

                //Si l'on appuie sur la laisse pour la bloquer/débloquer et que l'on est pas dans la zone de tension
                if (prevState.Buttons.X == ButtonState.Released && state.Buttons.X == ButtonState.Pressed && Vector3.Distance(gameObject.transform.position, _hook1.transform.position) <= _hook1.GetComponent<HookHeadF>().v_returnDistance * v_tensionLessDistanceRatio)
                {
                    Debug.Log(Vector3.Distance(gameObject.transform.position, _hook1.transform.position));
                  
                    if (!_Laisse) { 
                        //On prend en sauvegarde les valeurs de base de break et distance
                        temp_Break = _hook1.GetComponent<HookHeadF>().v_BreakDistance;
                        temp_Return = _hook1.GetComponent<HookHeadF>().v_returnDistance;
                        //On les remplace par le positionnement !!actuel!! du joueur
                        _hook1.GetComponent<HookHeadF>().v_returnDistance = Vector3.Distance(gameObject.transform.position, _hook1.transform.position) / v_tensionLessDistanceRatio;
                        _hook1.GetComponent<HookHeadF>().v_BreakDistance = Vector3.Distance(gameObject.transform.position, _hook1.transform.position) / v_tensionLessDistanceRatio;

                        _Laisse = true;
                    }
                    else if (_Laisse)
                    {
                        // On relache, on rend ses valeurs de base à break et distance
                        _hook1.GetComponent<HookHeadF>().v_BreakDistance = temp_Break;
                        _hook1.GetComponent<HookHeadF>().v_returnDistance = temp_Return;
                        _Laisse = false;
                        // !! A NOTER !! il faut aussi remettre les valeurs de bases de break et distance au moment du cassage du lien
                    }
                }

				//on cherche à quel point le gars est dans le kk
				//à savoir le ratio entre son écartement à sa cible divisé par la distance max autorisée
				_howDeep1=Vector3.Distance(gameObject.transform.position, _hook1.transform.position)/_hook1.GetComponent<HookHeadF>().v_BreakDistance;

				//si le joueur est au dela de la zone sans tension
                //if(Vector3.Distance(gameObject.transform.position, _hook1.GetComponent<HookHeadF>().GrappedTo.transform.position)>=v_tensionlessDistance){
				if(Vector3.Distance(gameObject.transform.position, _hook1.transform.position)>=_hook1.GetComponent<HookHeadF>().v_returnDistance*v_tensionLessDistanceRatio){
					//on ajoute la tension sur le joueur
					gameObject.GetComponent<Rigidbody>().AddForce(_direction1*_tensionStrenght*_howDeep1);

					//on ajoute la tension sur l'objet tracté
					//ICI
					//v_blockAttractionForce = une constante pour mieux gérer la traction via elasticité
					//rajouter un coefficient qui grandit avec le nombre de liens recus!
					//*gameObject.GetComponent<LinkStrenght>()._LinkCommited ceci est ce coefficient, il doit peut etre etre modulé pour un niveau de granularité plus fin

					_hook1.GetComponent<HookHeadF>().GrappedTo.GetComponent<Rigidbody>().AddForceAtPosition(-_direction1*_tensionStrenght*_howDeep1*v_blockAttractionForce*(gameObject.GetComponent<LinkStrenght>()._LinkCommited+1), _hook1.gameObject.transform.position);

					//si le joueur et le bloc sont trop loin
					if(Vector3.Distance(gameObject.transform.position, _hook1.transform.position)>=_hook1.GetComponent<HookHeadF>().v_BreakDistance){
						if(_breaking1==false){
							ElasticBreak(_direction1, _breaking1);
						}
					}
				}
			}
		}
	}

	public void ElasticBreak(Vector3 direction, bool breaking){
		breaking=true;
        _Laisse = false;
		GetComponent<Rigidbody>().AddForce(-direction*v_expulsionStrenght);
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
//	}
}
