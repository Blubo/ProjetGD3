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

		/*CAN TAKE DAMAGE
		 * idole
		 * joueur
		 * fronde (friable)
		 * interrupteur (friable)
		 * decor
		 * enemy petit vivant
		 * enemy ingenieur (=bombe)
		 * enemiKO (décor/fronde)
		 * caserneKO (décor/fronde)
		 * enemyVivants
		 * bombe
		 * 
		 *CAN INFLICT DAMAGE
		 *idole
		 *joueurs
		 *fronde (friable)
		 *interrupteur (friable)/décor unlinkable
		 *décor
		 *enemy petit vivant
		 *enemi ingénieur
		 *enemiKO
			
		*/



		//si j'ai un sticky
		if(col.gameObject.GetComponent<Sticky>()!=null){
			Sticky stickyCollided = col.gameObject.GetComponent<Sticky>();
			//si l'objet collidé est lié par qqch ou si je le suis ou si lié depuis peu ou si je n'ai pas de sticky (si je suis du décor)
			if(stickyCollided.v_numberOfLinks!=0 || mySticky!=null && mySticky.v_numberOfLinks!=0|| mySticky!=null && mySticky.wasLinkedNotLongAgo==true || mySticky==null){
				//si l'objet collidé va assez vite (selon moi), ou que je vais assez vite (selon moi) dans le cas où je peux bouger 
				if(stickyCollided._Velocity >= _necessaryVelocity || mySticky!=null && mySticky._Velocity >=_necessaryVelocity ){
					//si je cogne un arbre
					if(col.gameObject.tag.Equals("Arbre")){
						col.gameObject.GetComponent<ObjectStats>().TakeDamage(damageArbre);
						Debug.Log("case 1");
					}

					//si je cogne une caserne ou caserne KO
					if(col.gameObject.tag.Equals("Unlinkable") || col.gameObject.tag.Equals("CaserneKO")){
						col.gameObject.GetComponent<CaserneStats>().TakeDamage(damageCaserne);
						Debug.Log("case 2");

					}
				}
			}
		}


		//si j'ai un sticky qui va plus vite que mon necesaryVelocity et que j'ai un rigidbody
		//concretement, si je suis une fronde qui va assez vit (selon moi), ou un interrupteur qui va vite (selon moi) (???)
		//ou un décoar qui va vite
//		if(gameObject.GetComponent<Rigidbody>() != null && mySticky._Velocity >=  _necessaryVelocity && gameObject.GetComponent<Sticky>()!=null){
//			//alors je fais mal aux joueurs
//			if(col.gameObject.tag.Equals("Player")){
//				col.gameObject.GetComponent<Player_Status>().TakeDamage();
//				Debug.Log("case 3");
//
//			}
//			//alors je fais mal au décor
//			if(col.gameObject.tag.Equals("Arbre")){
//				col.gameObject.GetComponent<ObjectStats>().TakeDamage(damageArbre);
//				Debug.Log("case 4");
//
//			}
//		}

		//si je suis un projectile de tourelle
		if(gameObject.GetComponent<TurretProjectile>() != null){
			//alors je fais mal aux joueurs
			if(col.gameObject.tag.Equals("Player")){
				col.gameObject.GetComponent<Player_Status>().TakeDamage();
				Debug.Log("case 5");

			}
			//alors je fais mal au décor
			if(col.gameObject.tag.Equals("Arbre")){
				col.gameObject.GetComponent<ObjectStats>().TakeDamage(damageArbre);
				Debug.Log("case 6");

			}
		}

		//ENEMIES
		if (col.gameObject.tag == "Ennemy"){
			//Faiblar
			if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyA_AI){
				col.gameObject.GetComponent<BasicEnnemy>().Health -= 1;
			}
			//barak
			if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyB_AI){
				col.gameObject.GetComponent<BasicEnnemy>().Health -= 1;
				col.gameObject.GetComponent<Rigidbody>().AddForce(-col.transform.forward * 3000.0f);
			}
			//Coloss
			if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyC_Ai){
				col.gameObject.GetComponent<BasicEnnemy>().Health -= 2;
			}
			//Ingénieur
			if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyD_AI){
				col.gameObject.GetComponent<BasicEnnemy>().Health -= 1;
			}
		}
	}
}
