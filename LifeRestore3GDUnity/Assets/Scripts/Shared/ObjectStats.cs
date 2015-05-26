using UnityEngine;
using System.Collections;

public class ObjectStats : MonoBehaviour {

	[SerializeField]
	private bool brasero;

	[SerializeField]
	protected float v_itemHP;
	protected float maxHPproducing;
	
	[HideInInspector]
	public Block_SpawnCollectible myCollecSpwnr;
	protected Sticky mySticky;
	protected Rigidbody myRB;

	[SerializeField]
	protected GameObject explosionVisuel;

	private bool blinking;
	protected Color[] _hitColors;

	private bool tookDamage;

	protected Renderer[] myChildrenRenderers;

	[SerializeField]
	protected GameObject hitAnimation;

	// Use this for initialization
	public virtual void Start () {
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
			if(gameObject.tag.Equals("Arbre") == true && brasero ==false) Camera.main.GetComponent<SoundManagerHeritTest>().PlaySoundOneShot("Arbre destruction");
			if(brasero == true) Camera.main.GetComponent<SoundManagerHeritTest>().PlaySoundOneShot("Brasero destruction");
			Destroy(gameObject);
		}
	}

	public virtual void TakeDamage(float damage){
		if(hitAnimation != null) Instantiate(hitAnimation, gameObject.transform.position, Quaternion.identity);
		VisualDamageFeedback();
		v_itemHP -= damage;
	}
	
	public void VisualDamageFeedback(){
		StartCoroutine("VisualDamage", gameObject);
	}

	public IEnumerator VisualDamage(){
		foreach (Renderer rend in myChildrenRenderers){
			if(rend !=null) rend.material.color = Color.red;
		}

		yield return new WaitForSeconds(0.1f);

		for (int j = 0; j < myChildrenRenderers.Length; j++){
			myChildrenRenderers[j].material.color = _hitColors[j];
		}
		yield return new WaitForSeconds(0.1f);
	}
}
