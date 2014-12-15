using UnityEngine;
using System.Collections;
using XInputDotNetPure;

//l'idée: je tire une pilule sur qqch. si c'est un autre joueur, ca crée lien

public class NodeLifeRestore : MonoBehaviour {
	
	bool playerIndexSet = false;
	public PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;

	private PlayerState _myPlayerState;

	//le GO servant à créer le lien pr l'instant
	public GameObject v_Bullet;

	//le GO collidé est stocké là-dedans
	private GameObject _theShooterThatShotMe;

	[SerializeField]
	private float _SpeedBullet, _AllowedLinkSize;

	private float _timer = 0f;

	void Awake(){
		_myPlayerState = GetComponent<PlayerState>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		prevState = state;
		state = GamePad.GetState(playerIndex);

		_timer += 1 *Time.deltaTime;

		if(_timer>=1.5f){
			if(state.ThumbSticks.Right.X >0.5 || state.ThumbSticks.Right.X <-0.5||state.ThumbSticks.Right.Y >0.5||state.ThumbSticks.Right.Y<-0.5){
//				if(prevState.Buttons.B == ButtonState.Released && state.Buttons.B == ButtonState.Pressed){
//					Shoot();
//				}

				if(prevState.Buttons.RightShoulder == ButtonState.Released && state.Buttons.RightShoulder == ButtonState.Pressed){
					Shoot();
				}

//				if(prevState.Triggers.Right== 0 && state.Triggers.Right != 0){
//					Shoot();
//
//				}
			}
		}

//		if(_theShooterThatShotMe!=null){
//			DrawLine(_theShooterThatShotMe.transform.position, transform.position, Color.blue);
//		}

		if(_myPlayerState._whoLinkedMe.Count!=0){
			for (int i = 0; i < _myPlayerState._whoLinkedMe.Count;  i++) {
				DrawLine(_myPlayerState._whoLinkedMe[i].transform.position, transform.position, Color.blue);
				Debug.Log("they are"+ _myPlayerState._whoLinkedMe.Count);
			}
		}

		if(_myPlayerState.v_isPlayerLinked==true){
			float _linkSize = Vector3.Distance(_theShooterThatShotMe.transform.position, gameObject.transform.position);

			//cette ligne brise le lien si le tireur appuie sur A OU si le recepteur appuie sur A OU si la distance entre les deux est supérieure à _AllowedLinkSize
			//le lien est brisé en passant IsLinked en False
			//ceci implique que pour l'instant, un joueur ne peut pas choisir quel lien il brise
//			if (_theShooterThatShotMe.GetComponent<LifeRestore>().prevState.Buttons.A == ButtonState.Released && _theShooterThatShotMe.GetComponent<LifeRestore>().state.Buttons.A == ButtonState.Pressed
//			    || prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed
//			    || _linkSize>_AllowedLinkSize){
//				BreakLink();
//			}

			//en dessous: PERMET DE BRISER LES LIENS EXTERIEURS
			//à compléter, ya djà les bases de la list des "gens qui m'ont tiré dessus"
			if(_myPlayerState._whoLinkedMe.Count!=0){
				if(prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed  || _linkSize>_AllowedLinkSize){
					/*for (int i = 0; i < _myPlayerState._whoLinkedMe.Count;  i++) {
						_myPlayerState._whoLinkedMe[i]
						Debug.Log("they are"+ _myPlayerState._whoLinkedMe.Count);
					}*/
					Debug.Log("count"+ _myPlayerState._whoLinkedMe.Count);
					_myPlayerState._whoLinkedMe.Clear();
					_myPlayerState.v_isPlayerLinked=false;
					_theShooterThatShotMe=null;
				}
			}
		}
	}

	void Shoot(){
		Vector3 _temp = new Vector3(state.ThumbSticks.Right.X, 0 ,state.ThumbSticks.Right.Y);

			GameObject newBullet = Instantiate(v_Bullet, transform.TransformPoint(0f,0f,0f)  , transform.rotation) as GameObject;
			//ajout d'un rigidbody2D sur le projectile
			Rigidbody rb = newBullet.GetComponent<Rigidbody>();
			
			//on stocke dans la bullet meme quel objet l'a tirée
			newBullet.GetComponent<PelletScript>().v_whoShotMe=gameObject;
			
			if (rb != null){
				rb.AddForce(_temp*_SpeedBullet);
			}
			_timer = 0;

	}

	void BreakLink(){
		//if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed){
		Debug.Log("cut");
		_myPlayerState.v_isPlayerLinked=false;
		_theShooterThatShotMe=null;
	}

	void DrawLine (Vector3 first, Vector3 second, Color color) {
		Debug.DrawLine(first, second, color);
	}

	void OnTriggerEnter(Collider collided){
		//pas de collision entre le tireur et son propre bullet
		if (collided.transform.GetComponent<PelletScript>()!=null) {
			if(collided.transform.GetComponent<PelletScript>().v_whoShotMe != collider.gameObject){
				//s'il s'agit d'une bullet
				if(collided.transform.tag.Equals("Pellet")){
					//faire de _isPlayerLinked un int? 1=1 lien, 2=2 liens etc ?
					
					//SI JE ME PRENDS UNE BALLE, ALORS
					_myPlayerState.v_isPlayerLinked=true;
					//on stocke DE QUI VIENT LA BALLE
					_theShooterThatShotMe=collided.transform.GetComponent<PelletScript>().v_whoShotMe;
					//on ajoute AUTANT DE FOIS QU IL NOUS TIRE DESSUS, ATTENTION!! le joueur qui nous a tiré dessus dans _myPlayerState._whoLinkedMe
					//_myPlayerState._whoLinkedMe.Add(_theShooterThatShotMe);

					if(_myPlayerState._whoLinkedMe.Contains(_theShooterThatShotMe)){
						
					}else{
						_myPlayerState._whoLinkedMe.Add(_theShooterThatShotMe);
					}
				}
			}
		}
	}
	
	//	void OnTriggerStay(Collider collided){
	//		
	//		if(collided.gameObject.layer==9){
	//			renderer.material.color=Color.red;
	//			gameObject.GetComponent<PlayerState>().v_myHP-=1;
	//		}
	//	}
	//	
	//	void OnTriggerExit(Collider collided){
	//		
	//		if(collided.gameObject.layer==9){
	//			gameObject.renderer.material.color=Color.blue;
	//		}
	//	}

}
