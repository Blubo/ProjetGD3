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
	private float _tensionStrenght, _howDeep1, _distanceAtTime;

	//gère les booléennes "break"
	//ne peut casser un lien que s'il n'est pas déjà cassé (pour empecher que ElasticBreak ne soit appellé 2 fois
	private float _timer1, temp_Return, temp_Break, temp_TensionRatio;
	[HideInInspector]
	public bool _breaking1, _Laisse, _Block;

	private Vector3 _direction1, _Blockvit;
	private GameObject _hook1;
	
	[Tooltip("Check to make elasticity higher if player is farther away from the hookhead")]
	[SerializeField]
	private bool v_distanceAmplificator;

	[Tooltip("Check to disable traction on player")]
	[SerializeField]
	private bool v_applyTensionOnPlayer = true;

	[Space(10)]
	[Header("New attempt at traction")]

	[Tooltip("Check to enable and test a traction force not dependent on movement speed")]
	[SerializeField]
	private bool _newAttemptAtTractionForce;
	[SerializeField]
	private float v_newTractionForce;

	// Use this for initialization
	void Start () {
		_breaking1=false;
		_timer1 =0f;
        //La laisse est désactivé par défaut
        _Laisse = false;
        _Block = false;
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
				if(_newAttemptAtTractionForce == false){
					_tensionStrenght = gameObject.GetComponent<MovementScript5Janv>().v_movementSpeed*v_tensionRatio;
				}else{
					_tensionStrenght = v_newTractionForce;
				}

				//LAISSE
                //Si l'on appuie sur la laisse pour la bloquer/débloquer et que l'on est pas dans la zone de tension
				/*if (prevState.Buttons.X == ButtonState.Released && state.Buttons.X == ButtonState.Pressed && Vector3.Distance(gameObject.transform.position, _hook1.transform.position) <= _hook1.GetComponent<HookHeadF>().v_BreakDistance * v_tensionLessDistanceRatio)
                {
                    Debug.Log(Vector3.Distance(gameObject.transform.position, _hook1.transform.position));
                  
                    if (!_Laisse) { 
                        //On prend en sauvegarde la valeur de base de break
                        temp_Break = _hook1.GetComponent<HookHeadF>().v_BreakDistance;
                        temp_TensionRatio = v_tensionRatio;
                        //On la remplace par le positionnement !!actuel!! du joueur
  //                      _hook1.GetComponent<HookHeadF>().v_BreakDistance = Vector3.Distance(gameObject.transform.position, _hook1.transform.position)/v_tensionLessDistanceRatio;
                        //_hook1.GetComponent<HookHeadF>().v_BreakDistance = Vector3.Distance(gameObject.transform.position, _hook1.transform.position) + (gameObject.GetComponent<Collider>().bounds.extents.magnitude+ _hook1.GetComponent<HookHeadF>().GrappedTo.GetComponent<Collider>().bounds.size.magnitude)*3.0f;
                        _hook1.GetComponent<HookHeadF>().v_BreakDistance = Vector3.Distance(gameObject.transform.position, _hook1.transform.position) * 3.0f;

                        float ratio = _hook1.GetComponent<HookHeadF>().v_BreakDistance / temp_Break;
                        v_tensionRatio = v_tensionRatio / ratio;
                        _Laisse = true;
                    }
                    else if (_Laisse)
                    {
                        // On relache, on rend sa valeur de base à break
                        _hook1.GetComponent<HookHeadF>().v_BreakDistance = temp_Break;
                        v_tensionRatio = temp_TensionRatio;
                        _Laisse = false;
                        // !! A NOTER !! il faut aussi remettre la valeur de base de break au moment du cassage du lien
                    }
                }*/
        if(state.Buttons.X == ButtonState.Pressed){
          //On empêche les forces élastiques d'agir
          _hook1.GetComponent<HookHeadF>().newTensionLessDistance = 10.0f;
        }
        if (state.Buttons.X == ButtonState.Released && prevState.Buttons.X == ButtonState.Pressed)
        {
          //On met à jour la taille du lien
          _hook1.GetComponent<HookHeadF>().newTensionLessDistance = _hook1.GetComponent<HookHeadF>()._actualDistance;
         // _hook1.GetComponent<HookHeadF>().newBreakDistance = _hook1.GetComponent<HookHeadF>().newTensionLessDistance * 100 / _hook1.GetComponent<HookHeadF>().breakDistanceRatio;

        }

				//on cherche à quel point le gars est dans le kk
				//à savoir le ratio entre son écartement à sa cible divisé par la distance max autorisée
				if(_hook1.GetComponent<HookHeadF>().newLinkSystem == false){
					_howDeep1=Vector3.Distance(gameObject.transform.position, _hook1.transform.position)/_hook1.GetComponent<HookHeadF>().v_BreakDistance;
				}else{
          _howDeep1 = (Vector3.Distance(gameObject.transform.position, _hook1.transform.position) / _hook1.GetComponent<HookHeadF>().newBreakDistance);
				}
				//si le joueur est au dela de la zone sans tension
                //if(Vector3.Distance(gameObject.transform.position, _hook1.GetComponent<HookHeadF>().GrappedTo.transform.position)>=v_tensionlessDistance){
				if(_hook1.GetComponent<HookHeadF>().newLinkSystem == false){

					if (Vector3.Distance(gameObject.transform.position, _hook1.transform.position) >= 2f){
						if(Vector3.Distance(gameObject.transform.position, _hook1.transform.position)>=_hook1.GetComponent<HookHeadF>().v_BreakDistance*v_tensionLessDistanceRatio){
							//si amplificator= true (public)
							//make elasticity higher if player is farther away from the hookhead
							//sinon, indépendant de cela
							if(v_distanceAmplificator==true){
								//on ajoute la tension sur le joueur
								if(v_applyTensionOnPlayer==true){
									gameObject.GetComponent<Rigidbody>().AddForce(_direction1*_tensionStrenght*_howDeep1);
								}
								//on ajoute la tension sur l'objet tracté
								//ICI
								//v_blockAttractionForce = une constante pour mieux gérer la traction via elasticité
								//rajouter un coefficient qui grandit avec le nombre de liens recus!
								//*gameObject.GetComponent<LinkStrenght>()._LinkCommited ceci est ce coefficient, il doit peut etre etre modulé pour un niveau de granularité plus fin
								_hook1.GetComponent<HookHeadF>().GrappedTo.GetComponent<Rigidbody>().AddForceAtPosition(-_direction1*_tensionStrenght*_howDeep1*v_blockAttractionForce*(gameObject.GetComponent<LinkStrenght>()._LinkCommited+1), _hook1.gameObject.transform.position);
              }else{
								if(v_applyTensionOnPlayer==true){
									gameObject.GetComponent<Rigidbody>().AddForce(_direction1*_tensionStrenght);
								}
								_hook1.GetComponent<HookHeadF>().GrappedTo.GetComponent<Rigidbody>().AddForceAtPosition(-_direction1*_tensionStrenght*v_blockAttractionForce*(gameObject.GetComponent<LinkStrenght>()._LinkCommited+1), _hook1.gameObject.transform.position);
							}

							//si le joueur et le bloc sont trop loin
						/*	if(Vector3.Distance(gameObject.transform.position, _hook1.transform.position)>=_hook1.GetComponent<HookHeadF>().v_BreakDistance){
								if(_breaking1==false){
									ElasticBreak(_direction1, _breaking1);
								}
							}*/
						}
     				}
				}else{
					if(Vector3.Distance(gameObject.transform.position, _hook1.transform.position)>_hook1.GetComponent<HookHeadF>().newTensionLessDistance){ // changer +1.0f
						//si amplificator= true (public)
						//make elasticity higher if player is farther away from the hookhead
						//sinon, indépendant de cela
						if(v_distanceAmplificator==true){
							//on ajoute la tension sur le joueur
							if(v_applyTensionOnPlayer==true){
								gameObject.GetComponent<Rigidbody>().AddForce(_direction1*_tensionStrenght*(_howDeep1*5.0f));
							}
							//on ajoute la tension sur l'objet tracté
							//ICI
							//v_blockAttractionForce = une constante pour mieux gérer la traction via elasticité
							//rajouter un coefficient qui grandit avec le nombre de liens recus!
							//*gameObject.GetComponent<LinkStrenght>()._LinkCommited ceci est ce coefficient, il doit peut etre etre modulé pour un niveau de granularité plus fin
              
							//_hook1.GetComponent<HookHeadF>().GrappedTo.GetComponent<Rigidbody>().AddForceAtPosition(-_direction1*_tensionStrenght*_howDeep1*v_blockAttractionForce*(gameObject.GetComponent<LinkStrenght>()._LinkCommited+1), _hook1.gameObject.transform.position);
            }else{
							if(v_applyTensionOnPlayer==true){
								gameObject.GetComponent<Rigidbody>().AddForce(_direction1*_tensionStrenght*(_howDeep1*3.5f));
							}
              float distance = Vector3.Distance(gameObject.transform.position, _hook1.transform.position) - _hook1.GetComponent<HookHeadF>().newTensionLessDistance;
             // _hook1.GetComponent<Rigidbody>().AddForceAtPosition(-_direction1 * _tensionStrenght * (v_blockAttractionForce) * (gameObject.GetComponent<LinkStrenght>()._LinkCommited + 1), _hook1.gameObject.transform.position);
              if (_hook1.GetComponent<HookHeadF>().GrappedTo.GetComponent<Rigidbody>().velocity.magnitude >10.0f)
              {
                
                _hook1.GetComponent<HookHeadF>().GrappedTo.GetComponent<Rigidbody>().AddForceAtPosition(-_direction1 * _hook1.GetComponent<HookHeadF>().GrappedTo.GetComponent<Rigidbody>().velocity.magnitude*50, _hook1.gameObject.transform.position);
                if (_hook1.GetComponent<HookHeadF>().GrappedTo.GetComponent<Rigidbody>().velocity.magnitude > 20.0f)
                {
                  _hook1.GetComponent<HookHeadF>().GrappedTo.GetComponent<Rigidbody>().AddForce (-_hook1.GetComponent<HookHeadF>().GrappedTo.GetComponent<Rigidbody>().velocity*2.0f);
                }
               /*  if (_hook1.GetComponent<HookHeadF>().GrappedTo.GetComponent<Rigidbody>().velocity.magnitude > 40.0f)
                {
                   if(_Block == false){
                     _Blockvit = _hook1.GetComponent<HookHeadF>().GrappedTo.GetComponent<Rigidbody>().velocity;
                     _Block = true;
                   }
                   _hook1.GetComponent<HookHeadF>().GrappedTo.GetComponent<Rigidbody>().velocity = _Blockvit;
                }
                 else
                 {
                   _Block = false;
                 }*/
              }
              else
              {
                //_hook1.GetComponent<HookHeadF>().GrappedTo.GetComponent<Rigidbody>().AddForce(-_direction1 * _tensionStrenght * (v_blockAttractionForce*_howDeep1) * (gameObject.GetComponent<LinkStrenght>()._LinkCommited + 1));
               
                _hook1.GetComponent<HookHeadF>().GrappedTo.GetComponent<Rigidbody>().AddForceAtPosition(-_direction1 * _tensionStrenght * (v_blockAttractionForce) * (gameObject.GetComponent<LinkStrenght>()._LinkCommited + 1), _hook1.gameObject.transform.position);
              }
            }
						
						//si le joueur et le bloc sont trop loin
					/*	if(Vector3.Distance(gameObject.transform.position, _hook1.transform.position)>=_hook1.GetComponent<HookHeadF>().newBreakDistance){
							if(_breaking1==false){
								ElasticBreak(_direction1, _breaking1);
							}
						}*/
					}
				}

//
//				if(Vector3.Distance(gameObject.transform.position, _hook1.GetComponent<HookHeadF>().GrappedTo.transform.position)>5f){
//					Vector3 directionPlayerBloc = _hook1.GetComponent<HookHeadF>().GrappedTo.transform.position - gameObject.transform.position;
////					Vector3.Lerp(_hook1.GetComponent<HookHeadF>().GrappedTo.transform.position, gameObject.transform.position + directionPlayerBloc * 0.5f, 1f);
////					_hook1.GetComponent<HookHeadF>().GrappedTo.transform.position = Vector3.Lerp(_hook1.GetComponent<HookHeadF>().GrappedTo.transform.position, gameObject.transform.position + directionPlayerBloc * 0.7f, 0.8f);
//					_hook1.GetComponent<HookHeadF>().GrappedTo.transform.position = Vector3.MoveTowards(_hook1.GetComponent<HookHeadF>().GrappedTo.transform.position, gameObject.transform.position + directionPlayerBloc * 0.7f, 0.5f*Time.deltaTime);
//					Debug.DrawRay(gameObject.transform.position + directionPlayerBloc * 0.7f, Vector3.up);
//					//				_hook1.GetComponent<HookHeadF>().GrappedTo.transform.position = gameObject.transform.position + directionPlayerBloc * 0.8f;
//				}
			}
		}
	}

	public void ElasticBreak(Vector3 direction, bool breaking){
    if (_Laisse == true) {
      v_tensionRatio = temp_TensionRatio;
    }

		breaking=true;
        _Laisse = false;
		_distanceAtTime = 0f;
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
