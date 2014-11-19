using UnityEngine;
using System.Collections;
using XInputDotNetPure;

//l'idée: je tire une pilule sur qqch. si c'est un autre joueur, ca crée lien

public class LifeRestore : MonoBehaviour {
	
	bool playerIndexSet = false;
	public PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;

	private PlayerState _myPlayerState;

	//le GO servant à créer le lien pr l'instant
	public GameObject v_Bullet;

	//le GO collidé est stocké là-dedans
	private GameObject _collided;

	[SerializeField]
	private float _SpeedBullet;

	private float _timer = 0f;

	void Awake(){
		_myPlayerState = GetComponent<PlayerState>();
	
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// Find a PlayerIndex, for a single player game
		// Will find the first controller that is connected ans use it
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

		_timer += 1 *Time.deltaTime;
		Vector3 _temp = new Vector3(state.ThumbSticks.Right.X, 0 ,state.ThumbSticks.Right.Y);
		if(_timer>=1.5f){
			if(state.ThumbSticks.Right.X >0.5 || state.ThumbSticks.Right.X <-0.5||state.ThumbSticks.Right.Y >0.5||state.ThumbSticks.Right.Y<-0.5){
				GameObject newBullet = Instantiate(v_Bullet, transform.TransformPoint(0f,0f,0f)  , transform.rotation) as GameObject;
				//ajout d'un rigidbody2D sur le projectile
				Rigidbody rb = newBullet.GetComponent<Rigidbody>();
				newBullet.transform.parent=gameObject.transform;
				if (rb != null){
					rb.AddForce(_temp*_SpeedBullet);
				}
				_timer = 0;
			}
		}
		if(_collided!=null){
			DrawLine(_collided.transform.position, transform.position, Color.blue);
		}
	}

	void OnTriggerEnter(Collider collided){
		//pas de collision avec son propre parent
		if(collided.transform.parent !=collider.transform ){
			//s'il s'agit d'une bullet
			if(collided.transform.tag.Equals("Pellet")){
				_myPlayerState.v_isPlayerLinked=true;
				//on stocke qui on a collidé
				_collided=collided.transform.parent.gameObject;
				//Debug.DrawLine(collided.transform.parent.transform.position, transform.position, Color.blue);
			}
		}
	}

	void DrawLine (Vector3 first, Vector3 second, Color color) {
		Debug.DrawLine(first, second, color);
	}

}
