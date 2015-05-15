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

	private float _howDeep1;
	
	private Vector3 _direction1;
	private GameObject _hook1;

	[Space(10)]
	[Header("New traction")]
	
	[SerializeField]
	private float v_newTractionForce;

	private Rigidbody myRB, grappedToRB;
	private GameObject grappedTo;
	private HookHeadF myHookHeadF;

	[Tooltip("How much force when max link size")]
	[Range(1,5)]
	[SerializeField]
	private float constrictor;

	public bool altLink;

	// Use this for initialization
	void Start () {
		if(gameObject.GetComponent<Rigidbody>() != null){
			myRB = gameObject.GetComponent<Rigidbody>();
		}
	}
	
	// Update is called once per frame
	void Update () {

        //Xinput Stuff
        prevState = state;
        state = GamePad.GetState(playerIndex);


		//si le premier grappin existe
		if(gameObject.GetComponent<ShootF>()._myHook!=null){
			_hook1=gameObject.GetComponent<ShootF>()._myHook;
			myHookHeadF = _hook1.GetComponent<HookHeadF>();
			//l'objet auquel la tete 1 est fixée
			if(_hook1.GetComponent<HookHeadF>().GrappedTo!=null){
				grappedTo = _hook1.GetComponent<HookHeadF>().GrappedTo;
				grappedToRB = grappedTo.GetComponent<Rigidbody>();

				//la direction entre l'objet choppé et le joueur
				_direction1=(_hook1.transform.position-gameObject.transform.position);
				_direction1.Normalize();

//				if(state.Buttons.X == ButtonState.Pressed){
//					//On empêche les forces élastiques d'agir
//					myHookHeadF.newTensionLessDistance = 10.0f;
//				}
//
//				if (state.Buttons.X == ButtonState.Released && prevState.Buttons.X == ButtonState.Pressed){
//					//On met à jour la taille du lien
//					myHookHeadF.newTensionLessDistance = myHookHeadF._actualDistance;
//				}

				//on cherche à quel point le gars est dans le kk
				//à savoir le ratio entre son écartement à sa cible divisé par la distance max autorisée
				_howDeep1 = (Vector3.Distance(gameObject.transform.position, _hook1.transform.position) / myHookHeadF.newBreakDistance);

				//si le joueur est au dela de la zone sans tension
				if(Vector3.Distance(gameObject.transform.position, _hook1.transform.position)>myHookHeadF.newTensionLessDistance){
//					myRB.AddForce(_direction1*v_newTractionForce*_howDeep1);
					myRB.AddForce(_direction1*v_newTractionForce);

					if(grappedTo.name.Equals("Bloc_3_G")==false || grappedTo.GetComponent<Sticky>().v_numberOfLinks>=2){
						if (grappedToRB.velocity.magnitude >10.0f){
							grappedToRB.AddForceAtPosition(-_direction1 * grappedToRB.velocity.magnitude*50, _hook1.gameObject.transform.position);
							if (grappedToRB.velocity.magnitude > 20.0f){
								grappedToRB.AddForce (-grappedToRB.velocity*2.0f);
							}
						}else{
							grappedToRB.AddForceAtPosition(-_direction1 * v_newTractionForce * (v_blockAttractionForce) * (gameObject.GetComponent<LinkStrenght>()._LinkCommited + 1), _hook1.gameObject.transform.position);
						}
					}
				}

				if(Vector3.Distance(gameObject.transform.position, grappedTo.transform.position)>=(myHookHeadF.newBreakDistance*0.9f)){
//					Debug.Log("now");
					//enregistrer la position du joueur à chaque frame, et remplacer dans ce if sa position par celle de la frame précédente (ce que j'avais tenté de faire avec les blocs, mais sur le joueur, quoi)
					//rajouter *2
//					myRB.AddForce(_direction1*v_newTractionForce*_howDeep1*constrictor);
					if(altLink == false) myRB.AddForce(_direction1*v_newTractionForce*constrictor);
					if(altLink == true)	myRB.AddForce(_direction1*v_newTractionForce);
					if(grappedTo.name.Equals("Bloc_3_G")==false || grappedTo.GetComponent<Sticky>().v_numberOfLinks>=2){
						//rajouter *2
						grappedToRB.AddForceAtPosition(-_direction1 * v_newTractionForce * (v_blockAttractionForce) * (gameObject.GetComponent<LinkStrenght>()._LinkCommited + 1)*constrictor, _hook1.gameObject.transform.position);
				
					}
				}
			}
		}
	}
}
