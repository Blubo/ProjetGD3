using UnityEngine;
using System.Collections;

public class ActivatedSpawnCollectibles : MonoBehaviour {

	[Tooltip("Item to spawn")]
	[SerializeField]
	private GameObject itemToSpawn;
	private bool spawn;
	
	[Tooltip("Infinite items or limited number")]
	[SerializeField]
	private bool limitedPool;
	
	[Tooltip("Number of items you'll be able to spawn")]
	[SerializeField]
	private int maxItemsPoolSize, minItemsPoolSize;

	private int itemsPoolSize;
	
	//the number of times this has been activated
	private int activatedCounter;
	
	[Tooltip("Will the objects spawned rise from the ground")]
	[SerializeField]
	private bool willThisRiseFromTheGround;
	
	[Tooltip("How high compared to this item will the spawned rise?")]
	[SerializeField]
	private float altitude;

	// Use this for initialization
	void Start () {
		itemsPoolSize = Random.Range(minItemsPoolSize, maxItemsPoolSize);
	}
	
	// Update is called once per frame
	void Update () {
		if(spawn==true){
			//si on a un nombre limité qu'on peut créer
			if(limitedPool==true){
				//si on a pas crée le nombre max d'items
				for (int i = 0; i < itemsPoolSize; i++) {
					GameObject spawnedItem = Instantiate(itemToSpawn, new Vector3(gameObject.transform.position.x+Random.Range(-15.0f, 15.0f), 0.72f, gameObject.transform.position.z+Random.Range(-15.0f, 15.0f)), transform.rotation) as GameObject;

				}
//				if(activatedCounter<=itemsPoolSize){
				spawn = false;
//					if(willThisRiseFromTheGround==true){
//
//						
//					}
//				}
			}else{
				spawn = false;
				GameObject spawnedItem = Instantiate(itemToSpawn, gameObject.transform.position, transform.rotation) as GameObject;
//				if(willThisRiseFromTheGround==true){
//
//					
//				}
			}
		}

	}

	void Activated(){
		activatedCounter+=1;
		spawn = true;
	}

	void Deactivated(){
		
	}
}
