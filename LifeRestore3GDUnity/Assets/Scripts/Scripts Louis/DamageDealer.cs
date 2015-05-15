using UnityEngine;
using System.Collections;

public class DamageDealer : MonoBehaviour {

	private Sticky mySticky;
	//la vitesse nécessaire pour péter un fdp
	[SerializeField]
	private float _necessaryVelocity;
//	[SerializeField]
//	public int damageArbre = 1;
//	//	[SerializeField]
//	public int damageCaserne = 1;

	public int damageCaserne;
	public int damageArbre;

	private Color hitObjectColor;

	[SerializeField]
	private GameObject nuageVitesse;

	private Rigidbody myRB;

	// Use this for initialization
	void Start () {
		damageCaserne = 1;
		damageArbre = 1;
		if(gameObject.GetComponent<Sticky>()!=null) mySticky = gameObject.GetComponent<Sticky>();
		if(gameObject.GetComponent<Rigidbody>() != null) myRB = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if(myRB !=null){
			if(mySticky!=null){
				if(mySticky._Velocity >=_necessaryVelocity){
					if(nuageVitesse != null){
						GameObject nuageFeedback = Instantiate(nuageVitesse, gameObject.transform.position, Quaternion.identity) as GameObject;
					}
				}
			}
		}
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.GetComponent<Sticky>()!=null){

			Sticky stickyCollided = col.gameObject.GetComponent<Sticky>();
			//si l'objet collidé est lié par qqch ou si je le suis ou si lié depuis peu
			//that lign below won't do, need to add something like "linked in the last few seconds"
			if(stickyCollided.v_numberOfLinks!=0 || mySticky!=null && mySticky.v_numberOfLinks!=0|| mySticky!=null && mySticky.wasLinkedNotLongAgo==true || mySticky==null){
				if(stickyCollided._Velocity >= _necessaryVelocity || mySticky!=null && mySticky._Velocity >=_necessaryVelocity ){
					//différencier la valeur de ce takeDamage, en fonction de tags
					if(col.gameObject.tag.Equals("Arbre")){
//						StartCoroutine("VisualDamage", col.gameObject);
//						VisualDamageFeedback(col.gameObject);
//						col.gameObject.GetComponent<ObjectStats>().VisualDamage
						col.gameObject.GetComponent<ObjectStats>().TakeDamage(damageArbre);

					}

					if(col.gameObject.tag.Equals("Unlinkable") || col.gameObject.tag.Equals("CaserneKO")){
						col.gameObject.GetComponent<CaserneStats>().TakeDamage(damageCaserne);

					}

				
				}
			}
		}

		if(col.gameObject.tag.Equals("Player")){
			col.gameObject.GetComponent<Player_Status>().TakeDamage();
		}
	}

//	void VisualDamageFeedback(GameObject hitGameObject){
//		StartCoroutine("VisualDamage", hitGameObject);
//	}
//
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
}
