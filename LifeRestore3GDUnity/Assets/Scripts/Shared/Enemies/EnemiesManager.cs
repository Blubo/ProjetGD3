using UnityEngine;
using System.Collections;

//each room has an EnemiesManager
//no need to make it a singleton
//if EnemiesManagerCount = 0, do consequence on specfied object

public class EnemiesManager : MonoBehaviour {

	[HideInInspector]
	public int enemiesCount;

	//this for exterior consequence
	[SerializeField]
	[Tooltip("Insert gameObjects affected by this counter")]
	private GameObject activatedItem;
	
	[Tooltip("How much time between player arrival and this object acts")]
	[SerializeField]
	private float freezeTimer;

	private bool activatedTimer = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(activatedTimer==true){
			freezeTimer-=Time.deltaTime;
		}
		if(enemiesCount<=0 && freezeTimer <= 0f){
			activatedItem.SendMessage("Activated");
		}
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag.Equals("Idole")){
			activatedTimer=true;
		}
	}
}
