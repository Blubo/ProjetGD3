using UnityEngine;
using System.Collections;

public class RailChangeConstraint : MonoBehaviour {

	[SerializeField]
	private GameObject[] itemOnThisRail;

	private ItemOnRails[] itemOnRails;

	//si false, alors c'est un verticalTrigger
	public bool horizontalTrigger;

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

	void OnTriggerExit(Collider col){
		for (int i = 0; i < itemOnThisRail.Length; i++) {

			if(col.gameObject == itemOnThisRail[i]){
				if(horizontalTrigger == true){
					if(itemOnRails[i].verticalPassport==true){
						itemOnRails[i].verticalPassport = false;
						itemOnRails[i].verticalOnly=false;
						itemOnRails[i].horizontalOnly=true;
					}
					itemOnRails[i].horizontalPassport = true;
				}else{
					if(itemOnRails[i].horizontalPassport==true){
						itemOnRails[i].horizontalPassport = false;
						itemOnRails[i].horizontalOnly=false;
						itemOnRails[i].verticalOnly=true;
					}
					itemOnRails[i].verticalPassport = true;
				}
			}
		}
	}

//	[SerializeField]
//	private GameObject itemOnThisRail;
//	
//	private ItemOnRails itemOnRails;
//	
//	//si false, alors c'est un verticalTrigger
//	public bool horizontalTrigger;
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
//	void OnTriggerExit(Collider col){
//		if(col.gameObject == itemOnThisRail){
//			if(horizontalTrigger == true){
//				if(itemOnRails.verticalPassport==true){
//					itemOnRails.verticalPassport = false;
//					itemOnRails.verticalOnly=false;
//					itemOnRails.horizontalOnly=true;
//				}
//				itemOnRails.horizontalPassport = true;
//			}else{
//				if(itemOnRails.horizontalPassport==true){
//					itemOnRails.horizontalPassport = false;
//					itemOnRails.horizontalOnly=false;
//					itemOnRails.verticalOnly=true;
//				}
//				itemOnRails.verticalPassport = true;
//			}
//		}
//	}
}
