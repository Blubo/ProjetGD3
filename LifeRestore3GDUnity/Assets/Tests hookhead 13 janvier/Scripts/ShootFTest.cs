using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class ShootFTest : MonoBehaviour {

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
	public bool _Dashing;

	public float _Force;
	private float _thisForce;	

	private Vector3 _ThingGrapped;
		

	void Awake(){
		v_sizeRatio=1.75f;
		_initSizeRatio = v_sizeRatio;
		_myPlayerState = GetComponent<PlayerState>();
		_Dashing = false;
	}

	void FixedUpdate(){
//		Debug.Log(_Dashing);
		//Dash vers 1
		if(state.Buttons.RightShoulder == ButtonState.Pressed && _target != null){

//		if(state.Buttons.B == ButtonState.Pressed && _target != null){
			MovetowardsHook(_target);

			_Dashing = true;
//		}else{
//			_Dashing = false;
		}
		//dash vers 2
		if(state.Buttons.LeftShoulder == ButtonState.Pressed && _target1 != null){

//		if(state.Buttons.X == ButtonState.Pressed && _target1 != null){
			MovetowardsHook(_target1);
			_Dashing = true;
//		}else{
//			_Dashing = false;
		}

		if(state.Buttons.RightShoulder == ButtonState.Pressed && _target != null || state.Buttons.LeftShoulder == ButtonState.Pressed && _target1 != null){
			_Dashing = true;
		}else {_Dashing = false;}

		//*******************************************************************INPUTS (sauf dash qui est dans le fixed upadte)

		//décrémenter
		//tir droit 
		_timer += 1 *Time.deltaTime;
		if(_timer>=v_coolDown){
			//if(state.ThumbSticks.Right.X >0.5 || state.ThumbSticks.Right.X <-0.5||state.ThumbSticks.Right.Y >0.5||state.ThumbSticks.Right.Y<-0.5){
				if(prevState.Triggers.Right == 0 && state.Triggers.Right != 0){
					if(_target != null){
						DetachLink(0);
					}
					Hook();
				}
			//}
		}

		//tir gauche
		_timer1 += 1 *Time.deltaTime;
		if(_timer1>=v_coolDown){
			//if(state.ThumbSticks.Right.X >0.5 || state.ThumbSticks.Right.X <-0.5||state.ThumbSticks.Right.Y >0.5||state.ThumbSticks.Right.Y<-0.5){
				if(prevState.Triggers.Left == 0 && state.Triggers.Left != 0){
					if(_target1 != null){
						DetachLink(1);
					}
					Hook1();
				}
			//}
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
//		Debug.Log("force "+_Force);
//		Debug.Log("this force "+_thisForce);
	}

	// Grappin 1 
	void Hook(){
		//droite
		_myHook = Instantiate(_HookHead, v_instantiateur.transform.position, transform.rotation) as GameObject;
		Rigidbody rb = _myHook.GetComponent<Rigidbody>();
		if (rb != null)	rb.AddForce(gameObject.transform.forward* v_SpeedBullet);
		_myHook.GetComponent<HookHeadFTest>()._myShooter=gameObject;
		_myHook.GetComponent<HookHeadFTest>().howWasIShot=1;
		_timer = 0;
	}

	void Hook1(){
		//gauche
		_myHook1 = Instantiate(_HookHead, v_instantiateur.transform.position, transform.rotation) as GameObject;
		Rigidbody rb = _myHook1.GetComponent<Rigidbody>();
		if (rb != null)	rb.AddForce(gameObject.transform.forward* v_SpeedBullet);
		_myHook1.GetComponent<HookHeadFTest>()._myShooter=gameObject;
		_myHook1.GetComponent<HookHeadFTest>().howWasIShot=2;
		_timer1 = 0;
	}

	void MovetowardsHook(GameObject _target){
		Vector3 thisCible = new Vector3(_target.transform.position.x, gameObject.transform.position.y, _target.transform.position.z);
//		transform.LookAt(_target.transform);
		transform.LookAt(thisCible);

		rigidbody.AddForce (transform.forward*_thisForce);
	}

	void PullTowardsPlayer(GameObject _target){
		Vector3 whereShouldIGo = transform.position - _target.transform.position;
		whereShouldIGo.Normalize();
		_target.rigidbody.AddForce ((whereShouldIGo) * _thisForce);

		if(_target.tag=="Player"){
			_target.transform.rigidbody.AddForce ((whereShouldIGo) * _thisForce);
		}
	}

//	void PullTowardsPlayer(GameObject _target, Vector3 _origin){
//		Vector3 whereShouldIGo = transform.position - _target.transform.position;
//		whereShouldIGo.Normalize();
//		//		_target.rigidbody.AddForce ((transform.position - _target.transform.position) * _thisForce/ 10);
//		_target.rigidbody.AddForceAtPosition ((whereShouldIGo) * _thisForce, _origin);
//		Debug.Log("btaienbotan");
//		if(_target.tag=="Player"){
//			_target.transform.parent.rigidbody.AddForce ((whereShouldIGo) * _thisForce);
//		}
//	}
	
	//detache le(s) liens
	public void DetachLink(int _Todestroy){
		//Grappin 1
		if(_Todestroy == 0){
			if(_target != null){ Destroy(_myHook);}
			//Destroy(_target);
			if(_target.gameObject.tag!="Player"){
				if(_target.gameObject.GetComponent<Sticky>()!=null){
					_target.gameObject.GetComponent<Sticky>().v_numberOfLinks-=1;

					if(_target.gameObject.GetComponent<Sticky>().v_numberOfLinks <= 0){
//						Destroy(_target.rigidbody);
					}
				}
			}

			if(_target.gameObject.tag=="Player"){
				if(_target.gameObject.GetComponent<LinkStrenght>()!=null){
					_target.gameObject.GetComponent<LinkStrenght>()._LinkCommited-=1;
					gameObject.GetComponent<LinkStrenght>()._LinkCommited-=1;
					//si on veut, on rajoute unclamp à linkComited dans LinkStrenght
				}
			}
			_target=null;
		}

		//grappin 2
		if(_Todestroy == 1){
//			Destroy(_target1);
			if(_target1 != null){ Destroy(_myHook1);}
			if(_target1.gameObject.tag!="Player"){
				if(_target1.gameObject.GetComponent<Sticky>()!=null){
					_target1.gameObject.GetComponent<Sticky>().v_numberOfLinks-=1;
					gameObject.GetComponent<LinkStrenght>()._LinkCommited-=1;

					if(_target1.gameObject.GetComponent<Sticky>().v_numberOfLinks <= 0){
//						Destroy(_target1.rigidbody);
					}
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
				if(_target.gameObject.tag=="Player"){
					if(_target.gameObject.GetComponent<LinkStrenght>()!=null){
						_target.gameObject.GetComponent<LinkStrenght>()._LinkCommited-=1;
						gameObject.GetComponent<LinkStrenght>()._LinkCommited-=1;

					}
				}
				if(_target.gameObject.tag!="Player"){
					if(_target.gameObject.GetComponent<Sticky>()!=null){
						_target.gameObject.GetComponent<Sticky>().v_numberOfLinks-=1;

						
						if(_target.gameObject.GetComponent<Sticky>().v_numberOfLinks <= 0){
							//mettre numberOfLinks à 0?
//							Destroy(_target.rigidbody);
						}
					}
//					Destroy(_target.rigidbody);
				}
			}

			if(_target1 != null){ 
				Destroy(_myHook1);
				if(_target1.gameObject.tag=="Player"){
					if(_target1.gameObject.GetComponent<LinkStrenght>()!=null){
						_target1.gameObject.GetComponent<LinkStrenght>()._LinkCommited-=1;
						gameObject.GetComponent<LinkStrenght>()._LinkCommited-=1;

					}
				}
				if(_target1.gameObject.tag!="Player"){
					if(_target1.gameObject.GetComponent<Sticky>()!=null){
						_target1.gameObject.GetComponent<Sticky>().v_numberOfLinks-=1;

						
						if(_target1.gameObject.GetComponent<Sticky>().v_numberOfLinks <= 0){
//							Destroy(_target1.rigidbody);
						}
					}
					//Destroy(_target1.rigidbody);
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
					ShootFTest _PlayerTarget = _Collision.gameObject.GetComponent<ShootFTest>();
					if(_PlayerTarget != null){
						_Collision.gameObject.GetComponent<ShootFTest>().SendMessage("DetachLink", 2);
					}
				}
			}	
		}
	}
}
