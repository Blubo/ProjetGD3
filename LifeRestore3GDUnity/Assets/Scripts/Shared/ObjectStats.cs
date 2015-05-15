using UnityEngine;
using System.Collections;

public class ObjectStats : MonoBehaviour {

	[SerializeField]
	protected float v_itemHP;
	
	[HideInInspector]
	public Block_SpawnCollectible myCollecSpwnr;
	private Sticky mySticky;
	protected Rigidbody myRB;

	[SerializeField]
	private GameObject explosionVisuel;

	// Use this for initialization
	void Start () {
		if(gameObject.GetComponent<Rigidbody>()!=null) myRB = gameObject.GetComponent<Rigidbody>();
		if(gameObject.GetComponent<Sticky>()!=null) mySticky = gameObject.GetComponent<Sticky>();
		if(gameObject.GetComponent<Block_SpawnCollectible>()!=null)	myCollecSpwnr = gameObject.GetComponent<Block_SpawnCollectible>();
	}
	
	// Update is called once per frame
	public virtual void Update () {

		if(v_itemHP <= 0f){
			if(myCollecSpwnr!=null){
				myCollecSpwnr.SpawnCollectible();
			}
			if(explosionVisuel!=null){
				GameObject explosion = Instantiate(explosionVisuel, gameObject.transform.position, Quaternion.identity) as GameObject;
			}

			Destroy(gameObject);
		}
	}

	public virtual void TakeDamage(float damage){
		VisualDamageFeedback();
//		StartCoroutine("VisualDamage");
		v_itemHP -= damage;
	}

//	public void VisualDamageFeedback(GameObject hitGameObject){
//		StartCoroutine("VisualDamage", hitGameObject);
//	}
	
//	public IEnumerator VisualDamage(GameObject hitGameObject){
//		for (int i = 0; i < 2; i++){
//			Renderer[] renderer = hitGameObject.transform.Find("Visuel").GetComponentsInChildren<MeshRenderer>();
//			for (int j = 0; j < renderer.Length; j++) {
//				Color[] hitColors = new Color[renderer.Length];
//				hitColors[j] = renderer[j].material.color;
//				
//				foreach (Renderer rend in renderer){
//					rend.material.color = Color.white;
//				}
//				yield return new WaitForSeconds(0.0f);
//				
//				foreach (Renderer rend in renderer){
//					rend.material.color = hitColors[j];
//				}
//			}
//		}
//	}

	public void VisualDamageFeedback(){
		StartCoroutine("VisualDamage", gameObject);
	}

	public IEnumerator VisualDamage(){
		Renderer[] renderer = gameObject.transform.Find("Visuel").GetComponentsInChildren<MeshRenderer>();
		Color[] hitColors = new Color[renderer.Length];
		for (int l = 0; l < renderer.Length; l++) {

			hitColors[l] = renderer[l].material.color;
		}
		for (int i = 0; i < 2; i++){
			for (int j = 0; j < renderer.Length; j++) {
				foreach (Renderer rend in renderer){
					rend.material.color = Color.white;
				}
				yield return new WaitForSeconds(0.0f);
				
				foreach (Renderer rend in renderer){
					rend.material.color = hitColors[j];
				}
			}
		}
	}
}
