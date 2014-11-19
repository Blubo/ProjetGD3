using UnityEngine;
using System.Collections;
using XInputDotNetPure;


public class MovementScript : MonoBehaviour {

	bool playerIndexSet = false;
	PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;

	private float _movementSpeed;

	// Use this for initialization
	void Start () {
		_movementSpeed=10f;
	}
	
	// Update is called once per frame
	void Update () {
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

		Vector3 movement = new Vector3(state.ThumbSticks.Left.X * _movementSpeed * Time.deltaTime, 0.0f, state.ThumbSticks.Left.Y * _movementSpeed * Time.deltaTime );
		transform.localPosition += movement;
		
		transform.eulerAngles=new Vector3(0,0,transform.eulerAngles.z);

	}
	
}
