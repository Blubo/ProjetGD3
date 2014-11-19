using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicIdoleSript : MonoBehaviour {

	public List<GameObject> v_playersList; //tous les joueurs de la scene, à rentrer manuellement
	private List<GameObject> _playersInRange=new List<GameObject>(); //les joueurs à distance de l'idole à chaque frame
	private Vector3 _barycenter, _tempBarycenter; //barycentre
	public float v_detectionSize; //taille de détection de l'idole

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		//premieres méthodes alternatives pour couper les liens
		//ya moyen de couper le lien d'un gars qui n'est meme pas à portée de l'idole, pour l'instant!
		if(Input.GetKey(KeyCode.A)){
			v_playersList[0].GetComponent<PlayerState>().v_isLinked = false;
		}

		//ajoute les joueurs à distance de l'idole dans la liste pour calculer le barycentre
		for(int i=0; i<v_playersList.Count; i++){
			float distance = Vector3.Distance(transform.position, v_playersList[i].transform.position);
			if(distance<v_detectionSize && v_playersList[i].GetComponent<PlayerState>().v_isLinked == true){
				_playersInRange.Add(v_playersList[i]);
			}
		}

		//calcul du barycentre
		for(int i=0; i<_playersInRange.Count; i++){
			_tempBarycenter += _playersInRange[i].transform.position;
		}
		_tempBarycenter = _tempBarycenter/_playersInRange.Count;
		_barycenter=_tempBarycenter;
		_tempBarycenter=new Vector3(0,0,0);

		//reset le tableau
		_playersInRange.Clear();

		//positionne l'idole sur le barycentre
		//gameObject.transform.position=_barycenter;
		gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, _barycenter, 0.1f);

	}


	//vision de la taille de détection de l'idole
	void OnDrawGizmos(){
		Gizmos.color = new Color(255,255,0,0.1f);
		Gizmos.DrawSphere(transform.position, 10);
	}

}
