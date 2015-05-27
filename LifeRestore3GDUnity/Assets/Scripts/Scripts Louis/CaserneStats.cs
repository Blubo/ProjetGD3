using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CaserneStats : ObjectStats {

	[Tooltip("Caserne will be fronde when its hp go below this value")]
	[SerializeField]
	private float HPcaserneProducing;

	[SerializeField]
	public bool isProducing;

	public List<GameObject>epines;
	private List<GameObject>children;
	public GameObject reticule;

	// Use this for initialization
	void Start () {
		maxHPproducing = HPcaserneProducing;

		children = new List<GameObject>();
		foreach(Transform child in gameObject.transform.Find("Visuel")){
			children.Add(child.gameObject);
		}
		myChildrenRenderers = new Renderer[children.Count];
		for (int i = 0; i < children.Count; i++) {
			myChildrenRenderers[i] = children[i].GetComponent<MeshRenderer>();
		}

		_hitColors = new Color[myChildrenRenderers.Length];
		for (int l = 0; l < myChildrenRenderers.Length; l++) {
			_hitColors[l] = myChildrenRenderers[l].material.color;
		}

		if(gameObject.GetComponent<Rigidbody>()!=null) myRB = gameObject.GetComponent<Rigidbody>();
		if(gameObject.GetComponent<Sticky>()!=null) mySticky = gameObject.GetComponent<Sticky>();
		if(gameObject.GetComponent<Block_SpawnCollectible>()!=null)	myCollecSpwnr = gameObject.GetComponent<Block_SpawnCollectible>();
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
			if(isProducing==true){
				isProducing = false;
//				Debug.Log("huh");
				MakeFronde();
	
				if(myCollecSpwnr!=null){
					myCollecSpwnr.SpawnCollectible();
				}
			}
		}

		if(v_itemHP <= 0f){
			if(explosionVisuel!=null){
				GameObject explosion = Instantiate(explosionVisuel, gameObject.transform.position, Quaternion.identity) as GameObject;
			}
//			Camera.main.GetComponent<SoundManagerHeritTest>().PlaySoundOneShot("Caserne destruction");

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
//		gameObject.transform.Find("Visuel/Hedra012").GetComponent<MeshRenderer>().material.color = Color.grey;
//		gameObject.transform.Find("Visuel/Box045").GetComponent<MeshRenderer>().material.color = new Color32(199, 199, 136, 255);
//		gameObject.transform.Find("Visuel/Box046").GetComponent<MeshRenderer>().material.color = new Color32(199, 199, 136, 255);
//		gameObject.transform.Find("Visuel/Box047").GetComponent<MeshRenderer>().material.color = new Color32(199, 199, 136, 255);
//		gameObject.transform.Find("Visuel/Plane013").GetComponent<MeshRenderer>().material.color = Color.black;
//
//		foreach(Transform child in gameObject.transform.Find("Visuel")){
//			children.Add(child.gameObject);
//		}
//		myChildrenRenderers = new Renderer[children.Count];
//		for (int i = 0; i < children.Count; i++) {
//			myChildrenRenderers[i] = children[i].GetComponent<MeshRenderer>();
//		}
//
//		_hitColors = new Color[myChildrenRenderers.Length];
//		for (int l = 0; l < myChildrenRenderers.Length; l++) {
//			_hitColors[l] = myChildrenRenderers[l].material.color;
//		}

		//before there was this
		gameObject.tag = "CaserneKO";
		gameObject.GetComponent<Rigidbody>().isKinematic = false;
	}

	public override void TakeDamage(float damage){
		if(HPcaserneProducing<=0){

			base.TakeDamage(damage);
//			v_itemHP -= damage;

		}

		if(HPcaserneProducing>0){
			if(hitAnimation != null) Instantiate(hitAnimation, gameObject.transform.position, Quaternion.identity);

			base.VisualDamageFeedback();
			HPcaserneProducing -= damage;

			for (int i = 0; i < 8*(damage/maxHPproducing); i++) {
				LoseEpine();
			}
		}
	}
}
