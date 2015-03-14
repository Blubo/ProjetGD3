using UnityEngine;
using System.Collections;

public class CameraFocus : MonoBehaviour {
	
	public GameObject v_player1, v_player2, v_player3, v_player4;
	public float v_limitEcartement;
	private float _playerEcartement;
	public Camera v_gameCamera;
	private Vector3 _barycenter, _tempBarycenter;
	
	// Use this for initialization
	void Start () {
//		v_player1 = GameObject.FindWithTag("Player1");
//		v_player2 = GameObject.FindWithTag("Player2");
//		v_player3 = GameObject.FindWithTag("Player3");
//		v_player4 = GameObject.FindWithTag("Player4");

		_tempBarycenter = v_player1.transform.position+v_player2.transform.position+v_player3.transform.position+v_player4.transform.position;
		_tempBarycenter = _tempBarycenter/4;
		_barycenter=_tempBarycenter;
		_tempBarycenter=new Vector3(0,0,0);
		gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, _barycenter, 0.1f);

	}
	
	// Update is called once per frame
	void Update () {

		//calcul du barycentre
		_tempBarycenter = v_player1.transform.position+v_player2.transform.position+v_player3.transform.position+v_player4.transform.position;
		
		_tempBarycenter = _tempBarycenter/4;
		_barycenter=_tempBarycenter;
		_tempBarycenter=new Vector3(0,0,0);
		
		//positionne le focus sur le barycentre
		//gameObject.transform.position=_barycenter;
		gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, _barycenter, 0.1f);

	}
}