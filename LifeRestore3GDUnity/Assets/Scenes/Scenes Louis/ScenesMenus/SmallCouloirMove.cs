using UnityEngine;
using System.Collections;

public class SmallCouloirMove : MonoBehaviour {

	public Vector3[] nodes; //master list of all nodes, in order
	public Vector3[] path; //the path that you will use for iTween
	
	private iTweenPath myItweenPath;

	public GameObject pathHolder;

	// Use this for initialization
	void Start () {
		myItweenPath = pathHolder.GetComponent<iTweenPath>();
		nodes = new Vector3[myItweenPath.nodes.Count];
		for (int i = 0; i < myItweenPath.nodes.Count; i++) {
			nodes[i] = myItweenPath.nodes[i];
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CouloirMove(int i){
		if(i == 0){
			ChoosePath(0, 4);
			iTween.MoveTo(gameObject, iTween.Hash("path", path, "orienttopath", true, "lookahead", 0.5f, "time", 1.5f, "easetype", iTween.EaseType.easeInOutSine));
		}else if(i==1){
			ChoosePath(4, 0);
			iTween.MoveTo(gameObject, iTween.Hash("path", path, "orienttopath", true, "lookahead", 0.5f, "time", 1.5f, "easetype", iTween.EaseType.easeInOutSine));
		}
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
}
