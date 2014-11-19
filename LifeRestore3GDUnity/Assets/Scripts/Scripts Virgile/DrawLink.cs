using UnityEngine;
using System.Collections;

public class DrawLink : MonoBehaviour {

	private PlayerState _State;
	public GameObject _Idole;

	// Use this for initialization
	void Start () {
		_State = GetComponent<PlayerState> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(_State.v_isLinked){
			Debug.DrawLine(_Idole.transform.position, transform.position, Color.blue);
		}
	}
}
