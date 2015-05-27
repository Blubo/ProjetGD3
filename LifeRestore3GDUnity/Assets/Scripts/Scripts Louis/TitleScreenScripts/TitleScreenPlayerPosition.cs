using UnityEngine;
using System.Collections;

//on pose ce script sur les players
//on stocke dedans la salle dans laquelle il se trouve
//on a la fonction de téléport

public class TitleScreenPlayerPosition : MonoBehaviour {
	[SerializeField]
	public int whichRoomImIn;
	[HideInInspector]
	public bool allowInputs = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}

	public void TeleportPlayer(Vector3 travel){
		gameObject.transform.position+=travel;
	}

	public void ChangePlayerState(bool state){
		gameObject.SetActive(state);
	}
}
