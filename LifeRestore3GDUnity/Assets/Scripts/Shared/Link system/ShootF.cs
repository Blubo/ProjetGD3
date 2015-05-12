using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

public class ShootF : MonoBehaviour {
	
	bool playerIndexSet = false;
	public PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;
	
	private PlayerState _myPlayerState;
	private LinkStrenght _LinksBehavior;

	private float _timer = 1.5f;
	public float v_SpeedBullet, v_SpeedBulletTarget, v_coolDown, v_sizeRatio, v_sizeGrowth;
	
	[HideInInspector]
	public float _initSizeRatio;
	
	//tete du grappin
	public GameObject _HookHead, v_instantiateur;
	
	[HideInInspector]
	public  GameObject _target, _myHook;

	public float _Force;
	private float _thisForce;	

	private Vector3 _ThingGrapped;

	private ReticuleCone myRetCone;

	void Awake(){
		myRetCone = gameObject.GetComponent<ReticuleCone>();

		v_sizeRatio=0.5f;
		_initSizeRatio = v_sizeRatio;
		_myPlayerState = GetComponent<PlayerState>();
	}
	
	// Update is called once per frame
	void Update () {

		_timer += Time.deltaTime;
		if(_timer>=v_coolDown){
			if(prevState.Triggers.Right == 0 && state.Triggers.Right != 0){
				if(myRetCone.Vision()!=null){
					if(_target != null){
						DetachLink(0);
					}
					HookTarget();
				}else{
					if(_target != null){
						DetachLink(0);
					}
					Hook();
				}
			}
		}

		if (prevState.Triggers.Right != 0 && state.Triggers.Right != 0 ) {
		} else {
			if(_target != null){
				DetachLink(0);
			}
		}

		//tir droit 
//		_timer += Time.deltaTime;
//		if(_timer>=v_coolDown){
//			if(prevState.Triggers.Right == 0 && state.Triggers.Right != 0){
//				if(_target != null){
//					DetachLink(0);
//				}
//				Hook();
//			}
//		}
//		
//		if (prevState.Triggers.Right != 0 && state.Triggers.Right != 0 ) {
//		} else {
//			if(_target != null){
//				DetachLink(0);
//			}
//		}
		
		prevState = state;
		state = GamePad.GetState(playerIndex);
		_LinksBehavior = GetComponent<LinkStrenght> ();
		
		_thisForce = (_LinksBehavior._LinkCommited+1) *_Force;

		if(prevState.Buttons.Back == ButtonState.Released && state.Buttons.Back == ButtonState.Pressed){
			Application.LoadLevel(Application.loadedLevelName);
		}

		//REMOVE
		if(state.Buttons.LeftShoulder == ButtonState.Pressed){
			myRetCone.viewAngle-=Time.deltaTime*4;
		}

		if(state.Buttons.RightShoulder == ButtonState.Pressed){
			myRetCone.viewAngle+=Time.deltaTime*4;
		}

		if(state.Buttons.Y == ButtonState.Pressed){
			myRetCone.viewRange+=Time.deltaTime*2;
		}

		if(state.Buttons.A == ButtonState.Pressed){
			myRetCone.viewRange-=Time.deltaTime*2;
		}
		//END REMOVE
	}
	
	// Grappin 1 
	void Hook(){
		//ANCIEN AUDIO SANS FMOD
//		_whatSoundToPlay = Random.Range(0, v_linkShotList.Count);
//		GetComponent<AudioSource>().PlayOneShot(v_linkShotList[_whatSoundToPlay]);

		//ON JOUE LE SON FMOD ICI POUR LE TIR DU LIEN 
		Camera.main.GetComponent<SoundManagerHeritTest>().PlaySoundOneShot("Ouglou tir");

		//droite
		_myHook = Instantiate(_HookHead, v_instantiateur.transform.position, transform.rotation) as GameObject;
		Rigidbody rb = _myHook.GetComponent<Rigidbody>();
		if (rb != null)	rb.AddForce(gameObject.transform.forward* v_SpeedBullet * 1000);
		_myHook.GetComponent<HookHeadF>()._myShooter=gameObject;
		_myHook.GetComponent<HookHeadF>().howWasIShot=1;
		_timer = 0;
	}

	void HookTarget(){
		Camera.main.GetComponent<SoundManagerHeritTest>().PlaySoundOneShot("Ouglou tir");
		
		//droite
		_myHook = Instantiate(_HookHead, v_instantiateur.transform.position, transform.rotation) as GameObject;
		Rigidbody rb = _myHook.GetComponent<Rigidbody>();
		if (rb != null)	rb.AddForce((myRetCone.Vision().transform.position- gameObject.transform.position).normalized * v_SpeedBulletTarget * 1000);
		_myHook.GetComponent<HookHeadF>()._myShooter=gameObject;
		_myHook.GetComponent<HookHeadF>().howWasIShot=1;
		_timer = 0;
	}

	//detache le(s) liens
	public void DetachLink(int _Todestroy){
//		GetComponent<AudioSource>().PlayOneShot(v_linkBroken);
		
		//Grappin 1
		if(_Todestroy == 0){
			
			if(_target != null){
         		Vector3 direction = _myHook.transform.position-gameObject.transform.position;
				direction.Normalize();

				Destroy(_myHook);

				if(gameObject.GetComponent<SpringJoint>()!=null) Destroy(gameObject.GetComponent<SpringJoint>());

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

				if(playerIndex == PlayerIndex.One){
					_target.GetComponent<ReticuleTarget>().TurnReticuleOff(_target.GetComponent<ReticuleTarget>().GRend);
					
				}else if(playerIndex == PlayerIndex.Two){
					_target.GetComponent<ReticuleTarget>().TurnReticuleOff(_target.GetComponent<ReticuleTarget>().RRend);
					
				}else if(playerIndex == PlayerIndex.Three){
					_target.GetComponent<ReticuleTarget>().TurnReticuleOff(_target.GetComponent<ReticuleTarget>().BRend);
					
				}else if(playerIndex == PlayerIndex.Four){
					_target.GetComponent<ReticuleTarget>().TurnReticuleOff(_target.GetComponent<ReticuleTarget>().YRend);
				}

				_target=null;

			}else{
				Destroy(_myHook);
				if(gameObject.GetComponent<SpringJoint>()!=null) Destroy(gameObject.GetComponent<SpringJoint>());

			}
		}
	}
}
