using UnityEngine;
using System.Collections;

public class AlphaManager : MonoBehaviour {

	[HideInInspector]
	public bool featherIsTaken = false;

	[HideInInspector]
	public GameObject posessor;

	public int scoreBonusFlat;

	void Awake(){
//		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(posessor != null){
			GetComponent<Collider>().isTrigger =true;
			gameObject.transform.position = posessor.transform.position;
			gameObject.transform.parent = posessor.transform;
		}
	}
}
