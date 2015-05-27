using UnityEngine;
using System.Collections;
using XInputDotNetPure;


public class MovementScript5Janv : MonoBehaviour {

	bool playerIndexSet = false;
	public PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;
	
	public float v_movementSpeed;

	//rotation
	private float _rightStickX, _rightStickY;
	private Vector3 previousVectorMov, previousVectorRot;

	//movement
	[HideInInspector]
	public Vector3 _movement;

	public AudioClip v_playerCollision;

	[Tooltip("Check to change aiming system")]
	[SerializeField]
	private bool _alternateAiming, _strafe;

	//Tentative for traction, used in elasticScript
	[HideInInspector]
	public Vector3 _inputDirectionNormalized;

	private Animator myAvatarAnimator;

	// Use this for initialization
	void Start () {
//		_alternateAiming=false;
//		_strafe=false;
		//_movementSpeed=10f;

		myAvatarAnimator = transform.Find("Avatar/Body").GetComponent<Animator>();

	}

	// Update is called once per frame
	void FixedUpdate () {		
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
		if(GetComponent<Rigidbody>() == null){
			gameObject.AddComponent<Rigidbody>();
			gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
//			gameObject.rigidbody.drag=5.0f;
			gameObject.GetComponent<Rigidbody>().drag=4.5f;

		}

		Movement();
		gameObject.GetComponent<Rigidbody>().mass = 1.0f;
	}

	// Update is called once per frame
	void Update () {

		myAvatarAnimator.SetFloat("Speed", GetComponent<Rigidbody>().velocity.magnitude);

		prevState = state;
		state = GamePad.GetState(playerIndex);
		
		//orientation
		//Vector3 _temp = new Vector3(state.ThumbSticks.Right.X, 0 ,state.ThumbSticks.Right.Y);
		
		_rightStickX=state.ThumbSticks.Right.X;
		_rightStickY=state.ThumbSticks.Right.Y;

		//Louis prépare un strafe à la hammerwatch!!
		if(_strafe==true){
			if(state.Buttons.LeftShoulder==ButtonState.Pressed){
				_alternateAiming=false;
			}else{
				_alternateAiming=true;
			}
		}

		if(_alternateAiming==true){
			if(state.ThumbSticks.Left.X!=0 || state.ThumbSticks.Left.Y!=0){
				//if(state.ThumbSticks.Right.X!=0 && state.ThumbSticks.Right.Y!=0){
				//Vector3 player_pos = Camera.main.WorldToScreenPoint(this.transform.position);
				float angle = Mathf.Atan2 (state.ThumbSticks.Left.Y, state.ThumbSticks.Left.X) * Mathf.Rad2Deg;
				this.transform.rotation = Quaternion.Euler (new Vector3(0, -angle+90, 0));
			}
		}

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

		if(gameObject.GetComponent<Rigidbody>().velocity.sqrMagnitude == 0){
			//idle

		}
	}

	//Movement de Base avec joystick
	void Movement(){
		//un joueur attiré peut se déplacer librement vu que ces déplacement ne dépendent pas de la force
//		Vector3 movement = new Vector3(state.ThumbSticks.Left.X * v_movementSpeed * Time.deltaTime, 0.0f, state.ThumbSticks.Left.Y * v_movementSpeed * Time.deltaTime );
//		transform.localPosition += movement;

		Vector3 direction = new Vector3(0,0,0);
		direction.x=(state.ThumbSticks.Left.X);
		direction.y=0;
		direction.z=( state.ThumbSticks.Left.Y);
		direction.Normalize();
		_inputDirectionNormalized = direction;
		_movement = direction*v_movementSpeed;
		GetComponent<Rigidbody>().AddForce(_movement);

		//ligne mise en commentaire pour pas niquer l'orientation par rapport au stick droit
		//transform.eulerAngles=new Vector3(0,0,transform.eulerAngles.z);
	}

//	void OnCollisionEnter (Collision collision){
//		if(collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("Block")){
//			GetComponent<AudioSource>().PlayOneShot(v_playerCollision);
////			Debug.Log("this fucker's name "+ gameObject.name);
//		}
//
////		if(collision.gameObject.tag.Equals("Block")){
////			
////		}
//
//	}
}
