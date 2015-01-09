using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class ChangeNode : MonoBehaviour {

	bool playerIndexSet = false;
	public PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;

	public GameObject v_attackNode, v_sizeNode, v_colorNode;

	[HideInInspector]
	public int v_selectedNode;
	public GameObject v_selectedNodeObject;

	// Use this for initialization
	void Start () {
		//1=attaque
		//2=size
		//3=color
		v_selectedNode=1;
		v_selectedNodeObject=v_attackNode;
	}
	
	// Update is called once per frame
	void Update () {
		prevState = state;
		state = GamePad.GetState(playerIndex);

		if(prevState.Triggers.Right== 0 && state.Triggers.Right != 0){
			ChangeRight();
		}
		if(prevState.Triggers.Left== 0 && state.Triggers.Left != 0){
			ChangeLeft();
		}

		if(v_selectedNode==4){
			v_selectedNode=1;
		}

		if(v_selectedNode==0){
			v_selectedNode=3;
		}

		if(v_selectedNode==1){
			v_selectedNodeObject=v_attackNode;

		}

		if(v_selectedNode==2){
			v_selectedNodeObject=v_sizeNode;

		}

		if(v_selectedNode==3){
			v_selectedNodeObject=v_colorNode;

		}
		//Debug.Log("selected node is "+v_selectedNode);
	}

	void ChangeRight(){

		//gameObject.transform.rotation= Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
//		gameObject.transform.rotation = Quaternion.Euler(new Vector3(120,0,0));
		gameObject.transform.Rotate(Vector3.up, 120f);
		v_selectedNode+=1;
	}

	 void ChangeLeft(){
		//gameObject.transform.rotation= Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
		//gameObject.transform.localRotation= Quaternion.Euler(new Vector3(-120,0,0));
		gameObject.transform.Rotate(Vector3.up, -120f);
		v_selectedNode-=1;


	}
}
