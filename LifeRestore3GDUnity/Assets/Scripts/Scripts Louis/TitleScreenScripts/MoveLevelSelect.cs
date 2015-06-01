using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class MoveLevelSelect : MonoBehaviour {

	public bool playerIndexSet = false;
	public PlayerIndex playerIndex;
	public GamePadState state;
	public GamePadState prevState;

	[HideInInspector]
	public bool playersInSight = false;

	public bool inputsAllowed = true;

	public Vector3[] nodes, cameraNodes; //master list of all nodes, in order
	public Vector3[] path, cameraPath; //the path that you will use for iTween

	private iTweenPath myItweenPath;
	private iTweenPath cameraiTweenPath;

	public GameObject pathHolder, cameraPathHolder;

	[SerializeField]
	private GameObject player1, player2, player3;
	[SerializeField]
	private Transform reputPlayer1, reputPlayer2, reputPlayer3;
	[SerializeField]
	private GameObject trigger0to1;
	// Use this for initialization
	void Start () {
		myItweenPath = pathHolder.GetComponent<iTweenPath>();
		cameraiTweenPath = cameraPathHolder.GetComponent<iTweenPath>();

		nodes = new Vector3[myItweenPath.nodes.Count];
		for (int i = 0; i < myItweenPath.nodes.Count; i++) {
			nodes[i] = myItweenPath.nodes[i];
		}

		cameraNodes = new Vector3[cameraiTweenPath.nodes.Count];
		for (int i = 0; i < cameraiTweenPath.nodes.Count; i++) {
			cameraNodes[i] = cameraiTweenPath.nodes[i];
		}
	}
	
	// Update is called once per frame
	void Update () {

		if(gameObject.transform.position != nodes[0]
		   && gameObject.transform.position != nodes[8]
		   && gameObject.transform.position != nodes[12]
		   && gameObject.transform.position != nodes[15]){
			inputsAllowed = false;
			Debug.Log("NOPE");
		}else{
			inputsAllowed = true;
		}

		if(playersInSight == false) inputsAllowed = false;

		//si on permet de bouger (si on est sur cet partie de l'écran de début)
		if(inputsAllowed == true){
			//SI JE SUIS AU WAYPOINT 0 
			if(gameObject.transform.position == nodes[0]){
				if(prevState.ThumbSticks.Left.X==0 && state.ThumbSticks.Left.X<0){
					ChoosePath(0, 8);
					iTween.MoveTo(gameObject, iTween.Hash("path", path, "orienttopath", true, "lookahead", 0.5f, "time", 1.5f, "easetype", iTween.EaseType.easeInOutSine));
//					ChooseCameraPath(0, 1);
//					iTween.MoveTo(Camera.main.gameObject, iTween.Hash("path", cameraPath, "time", 1.5f, "easetype", iTween.EaseType.easeInOutSine));
				}else if(prevState.ThumbSticks.Left.Y==0 && state.ThumbSticks.Left.Y<0){

					gameObject.GetComponent<SmallCouloirMove>().CouloirMove(1);

//					Debug.Log("well?");
					playersInSight = false;

					trigger0to1.GetComponent<TitleScreenTrigger0to1>().playerCount = 0;

					player1.transform.position= reputPlayer1.position;
					player2.transform.position= reputPlayer2.position;
					player3.transform.position= reputPlayer3.position;

					player1.GetComponent<TitleScreenPlayerPosition>().ChangePlayerState(true);
					player2.GetComponent<TitleScreenPlayerPosition>().ChangePlayerState(true);
					player3.GetComponent<TitleScreenPlayerPosition>().ChangePlayerState(true);

					Camera.main.GetComponent<TitleScreenCameraManager>().MoveCamera(0);
					Camera.main.GetComponent<TitleScreenCameraManager>().currentWayPointNumber = 0;
					Camera.main.transform.Find("GeneralScoreUI").gameObject.SetActive(false);
				}

//				if(prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed){
//					Application.LoadLevel(1);
//				}

			}else
				//SI JE SUIS AU WAYPOINT 1
			if(gameObject.transform.position == nodes[8]){
				if(prevState.ThumbSticks.Left.X==0 && state.ThumbSticks.Left.X>0){
					ChoosePath(8, 12);
					iTween.MoveTo(gameObject, iTween.Hash("path", path, "orienttopath", true, "lookahead", 0.5f,"time", 1.5f, "easetype", iTween.EaseType.easeInOutSine));
//					ChooseCameraPath(1, 2);
//					iTween.MoveTo(Camera.main.gameObject, iTween.Hash("path", cameraPath, "time", 1.5f, "easetype", iTween.EaseType.easeInOutSine));
				}else if(prevState.ThumbSticks.Left.X==0 && state.ThumbSticks.Left.X<0){
					ChoosePath(8, 0);
					iTween.MoveTo(gameObject, iTween.Hash("path", path, "orienttopath", true, "lookahead", 0.5f,"time", 1.5f, "easetype", iTween.EaseType.easeInOutSine));
//					ChooseCameraPath(1, 0);
//					iTween.MoveTo(Camera.main.gameObject, iTween.Hash("path", cameraPath, "time", 1.5f, "easetype", iTween.EaseType.easeInOutSine));
				}

				if(prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed){
					Application.LoadLevel(1);
				}

			}else
				//SI JE SUIS AU WAYPOINT 2
			if(gameObject.transform.position == nodes[12]){
				if(prevState.ThumbSticks.Left.X==0 && state.ThumbSticks.Left.X>0){
					ChoosePath(12, 15);
					iTween.MoveTo(gameObject, iTween.Hash("path", path, "orienttopath", true, "lookahead", 0.5f,"time", 1.5f, "easetype", iTween.EaseType.easeInOutSine));
//					ChooseCameraPath(2, 3);
//					iTween.MoveTo(Camera.main.gameObject, iTween.Hash("path", cameraPath, "time", 1.5f, "easetype", iTween.EaseType.easeInOutSine));
				}else if(prevState.ThumbSticks.Left.X==0 && state.ThumbSticks.Left.X<0){
					ChoosePath(12, 8);
					iTween.MoveTo(gameObject, iTween.Hash("path", path, "orienttopath", true, "lookahead", 0.5f,"time", 1.5f, "easetype", iTween.EaseType.easeInOutSine));
//					ChooseCameraPath(2, 1);
//					iTween.MoveTo(Camera.main.gameObject, iTween.Hash("path", cameraPath, "time", 1.5f, "easetype", iTween.EaseType.easeInOutSine));
				}

				if(prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed){
					Application.LoadLevel(2);
				}

			}else
				//SI JE SUIS AU WAYPOINT 3
			if(gameObject.transform.position == nodes[15]){
				if(prevState.ThumbSticks.Left.X==0 && state.ThumbSticks.Left.X>0){
//					ChoosePath(3, 4);
//					iTween.MoveTo(gameObject, iTween.Hash("path", path, "time", 5, "easetype", iTween.EaseType.easeInOutSine));
					//
				}else if(prevState.ThumbSticks.Left.X==0 && state.ThumbSticks.Left.X<0){
					ChoosePath(15, 12);
					iTween.MoveTo(gameObject, iTween.Hash("path", path, "orienttopath", true, "lookahead", 0.5f,"time", 1.5f, "easetype", iTween.EaseType.easeInOutSine));
//					ChooseCameraPath(3, 2);
//					iTween.MoveTo(Camera.main.gameObject, iTween.Hash("path", cameraPath, "time", 1.5f, "easetype", iTween.EaseType.easeInOutSine));
				}

				if(prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed){
					Application.LoadLevel(3);
				}
			}
		}

		PlayerIndex playerIndex = (PlayerIndex)0;
		prevState = state;
		state = GamePad.GetState(playerIndex);
	}
		
	void ChoosePath (int pathStart, int pathEnd) {
		path = new Vector3[Mathf.Abs(pathEnd-pathStart)+1];
		//make the path move in the opposite direction is pathStart is greater than pathEnd
		int sign = new int(); //negative or positive value
		if (pathStart > pathEnd) {
			sign = -1;
		} else {
			sign = 1;
		}
		//assign values from nodes to path
		for (int i = 0; i < path.Length; i++) {
			path[i] = nodes[pathStart+(sign*i)];
		}
	}

	void ChooseCameraPath (int pathStart, int pathEnd) {
		cameraPath = new Vector3[Mathf.Abs(pathEnd-pathStart)+1];
		//make the path move in the opposite direction is pathStart is greater than pathEnd
		int sign = new int(); //negative or positive value
		if (pathStart > pathEnd) {
			sign = -1;
		} else {
			sign = 1;
		}
		//assign values from nodes to path
		for (int i = 0; i < cameraPath.Length; i++) {
			cameraPath[i] = cameraNodes[pathStart+(sign*i)];
		}
	}
}
