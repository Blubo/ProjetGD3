using UnityEngine;
using System.Collections;

public class ModuleSceneManager : MonoBehaviour {

	void Awake(){
		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyDown(KeyCode.Space)){
			if(Application.loadedLevel == 0){
				Application.LoadLevel(1);
			}
			if(Application.loadedLevel == 1){
				Application.LoadLevel(2);
			}
			if(Application.loadedLevel == 2){
				Application.LoadLevel(0);
			}
		}

	}
}
