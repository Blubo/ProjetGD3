using UnityEngine;
using System.Collections;

public class CameraFocus : MonoBehaviour {
	
	public GameObject v_player1, v_player2, v_player3, v_player4;
	public float v_limitEcartement;
	private float _playerEcartement;
	private Vector3 _barycenter, _tempBarycenter;

	public int numberOfPlayers;
	public float myTime;

	// Use this for initialization
	void Start () {
//		v_player1 = GameObject.FindWithTag("Player1");
//		v_player2 = GameObject.FindWithTag("Player2");
//		v_player3 = GameObject.FindWithTag("Player3");
//		v_player4 = GameObject.FindWithTag("Player4");

		if(numberOfPlayers==3){
			_tempBarycenter = v_player1.transform.position+v_player2.transform.position+v_player3.transform.position;
			_tempBarycenter = _tempBarycenter/3;
			_barycenter=_tempBarycenter;
			_tempBarycenter=new Vector3(0,0,0);
			StartCoroutine(MoveObject(gameObject.transform, gameObject.transform.position, _barycenter, myTime));
//			gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, _barycenter, 0.1f);

		}else if(numberOfPlayers==4){

			_tempBarycenter = v_player1.transform.position+v_player2.transform.position+v_player3.transform.position+v_player4.transform.position;
			_tempBarycenter = _tempBarycenter/4;
			_barycenter=_tempBarycenter;
			_tempBarycenter=new Vector3(0,0,0);
			StartCoroutine(MoveObject(gameObject.transform, gameObject.transform.position, _barycenter, myTime));

//			gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, _barycenter, 0.1f);
		}
	}
	
	// Update is called once per frame
	void Update () {

		if(numberOfPlayers==3){
			//calcul du barycentre
			_tempBarycenter = v_player1.transform.position+v_player2.transform.position+v_player3.transform.position;
			
			_tempBarycenter = _tempBarycenter/3;
			_barycenter=_tempBarycenter;
			_tempBarycenter=new Vector3(0,0,0);
			
			//positionne le focus sur le barycentre
			//gameObject.transform.position=_barycenter;
			//ATTENTION, FAUT PTET METTRE UN T.DELTATIME
			StartCoroutine(MoveObject(gameObject.transform, gameObject.transform.position, _barycenter, myTime));

//			gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, _barycenter, 0.1f);
		
		}else if(numberOfPlayers==4){
			//calcul du barycentre
			_tempBarycenter = v_player1.transform.position+v_player2.transform.position+v_player3.transform.position+v_player4.transform.position;
			
			_tempBarycenter = _tempBarycenter/4;
			_barycenter=_tempBarycenter;
			_tempBarycenter=new Vector3(0,0,0);
			
			//positionne le focus sur le barycentre
			//gameObject.transform.position=_barycenter;
			//ATTENTION, FAUT PTET METTRE UN T.DELTATIME
			StartCoroutine(MoveObject(gameObject.transform, gameObject.transform.position, _barycenter, myTime));

//			gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, _barycenter, 0.1f);
		}
	}

	IEnumerator MoveObject (Transform thisTransform, Vector3 startPos, Vector3 endPos, float time){
		float i = 0;
		float rate = 1/time;
		while (i < 1) {
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp(startPos, endPos, i);
			yield return null; 
		}
	}
}