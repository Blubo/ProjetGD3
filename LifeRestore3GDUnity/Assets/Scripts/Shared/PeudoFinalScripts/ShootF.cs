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
	public float _SpeedBullet;

	//tete du grappin
	public GameObject _HookHead, _myHook, _myHook1;
	public  GameObject _target, _target1;
	//Bool pour permettre déplacement towards
	private bool _Dashing;

	public float _Force;
	private float _thisForce;	

	private Vector3 _ThingGrapped;
		
	void Awake(){
		_myPlayerState = GetComponent<PlayerState>();
		_Dashing = false;
//		_Force = 200.0f;
	}

	void FixedUpdate(){
		//dash vers 2
		if(state.Buttons.X == ButtonState.Pressed && _target1 != null){
			MovetowardsHook(_target1);
			_Dashing = true;
		}else{
			_Dashing = false;
		}
		//Dash vers 1
		if(state.Buttons.B == ButtonState.Pressed && _target != null){
			MovetowardsHook(_target);
			_Dashing = true;
		}else{
			_Dashing = false;
		}

		//*******************************************************************INPUTS (sauf dash qui est dans le fixed upadte)

		//décrémenter
		//tir droit 
		_timer += 1 *Time.deltaTime;
		if(_timer>=1.5f){
			if(state.ThumbSticks.Right.X >0.5 || state.ThumbSticks.Right.X <-0.5||state.ThumbSticks.Right.Y >0.5||state.ThumbSticks.Right.Y<-0.5){
				if(prevState.Triggers.Right == 0 && state.Triggers.Right != 0){
					if(_target != null){
						DetachLink(0);
					}
					Hook();
				}
			}
		}

		//tir gauche
		_timer1 += 1 *Time.deltaTime;
		if(_timer1>=1.5f){
			if(state.ThumbSticks.Right.X >0.5 || state.ThumbSticks.Right.X <-0.5||state.ThumbSticks.Right.Y >0.5||state.ThumbSticks.Right.Y<-0.5){
				if(prevState.Triggers.Left == 0 && state.Triggers.Left != 0){
					if(_target1 != null){
						DetachLink(1);
					}
					Hook1();
				}
			}
		}
		//Annluation des liens 
//		if(prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed && _target != null){
		if(state.Buttons.A == ButtonState.Pressed && _target != null){
			DetachLink(2);
		}

		if(state.Buttons.RightShoulder == ButtonState.Pressed && _target != null){
			PullTowardsPlayer(_target);
		}
		if(state.Buttons.LeftShoulder == ButtonState.Pressed && _target1 != null){
			PullTowardsPlayer(_target1);
		}
	}

	// Update is called once per frame
	void Update () {
		prevState = state;
		state = GamePad.GetState(playerIndex);
		_LinksBehavior = GetComponent<LinkStrenght> ();

		_thisForce = (_LinksBehavior._LinkCommited+1) *_Force;
//		Debug.Log("force "+_Force);
//		Debug.Log("this force "+_thisForce);
		//INSUFFISANT
//		if(prevState.Buttons.Y == ButtonState.Released && state.Buttons.Y == ButtonState.Pressed){
//			if(_myHook!=null) Destroy(_myHook);
//			if(_myHook1!=null) Destroy(_myHook1);
//		}
	}

	// Grappin 1 
	void Hook(){
		//droite
		_myHook = Instantiate(_HookHead, transform.TransformPoint(0f,0f,0f), transform.rotation) as GameObject;
		Rigidbody rb = _myHook.GetComponent<Rigidbody>();
		if (rb != null)	rb.AddForce(gameObject.transform.forward* _SpeedBullet);
		_myHook.GetComponent<HookHeadF>()._myShooter=gameObject;
		_myHook.GetComponent<HookHeadF>().howWasIShot=1;
		_timer = 0;
	}

	void Hook1(){
		//gauche
		_myHook1 = Instantiate(_HookHead, transform.TransformPoint(0f,0f,0f), transform.rotation) as GameObject;
		Rigidbody rb = _myHook1.GetComponent<Rigidbody>();
		if (rb != null)	rb.AddForce(gameObject.transform.forward* _SpeedBullet);
		_myHook1.GetComponent<HookHeadF>()._myShooter=gameObject;
		_myHook1.GetComponent<HookHeadF>().howWasIShot=2;
		_timer1 = 0;
	}

	void MovetowardsHook(GameObject _target){
		Vector3 thisCible = new Vector3(_target.transform.position.x ,gameObject.transform.position.y, _target.transform.position.z);
//		transform.LookAt(_target.transform);
		transform.LookAt(thisCible);

		rigidbody.AddForce (transform.forward*_thisForce);
	}

	void PullTowardsPlayer(GameObject _target){
		Vector3 whereShouldIGo = transform.position - _target.transform.position;
		whereShouldIGo.Normalize();
//		_target.rigidbody.AddForce ((transform.position - _target.transform.position) * _thisForce/ 10);
		_target.rigidbody.AddForce ((whereShouldIGo) * _thisForce);

	}
	
	//detache le(s) liens
	void DetachLink(int _Todestroy){
		//Grappin 1
		if(_Todestroy == 0){
			if(_target != null){ Destroy(_myHook);}
			//Destroy(_target);
			if(_target.gameObject.tag!="Player"){
				if(_target.gameObject.GetComponent<Sticky>()!=null){
					_target.gameObject.GetComponent<Sticky>().v_numberOfLinks-=1;

					if(_target.gameObject.GetComponent<Sticky>().v_numberOfLinks <= 0){
						Destroy(_target.rigidbody);
					}
				}
			}

			if(_target.gameObject.tag=="Player"){
				if(_target.gameObject.GetComponent<LinkStrenght>()!=null){
					_target.gameObject.GetComponent<LinkStrenght>()._LinkCommited-=1;
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
					
					if(_target1.gameObject.GetComponent<Sticky>().v_numberOfLinks <= 0){
						Destroy(_target1.rigidbody);
					}
				}
			}

			if(_target1.gameObject.tag=="Player"){
				if(_target1.gameObject.GetComponent<LinkStrenght>()!=null){
					_target1.gameObject.GetComponent<LinkStrenght>()._LinkCommited-=1;
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
					}
				}
				if(_target.gameObject.tag!="Player"){
					if(_target.gameObject.GetComponent<Sticky>()!=null){
						_target.gameObject.GetComponent<Sticky>().v_numberOfLinks-=1;
						
						if(_target.gameObject.GetComponent<Sticky>().v_numberOfLinks <= 0){
							//mettre numberOfLinks à 0?
							Destroy(_target.rigidbody);
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
					}
				}
				if(_target1.gameObject.tag!="Player"){
					if(_target1.gameObject.GetComponent<Sticky>()!=null){
						_target1.gameObject.GetComponent<Sticky>().v_numberOfLinks-=1;
						
						if(_target1.gameObject.GetComponent<Sticky>().v_numberOfLinks <= 0){
							Destroy(_target1.rigidbody);
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
		if (_Collision.gameObject.tag == "Player" && _Collision.gameObject != gameObject ) {
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
