using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class ElasticScript : MonoBehaviour {

    //Xinput Stuff
    bool playerIndexSet = false;
    public PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

	public float v_blockAttractionForce;

	private float _tensionStrenght, _howDeep1;
	
	private Vector3 _direction1;
	private GameObject _hook1;

	[Space(10)]
	[Header("New traction")]
	
	[SerializeField]
	private float v_newTractionForce;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        //Xinput Stuff
        prevState = state;
        state = GamePad.GetState(playerIndex);

		//si le premier grappin existe
		if(gameObject.GetComponent<ShootF>()._myHook!=null){
			_hook1=gameObject.GetComponent<ShootF>()._myHook;
			//l'objet auquel la tete 1 est fixée
			if(_hook1.GetComponent<HookHeadF>().GrappedTo!=null){
				//la direction entre l'objet choppé et le joueur
				_direction1=(_hook1.transform.position-gameObject.transform.position);
				_direction1.Normalize();

				_tensionStrenght = v_newTractionForce;

				if(state.Buttons.X == ButtonState.Pressed){
					//On empêche les forces élastiques d'agir
					_hook1.GetComponent<HookHeadF>().newTensionLessDistance = 10.0f;
				}

				if (state.Buttons.X == ButtonState.Released && prevState.Buttons.X == ButtonState.Pressed){
					//On met à jour la taille du lien
					_hook1.GetComponent<HookHeadF>().newTensionLessDistance = _hook1.GetComponent<HookHeadF>()._actualDistance;
				}

				//on cherche à quel point le gars est dans le kk
				//à savoir le ratio entre son écartement à sa cible divisé par la distance max autorisée
				_howDeep1 = (Vector3.Distance(gameObject.transform.position, _hook1.transform.position) / _hook1.GetComponent<HookHeadF>().newBreakDistance);

				//si le joueur est au dela de la zone sans tension
				if(Vector3.Distance(gameObject.transform.position, _hook1.transform.position)>_hook1.GetComponent<HookHeadF>().newTensionLessDistance){
					gameObject.GetComponent<Rigidbody>().AddForce(_direction1*_tensionStrenght*_howDeep1);

					if(_hook1.GetComponent<HookHeadF>().GrappedTo.name.Equals("Bloc_3_G")==false || _hook1.GetComponent<HookHeadF>().GrappedTo.GetComponent<Sticky>().v_numberOfLinks>=2){

						if (_hook1.GetComponent<HookHeadF>().GrappedTo.GetComponent<Rigidbody>().velocity.magnitude >10.0f){
							_hook1.GetComponent<HookHeadF>().GrappedTo.GetComponent<Rigidbody>().AddForceAtPosition(-_direction1 * _hook1.GetComponent<HookHeadF>().GrappedTo.GetComponent<Rigidbody>().velocity.magnitude*50, _hook1.gameObject.transform.position);
							if (_hook1.GetComponent<HookHeadF>().GrappedTo.GetComponent<Rigidbody>().velocity.magnitude > 20.0f){
								_hook1.GetComponent<HookHeadF>().GrappedTo.GetComponent<Rigidbody>().AddForce (-_hook1.GetComponent<HookHeadF>().GrappedTo.GetComponent<Rigidbody>().velocity*2.0f);
							}
						}else{
							_hook1.GetComponent<HookHeadF>().GrappedTo.GetComponent<Rigidbody>().AddForceAtPosition(-_direction1 * _tensionStrenght * (v_blockAttractionForce) * (gameObject.GetComponent<LinkStrenght>()._LinkCommited + 1), _hook1.gameObject.transform.position);
						}
					}
				}

				if(Vector3.Distance(gameObject.transform.position, _hook1.GetComponent<HookHeadF>().GrappedTo.transform.position)>=(_hook1.GetComponent<HookHeadF>().newBreakDistance*9/10)){
					Debug.Log("now");
					//enregistrer la position du joueur à chaque frame, et remplacer dans ce if sa position par celle de la frame précédente (ce que j'avais tenté de faire avec les blocs, mais sur le joueur, quoi)
					gameObject.GetComponent<Rigidbody>().AddForce(_direction1*_tensionStrenght*_howDeep1);

					if(_hook1.GetComponent<HookHeadF>().GrappedTo.name.Equals("Bloc_3_G")==false || _hook1.GetComponent<HookHeadF>().GrappedTo.GetComponent<Sticky>().v_numberOfLinks>=2){

						_hook1.GetComponent<HookHeadF>().GrappedTo.GetComponent<Rigidbody>().AddForceAtPosition(-_direction1 * _tensionStrenght * (v_blockAttractionForce) * (gameObject.GetComponent<LinkStrenght>()._LinkCommited + 1), _hook1.gameObject.transform.position);
				
					}
				}
			}
		}
	}
}
