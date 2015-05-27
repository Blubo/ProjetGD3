using UnityEngine;
using System.Collections;

public class ActivatedMusicChange : MonoBehaviour {

	[SerializeField]
	private float paramFMODmusic;
	[SerializeField]
	private int myStep;
	private MusicManagerFMOD myMusicManager;

	// Use this for initialization
	void Start () {
		if(Camera.main.GetComponent<MusicManagerFMOD>()!= null) myMusicManager = Camera.main.GetComponent<MusicManagerFMOD>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Activated(){
		if(myStep>myMusicManager.step){
			myMusicManager.step = myStep;
			myMusicManager.ChangeParamMainMusic(paramFMODmusic);
		}
	}

	void Deactivated(){}
}
