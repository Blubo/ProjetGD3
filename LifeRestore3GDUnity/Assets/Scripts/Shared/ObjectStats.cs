using UnityEngine;
using System.Collections;

public class ObjectStats : MonoBehaviour {

	[SerializeField]
	private float v_itemHP;

	[SerializeField]
	private float v_casernevideHP;

	[SerializeField]
	private bool caserne;

	private Block_SpawnCollectible myCollecSpwnr;
	private Sticky mySticky;
	private Rigidbody myRB;

	// Use this for initialization
	void Start () {
		if(gameObject.GetComponent<Rigidbody>()!=null) myRB = gameObject.GetComponent<Rigidbody>();
		if(gameObject.GetComponent<Sticky>()!=null) mySticky = gameObject.GetComponent<Sticky>();
		if(gameObject.GetComponent<Block_SpawnCollectible>()!=null)	myCollecSpwnr = gameObject.GetComponent<Block_SpawnCollectible>();
	}
	
	// Update is called once per frame
	void Update () {

		if(v_itemHP <= 0f){
			if(myCollecSpwnr!=null){
				myCollecSpwnr.SpawnCollectible();
			}

			//on detruit ici les objets quand ils sont trop abimés
			//de base un destroy
			if(caserne == true){
				mySticky.fronde = true;
				gameObject.tag = "Untagged";
//				gameObject.transform.Find("NewCaserne").gameObject.GetComponent<Renderer>().material.color = Color.grey;
				myRB.constraints = RigidbodyConstraints.None;
				if(v_itemHP<=-v_casernevideHP){
					Destroy(gameObject);
				}
				return;
			}else{
				Destroy(gameObject);
			}
		}
	}

	public void TakeDamage(float damage){
		v_itemHP -= damage;
	}
}
