using UnityEngine;
using System.Collections;

public class RailWaypoint : MonoBehaviour {

	[SerializeField]
	private GameObject[] itemOnThisRail;

	private ItemOnRails[] itemOnRails;

	// Use this for initialization
	void Start () {
		itemOnRails = new ItemOnRails[itemOnThisRail.Length];

		for (int i = 0; i < itemOnThisRail.Length; i++) {
			itemOnRails[i] = itemOnThisRail[i].GetComponent<ItemOnRails>();

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		for (int i = 0; i < itemOnThisRail.Length; i++) {
			if(col.gameObject == itemOnThisRail[i]){
				if(itemOnRails[i].verticalPassport == true){
					//				itemOnRails.verticalOnly = true;
					itemOnRails[i].RemoveConstraints();
				}
				
				if(itemOnRails[i].horizontalPassport == true){
					//				itemOnRails.horizontalOnly = true;
					itemOnRails[i].RemoveConstraints();
				}
			}
		}
	}

//	[SerializeField]
//	private GameObject itemOnThisRail;
//	
//	private ItemOnRails itemOnRails;
//	
//	// Use this for initialization
//	void Start () {
//		itemOnRails = itemOnThisRail.GetComponent<ItemOnRails>();
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		
//	}
//	
//	void OnTriggerEnter(Collider col){
//		if(col.gameObject == itemOnThisRail){
//			if(itemOnRails.verticalPassport == true){
//				//				itemOnRails.verticalOnly = true;
//				itemOnRails.RemoveConstraints();
//			}
//			
//			if(itemOnRails.horizontalPassport == true){
//				//				itemOnRails.horizontalOnly = true;
//				itemOnRails.RemoveConstraints();
//			}
//		}
//	}

}
