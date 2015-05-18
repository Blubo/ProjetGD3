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

	private bool blinking;
	private Color[] _hitColors;

	private bool tookDamage;

	private Renderer[] myChildrenRenderers;

	// Use this for initialization
	void Start () {
		myChildrenRenderers = gameObject.transform.Find("Visuel").GetComponentsInChildren<MeshRenderer>();
		_hitColors = new Color[myChildrenRenderers.Length];
		for (int l = 0; l < myChildrenRenderers.Length; l++) {
			_hitColors[l] = myChildrenRenderers[l].material.color;
		}
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
		v_itemHP -= damage;
	}
	
	public void VisualDamageFeedback(){
		StartCoroutine("VisualDamage", gameObject);
	}

	public IEnumerator VisualDamage(){
		for (int j = 0; j < myChildrenRenderers.Length; j++) {
//			foreach (Renderer rend in myChildrenRenderers){
//				rend.material.color = Color.white;
//			}
//			yield return new WaitForSeconds(0.01f);
//			
//			foreach (Renderer rend in myChildrenRenderers){
//				myChildrenRenderers[j].material.color= _hitColors[j];
//				//rend.material.color = _hitColors[j];
//			}

			foreach (Renderer rend in myChildrenRenderers){
//				myChildrenRenderers[j].material.color = Color.white;
				rend.material.color = Color.white;

			}
			yield return new WaitForSeconds(0.1f);
			
//			foreach (Renderer rend in myChildrenRenderers){
				myChildrenRenderers[j].material.color= _hitColors[j];
//				rend.material.color = _hitColors[j];
//				rend[j].material.color = _hitColors[j];

//			}

		}
	}
}
