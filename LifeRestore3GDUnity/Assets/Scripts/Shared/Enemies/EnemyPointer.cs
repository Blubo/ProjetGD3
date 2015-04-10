using UnityEngine;
using System.Collections;

public class EnemyPointer : MonoBehaviour {

	//allows us to add the enemy to the counter, once and only once
	private bool addedToCounter = false;

	[Tooltip("This enemy EnemiesManager")]
	public GameObject MyEnemiesManager;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.GetComponent<Renderer>().isVisible == true && addedToCounter == false){

			MyEnemiesManager.GetComponent<EnemiesManager>().enemiesCount+=1;
			addedToCounter=true;
		}
	}
}
