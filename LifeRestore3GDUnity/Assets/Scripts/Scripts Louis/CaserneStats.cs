using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CaserneStats : ObjectStats {

	[Tooltip("Caserne will be fronde when its hp go below this value")]
	[SerializeField]
	private float HPcaserneProducing;

	[SerializeField]
	private bool isProducing;

	public List<GameObject>epines;

	public GameObject reticule;

	// Use this for initialization
	void Start () {
//		if(gameObject.GetComponent<Block_SpawnCollectible>()!=null)	myCollecSpwnr = gameObject.GetComponent<Block_SpawnCollectible>();

	}

	public override void Update(){

		//on detruit ici les objets quand ils sont trop abimés
		//de base un destroy
		//			if(caserne == true){
		//				mySticky.fronde = true;
		//				gameObject.tag = "Untagged";
		////				gameObject.transform.Find("NewCaserne").gameObject.GetComponent<Renderer>().material.color = Color.grey;
		//				myRB.constraints = RigidbodyConstraints.None;
		//				if(v_itemHP<=-v_casernevideHP){
		//					Destroy(gameObject);
		//				}
		//				return;
		//			}else{

		if(HPcaserneProducing<=0 ){
			if(isProducing=true){
				isProducing = false;
				MakeFronde();
			}
		}

		if(v_itemHP <= 0f){
			if(myCollecSpwnr!=null){
				myCollecSpwnr.SpawnCollectible();
			}
			Destroy(gameObject);
		}
	}

	void LoseEpine(){
		int random = Random.Range(0, epines.Count);
		if(epines[random]!=null){
			epines[random].AddComponent<Rigidbody>();
			BoxCollider epineCollider = epines[random].AddComponent<BoxCollider>();
			epineCollider.size = new Vector3(0.36f, 0.38f, 0.5f);
			epineCollider.center = new Vector3(0,0,0.25f);
			epines.RemoveAt(random);
		}
	}

	void MakeFronde(){
		gameObject.tag = "CaserneKO";
		gameObject.GetComponent<Rigidbody>().isKinematic = false;
	}

	public override void TakeDamage(float damage){
		if(HPcaserneProducing<=0){
			base.TakeDamage(damage);
		}

		if(HPcaserneProducing>0){
			HPcaserneProducing -= damage;
			LoseEpine();
		}
	}
}
