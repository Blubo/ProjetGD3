using UnityEngine;
using System.Collections;

public class ActivatedSpawnByRéceptacle : MonoBehaviour {

	[Tooltip("This spawner is linked to this réceptacle")]
	[SerializeField]
	private GameObject myLinkedReceptacle;

	[Tooltip("Item to spawn")]
	[SerializeField]
	private GameObject itemToSpawn;
	private bool spawn;

	[Tooltip("Infinite items or limited number")]
	[SerializeField]
	private bool limitedPool;

	[Tooltip("Number of items you'll be able to spawn")]
	[SerializeField]
	private int itemsPoolSize;

	//the number of times this has been activated
	private int activatedCounter;

	// Use this for initialization
	void Start () {
		activatedCounter = 0;
		spawn = false;
	}
	
	// Update is called once per frame
	void Update () {
		//ne créer qu'un item par activation
		if(spawn==true){
			//si on a un nombre limité qu'on peut créer
			if(limitedPool==true){
				//si on a pas crée le nombre max d'items
				if(activatedCounter<=itemsPoolSize){
					spawn = false;
					GameObject spawnedItem = Instantiate(itemToSpawn, gameObject.transform.position, transform.rotation) as GameObject;
				}
			}else{
				spawn = false;
				GameObject spawnedItem = Instantiate(itemToSpawn, gameObject.transform.position, transform.rotation) as GameObject;
			}
		}

		if(activatedCounter>=itemsPoolSize){
			if(myLinkedReceptacle.GetComponent<ReceptacleKey>().stillUsable==true){
				myLinkedReceptacle.GetComponent<ReceptacleKey>().stillUsable=false;
			}
		}
	}

	void Activated(){
		activatedCounter+=1;
		spawn=true;
	}
}
