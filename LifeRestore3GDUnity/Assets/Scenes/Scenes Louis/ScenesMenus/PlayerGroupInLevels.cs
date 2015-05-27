using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PlayerGroupInLevels : MonoBehaviour {

	bool playerIndexSet = false;
	public PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;

	public SplineController splineController;
	public GameObject path1to2, path2to1, path2to3, path3to2, path3to4, path4to3;
	public Transform waypoint1, waypoint2, waypoint3, waypoint4;

	public bool inputsAllowed;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		if(inputsAllowed == true){

		}

		//SUR PREMIER WP
		if(gameObject.transform.position == waypoint1.transform.position){
			//IF LEFT
			if(prevState.ThumbSticks.Left.X==0 && state.ThumbSticks.Left.X>0){
//				splineController.SplineRoot = path1to2;
//				splineController.SplineRoot = GameObject.Find("path1to2");

				splineController.AutoStart = true;
			}
//			else if(prevState.ThumbSticks.Left.X==0 && state.ThumbSticks.Left.X<0){
//				splineController.SplineRoot = path2to1;
//				splineController.AutoStart = true;
//			}

			//SUR DEUXIEME WP
		}else if(gameObject.transform.position == waypoint2.transform.position){
			//IF LEFT
			if(prevState.ThumbSticks.Left.X==0 && state.ThumbSticks.Left.X>0){
				splineController.SplineRoot = path2to3;
				splineController.AutoStart = true;
			}
			//IF RIGHT
			else if(prevState.ThumbSticks.Left.X==0 && state.ThumbSticks.Left.X<0){
				splineController.SplineRoot = path2to1;
				splineController.AutoStart = true;
			}

			//SUR TROISIEME WP
		}else if(gameObject.transform.position == waypoint3.transform.position){


			//SUR 4EME WP
		}else if(gameObject.transform.position == waypoint4.transform.position){
			
		}

		prevState = state;
		state = GamePad.GetState(playerIndex);
	}
}
