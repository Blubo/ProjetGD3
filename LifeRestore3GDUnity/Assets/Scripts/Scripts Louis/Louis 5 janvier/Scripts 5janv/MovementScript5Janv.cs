using UnityEngine;
using System.Collections;
using XInputDotNetPure;


public class MovementScript5Janv : MonoBehaviour {

	bool playerIndexSet = false;
	public PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;
	
	public float v_movementSpeed, v_dashConstant;
	
	//Dash 
	private float _DashTimer;
	private float _DashTimerCD;

	private float _DashDuree;
	private float _DashCD;
	private bool _StopDash;
	
	private int _AtkDash;

	//rotation
	private float _rightStickX, _rightStickY;
	private Vector3 previousVectorMov, previousVectorRot;
	
	//Delegate pour enlever et remettre 
	//delegate void Mydelegate();
	//Mydelegate Actions ;
	
	// Use this for initialization
	void Start () {
		//_movementSpeed=10f;
		
		_DashDuree = 0.3f;
		_DashTimer = _DashDuree;
		_DashCD = 0.5f;
		_DashTimerCD = _DashCD;
		_StopDash = false;
		
		_AtkDash = 100;
	}
	
	// Update is called once per frame
	void Update () {
		prevState = state;
		state = GamePad.GetState(playerIndex);

		//orientation
		Vector3 _temp = new Vector3(state.ThumbSticks.Right.X, 0 ,state.ThumbSticks.Right.Y);
		
		_rightStickX=state.ThumbSticks.Right.X;
		_rightStickY=state.ThumbSticks.Right.Y;
		
		//http://blog.rastating.com/creating-a-2d-rotating-aim-assist-in-unity/
		
		//		si je vise pas
		//		if(state.ThumbSticks.Right.X==0 && state.ThumbSticks.Right.Y==0){
		//		}
		if(state.ThumbSticks.Right.X!=0 || state.ThumbSticks.Right.Y!=0){
			//if(state.ThumbSticks.Right.X!=0 && state.ThumbSticks.Right.Y!=0){
			//Vector3 player_pos = Camera.main.WorldToScreenPoint(this.transform.position);
			float angle = Mathf.Atan2 (_rightStickY, _rightStickX) * Mathf.Rad2Deg;
			this.transform.rotation = Quaternion.Euler (new Vector3(0, -angle+90, 0));
		}
	}

	void FixedUpdate () {
		//Timer du CD du dash qui décroit 
		if(_DashTimer> -1.0f){_DashTimer -= 1.0f*Time.deltaTime;}
		if(_DashTimerCD> -1.0f){_DashTimerCD -= 1.0f*Time.deltaTime;}
		
		//		if (!playerIndexSet || !prevState.IsConnected)
		//		{
		//			for (int i = 0; i < 4; ++i)
		//			{
		//				PlayerIndex testPlayerIndex = (PlayerIndex)i;
		//				GamePadState testState = GamePad.GetState(testPlayerIndex);
		//				if (testState.IsConnected)
		//				{
		//					Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
		//					playerIndex = testPlayerIndex;
		//					playerIndexSet = true;
		//				}
		//			}
		//		}
		prevState = state;
		state = GamePad.GetState(playerIndex);
		
		//Ajout d'un rigidbody
		if(rigidbody == null){
			gameObject.AddComponent<Rigidbody>();
			gameObject.rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationZ;
//			gameObject.rigidbody.isKinematic=true;
			gameObject.rigidbody.drag=3.0f;
			//gameObject.rigidbody.collisionDetectionMode=CollisionDetectionMode.Continuous;
		}
		//Appui sur X = Dash 
		if (_DashTimer <= 0.0f) {
			if (_StopDash == true){gameObject.rigidbody.velocity = Vector3.zero; _StopDash = false;}
			Movement ();
			gameObject.rigidbody.mass = 1.0f;
		}
		if (prevState.Buttons.X == ButtonState.Released && state.Buttons.X == ButtonState.Pressed) {
			if(_DashTimerCD <= 0.0f){
				//Durant le dash on peut faire des tests colliders pour envoyer les gens plus loin, arreter le dash avant son intérgité etc.
				Dash ();
				//AR = (state.ThumbSticks.Left.X * _movementSpeed , 0.0f, state.ThumbSticks.Left.Y * _movementSpeed );
				_DashTimerCD =_DashCD;
			}else {
				//Debug.Log("dash non dispo");
			}
		}
	}
	//Movement de Base avec joystick
	void Movement(){
		
		Vector3 movement = new Vector3(state.ThumbSticks.Left.X * v_movementSpeed * Time.deltaTime, 0.0f, state.ThumbSticks.Left.Y * v_movementSpeed * Time.deltaTime );
		transform.localPosition += movement;

		//ligne mise en commentaire pour pas niquer l'orientation par rapport au stick droit
		//transform.eulerAngles=new Vector3(0,0,transform.eulerAngles.z);
	}
	
	//Action de dash 
	void Dash(){
		//possibilité de changer la hitbox pour faciliter le renre dedans
		gameObject.rigidbody.mass = 500.0f;
		gameObject.rigidbody.AddForce (new Vector3(state.ThumbSticks.Left.X * v_movementSpeed , 0.0f, state.ThumbSticks.Left.Y * v_movementSpeed)*v_dashConstant, ForceMode.Impulse);
		
		_StopDash = true;
		_DashTimer = _DashDuree;
	}
	void OnCollisionEnter (Collision _collider){
		//Si le dash touche quelque chose alors on inflige des dommages
		if(_StopDash == true){
			if(_collider.gameObject.tag == "Boss"){
				//Debug.Log("atk on boss");
				//
				_collider.gameObject.SendMessage("TakeDamage", _AtkDash);
			}
		}
	}
}
