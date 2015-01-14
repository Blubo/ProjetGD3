using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class ShootF : MonoBehaviour {

	bool playerIndexSet = false;
	public PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;

	private PlayerState _myPlayerState;
	private LinkStrenght _LinksBehavior;
	private HookHead _HookHeadbehavior,_HookHeadbehavior1;

	private float _timer = 1.5f, _timer1 = 1.5f;
	public float v_SpeedBullet, v_coolDown, v_sizeRatio, v_sizeGrowth;

	[HideInInspector]
	public float _initSizeRatio;

	//tete du grappin
	public GameObject _HookHead, v_instantiateur;

	[HideInInspector]
	public Vector3 _instantiatePos;

	[HideInInspector]
	public  GameObject _target, _target1, _myHook, _myHook1;
	//Bool pour permettre déplacement towards
	[HideInInspector]
	public bool _Dashing;

	public float _Force;
	private float _thisForce;	

	private Vector3 _ThingGrapped;

	public AudioClip v_linkShot, v_linkBroken;

	void Awake(){
		v_sizeRatio=1.75f;
		_initSizeRatio = v_sizeRatio;
		_myPlayerState = GetComponent<PlayerState>();
		_Dashing = false;
	}

	void FixedUpdate(){
		//Dash vers 1
		if(state.Buttons.RightShoulder == ButtonState.Pressed && _target != null){
			MovetowardsHook(_target);
		}

		//dash vers 2
		if(state.Buttons.LeftShoulder == ButtonState.Pressed && _target1 != null){
			MovetowardsHook(_target1);
		}

		if(state.Buttons.RightShoulder == ButtonState.Pressed && _target != null || state.Buttons.LeftShoulder == ButtonState.Pressed && _target1 != null){
			_Dashing = true;
		}else {
			_Dashing = false;
		}

		//*******************************************************************INPUTS (sauf dash qui est dans le fixed upadte)

		//DECREMENTER LA PATATE DES JOUEURS LIES

		//tir droit 
		_timer += 1 *Time.deltaTime;
		if(_timer>=v_coolDown){
			if(prevState.Triggers.Right == 0 && state.Triggers.Right != 0){
				if(_target != null){
					DetachLink(0);
				}
				Hook();
			}
		}

		//tir gauche
		_timer1 += 1 *Time.deltaTime;
		if(_timer1>=v_coolDown){
			if(prevState.Triggers.Left == 0 && state.Triggers.Left != 0){
				if(_target1 != null){
					DetachLink(1);
				}
				Hook1();
			}
		}

		//Annluation des liens 
//		if(prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed && _target != null){
		if(state.Buttons.A == ButtonState.Pressed && (_target != null || _target1 != null)){
			DetachLink(2);
		}
		if(state.Buttons.B == ButtonState.Pressed && _target != null){
			PullTowardsPlayer(_target);
		}

		if(state.Buttons.X == ButtonState.Pressed && _target1 != null){
			PullTowardsPlayer(_target1);
		}
	}

	// Update is called once per frame
	void Update () {
//		Debug.Log("la force de "+gameObject.name+ " est "+ gameObject.GetComponent<LinkStrenght>()._LinkCommited);
		prevState = state;
		state = GamePad.GetState(playerIndex);
		_LinksBehavior = GetComponent<LinkStrenght> ();

		Vector3 growth = new Vector3(0.1f,0.1f,0.1f);
		if(state.Buttons.Start == ButtonState.Pressed){
			gameObject.transform.localScale+=growth;
			v_sizeRatio+=v_sizeGrowth;
		}
		
		if(state.Buttons.Back == ButtonState.Pressed){
			gameObject.transform.localScale-=growth;
			v_sizeRatio-=v_sizeGrowth;
		}

		_thisForce = (_LinksBehavior._LinkCommited+1) *_Force;
	}

	// Grappin 1 
	void Hook(){
		audio.PlayOneShot(v_linkShot);
		//droite
		_myHook = Instantiate(_HookHead, v_instantiateur.transform.position, transform.rotation) as GameObject;
		Rigidbody rb = _myHook.GetComponent<Rigidbody>();
		if (rb != null)	rb.AddForce(gameObject.transform.forward* v_SpeedBullet);
		_myHook.GetComponent<HookHeadF>()._myShooter=gameObject;
		_myHook.GetComponent<HookHeadF>().howWasIShot=1;
		_timer = 0;
	}

	void Hook1(){
		audio.PlayOneShot(v_linkShot);
		//gauche
		_myHook1 = Instantiate(_HookHead, v_instantiateur.transform.position, transform.rotation) as GameObject;
		Rigidbody rb = _myHook1.GetComponent<Rigidbody>();
		if (rb != null)	rb.AddForce(gameObject.transform.forward* v_SpeedBullet);
		_myHook1.GetComponent<HookHeadF>()._myShooter=gameObject;
		_myHook1.GetComponent<HookHeadF>().howWasIShot=2;
		_timer1 = 0;
	}

	void MovetowardsHook(GameObject _target){
		Vector3 thisCible = new Vector3(_target.transform.position.x,gameObject.transform.position.y, _target.transform.position.z);
		//CE RATIO SERT A REPRESENTER LA PUISSANCE PLUS GRANDE SI JE SUIS LOIN DE L OBJET AUQUEL JE SUIS ATTACHE
		float ratio = Vector3.Distance(_target.transform.position, gameObject.transform.position)/ _HookHead.GetComponent<HookHeadF>().v_returnDistance;
		transform.LookAt(thisCible);
		if(_target.tag!="Player"){
			//possibilité d'augmenter le ratio artificiellement ? concretement, avec cette formule, on est OBLIGE de ralentir en dashant vers un bloc!
			//c'est pas "si je COMMENCE à dasher à cet endroit alors je vais dasher à tant de vitesse", c'est plutot
			//"ma vitesse de dash va décroitre régulierement tant que je m'approche de ma cible"...
			//soit changer de formule
			//(par exemple, tenter de stocker ce ratio à l'instant t et l'appliquer jusqu'à ce que dash soit faux ?
			//soit changer l'addForce (cf cahier des charges) pour voir les interactions d'une force "plus breve" et de ce truc
			rigidbody.AddForce (transform.forward*_thisForce*ratio);
		}

		if(_target.tag=="Player"){
			if(Vector3.Distance(_target.transform.position, gameObject.transform.position)>=8f){
				//LE RATIO EST PRESENT ICI AUSSI
				rigidbody.AddForce (transform.forward*_thisForce*ratio);
			}
		}
	}

	void PullTowardsPlayer(GameObject _target){
		Vector3 whereShouldIGo = transform.position - _target.transform.position;
		whereShouldIGo.Normalize();
		if(_target.tag!="Player"){
			_target.rigidbody.AddForce ((whereShouldIGo) * _thisForce);
		}

		if(_target.tag=="Player"){
			if(Vector3.Distance(_target.transform.position, gameObject.transform.position)>=8f){
				_target.rigidbody.AddForce ((whereShouldIGo) * _thisForce);
			}
		}
	}

	//detache le(s) liens
	public void DetachLink(int _Todestroy){
		audio.PlayOneShot(v_linkBroken);
		//Grappin 1
		if(_Todestroy == 0){

			if(_target != null){
				if(Vector3.Distance(gameObject.transform.position, _target.transform.position)>=gameObject.GetComponent<ElasticScript>().v_tensionlessDistance){
					Vector3 direction = _myHook.GetComponent<HookHeadF>().GrappedTo.transform.position-gameObject.transform.position;
					direction.Normalize();
					gameObject.GetComponent<ElasticScript>().ElasticBreak(direction, gameObject.GetComponent<ElasticScript>()._breaking1);
				}
				Destroy(_myHook);
			}

			if(_target.gameObject.tag!="Player"){
				if(_target.gameObject.GetComponent<Sticky>()!=null){
					_target.gameObject.GetComponent<Sticky>().v_numberOfLinks-=1;
				}
			}

			if(_target.gameObject.tag=="Player"){
				if(_target.gameObject.GetComponent<LinkStrenght>()!=null){
					_target.gameObject.GetComponent<LinkStrenght>()._LinkCommited-=1;
					gameObject.GetComponent<LinkStrenght>()._LinkCommited-=1;
				}
			}
			_target=null;
		}

		//grappin 2
		if(_Todestroy == 1){
//			Destroy(_target1);
			if(_target1 != null){
				if(Vector3.Distance(gameObject.transform.position, _target1.transform.position)>=gameObject.GetComponent<ElasticScript>().v_tensionlessDistance){
					Vector3 direction = _myHook1.GetComponent<HookHeadF>().GrappedTo.transform.position-gameObject.transform.position;
					direction.Normalize();
					gameObject.GetComponent<ElasticScript>().ElasticBreak(direction, gameObject.GetComponent<ElasticScript>()._breaking2);
				}
				Destroy(_myHook1);
			}

			if(_target1.gameObject.tag!="Player"){
				if(_target1.gameObject.GetComponent<Sticky>()!=null){
					_target1.gameObject.GetComponent<Sticky>().v_numberOfLinks-=1;
				}
			}

			if(_target1.gameObject.tag=="Player"){
				if(_target1.gameObject.GetComponent<LinkStrenght>()!=null){
					_target1.gameObject.GetComponent<LinkStrenght>()._LinkCommited-=1;
					gameObject.GetComponent<LinkStrenght>()._LinkCommited-=1;
				}
			}
			_target1=null;
		}

		//Tous grappins
		if(_Todestroy == 2){
			if(_target != null){
				Destroy(_myHook);
				if(Vector3.Distance(gameObject.transform.position, _target.transform.position)>=gameObject.GetComponent<ElasticScript>().v_tensionlessDistance){
					Vector3 direction = _myHook.GetComponent<HookHeadF>().GrappedTo.transform.position-gameObject.transform.position;
					direction.Normalize();
					gameObject.GetComponent<ElasticScript>().ElasticBreak(direction, gameObject.GetComponent<ElasticScript>()._breaking1);
				}
				if(_target.gameObject.tag=="Player"){
					if(_target.gameObject.GetComponent<LinkStrenght>()!=null){
						_target.gameObject.GetComponent<LinkStrenght>()._LinkCommited-=1;
						gameObject.GetComponent<LinkStrenght>()._LinkCommited-=1;

					}
				}
				if(_target.gameObject.tag!="Player"){
					if(_target.gameObject.GetComponent<Sticky>()!=null){
						_target.gameObject.GetComponent<Sticky>().v_numberOfLinks-=1;
					}
				}
			}

			if(_target1 != null){ 
				Destroy(_myHook1);
				if(Vector3.Distance(gameObject.transform.position, _target1.transform.position)>=gameObject.GetComponent<ElasticScript>().v_tensionlessDistance){
					Vector3 direction = _myHook1.GetComponent<HookHeadF>().GrappedTo.transform.position-gameObject.transform.position;
					direction.Normalize();
					gameObject.GetComponent<ElasticScript>().ElasticBreak(direction, gameObject.GetComponent<ElasticScript>()._breaking2);
				}
				if(_target1.gameObject.tag=="Player"){
					if(_target1.gameObject.GetComponent<LinkStrenght>()!=null){
						_target1.gameObject.GetComponent<LinkStrenght>()._LinkCommited-=1;
						gameObject.GetComponent<LinkStrenght>()._LinkCommited-=1;

					}
				}
				if(_target1.gameObject.tag!="Player"){
					if(_target1.gameObject.GetComponent<Sticky>()!=null){
						_target1.gameObject.GetComponent<Sticky>().v_numberOfLinks-=1;
					}
				}
			}

			_target=null;
			_target1=null;
		}
	}

	//élément pour le dash 
	void OnCollisionEnter(Collision _Collision){
		//destruction du lien
		if (_Collision.gameObject.transform.Find("ApparenceAvatar")!=null){
			if (_Collision.gameObject.transform.Find("ApparenceAvatar").tag == "Player" && _Collision.gameObject != gameObject ) {
				//Supprimer tous les liens du joueurs
				if (_Dashing){
					ShootF _PlayerTarget = _Collision.gameObject.GetComponent<ShootF>();
					if(_PlayerTarget != null){
						_Collision.gameObject.GetComponent<ShootF>().SendMessage("DetachLink", 2);
					}
				}
			}	
		}
	}
}
