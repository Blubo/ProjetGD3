using UnityEngine;
using System.Collections;

public class DamageDealer : MonoBehaviour {
	
	private Sticky mySticky;
	//la vitesse nécessaire pour péter un fdp
	[SerializeField]
	private float _necessaryVelocity;

	[SerializeField]
	private float _playerSizeMultiplicator = 0.9f;

	public int damageCaserne;
	public int damageArbre;
	public int damageEnnemy;
	
	private Color hitObjectColor;
	
	[SerializeField]
	private GameObject nuageVitesse;
	
	private Rigidbody myRB;
	
	// Use this for initialization
	void Start () {

		if(gameObject.GetComponent<Sticky>()!=null) mySticky = gameObject.GetComponent<Sticky>();
		if(gameObject.GetComponent<Rigidbody>() != null) myRB = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if(myRB !=null){
			if(mySticky!=null){
//				if(Input.GetKeyDown(KeyCode.Space)){
//					Debug.Log("fdp???");
//					if(mySticky.v_numberOfLinks>0)
//					{
//						Debug.Log("my speed is "+mySticky._Velocity);
//					}
//				}
				if(mySticky._Velocity >=_necessaryVelocity){
					if(nuageVitesse != null){
						GameObject nuageFeedback = Instantiate(nuageVitesse, gameObject.transform.position, Quaternion.identity) as GameObject;
					}
				}
			}
		}
	}
	
	void OnCollisionEnter(Collision col){

		//NEW ATTEMPT 22ND MAY

		/*CAN TAKE DAMAGE
		 * idole
		 * joueur
		 * fronde (friable)
		 * interrupteur (friable)
		 * decor
		 * enemy petit vivant
		 * enemy ingenieur
		 * enemiKO (décor/fronde)
		 * caserneKO (décor/fronde)
		 * enemyVivants


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

		//si je suis l'idole
		if(gameObject.tag.Equals("Idole")){
			//si un ennemi me touche
//			if(col.gameObject.tag.Equals("Ennemy")){
//				//faiblard
//				if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyA_AI){
//					col.gameObject.GetComponent<BasicEnnemy>().TakeDamage(damageEnnemy);
//					return;
//				}
//				//Ingénieur
//				else if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyD_AI){
//					col.gameObject.GetComponent<BasicEnnemy>().TakeDamage(damageEnnemy);
//					return;
//				}
//			}
		}

		//si je suis un joueur
		if(gameObject.tag.Equals("Player")){
			//SI c'est l'autre la fronde
			if(col.gameObject.GetComponent<Sticky>()!=null){
				Sticky stickyCollided = col.gameObject.GetComponent<Sticky>();
				//si lié ou lié depuis peu
				if(stickyCollided.v_numberOfLinks!=0|| stickyCollided.wasLinkedNotLongAgo==true){
					//si cette fronde va vite
					if(col.gameObject.GetComponent<DamageDealer>()!= null && stickyCollided._Velocity>col.gameObject.GetComponent<DamageDealer>()._necessaryVelocity){
						//si une fronde friable me cogne
						if(col.gameObject.tag.Equals("FrondeFriable")){
							col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
							return;
						}else
						if(col.gameObject.tag.Equals("CaserneKO")){
							col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
							return;
						}
						else if(col.gameObject.tag.Equals("Arbre")){
							col.gameObject.GetComponent<ObjectStats>().TakeDamage(damageArbre);
							return;
						}
						//si je cogne un ennemi mort
						else if(col.gameObject.tag.Equals("Ragdoll")){
							col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
							return;
						}
					}
				}
			}
			//si un ennemi me touche
//			else if(col.gameObject.tag.Equals("Ennemy")){
//				//faiblard
//				if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyA_AI){
//					col.gameObject.GetComponent<BasicEnnemy>().TakeDamage(damageEnnemy);
//					return;
//				}
//				//Ingénieur
//				else if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyD_AI){
//					col.gameObject.GetComponent<BasicEnnemy>().TakeDamage(damageEnnemy);
//					return;
//				}
//			}
		}

		//si je suis une fronde
		if(gameObject.tag.Equals("Fronde") || gameObject.tag.Equals("FrondeFriable") || gameObject.tag.Equals("CaserneKO")){
			//si ma fronde est liée ou liée ya pas longtemps
			if(mySticky!=null && mySticky.v_numberOfLinks!=0|| mySticky!=null && mySticky.wasLinkedNotLongAgo==true){
				//si ma fronde va vite
				if(myRB.velocity.magnitude>_necessaryVelocity){
					if(col.gameObject.tag.Equals("Player")){
//						Debug.Log(col.gameObject.name);
//						Debug.Log(mySticky.myHolderPlayer.name);
						if(mySticky.myHolderPlayer != col.gameObject){
							Vector3 directionOfCollision = new Vector3(col.gameObject.transform.position.x, col.gameObject.transform.position.y + 10f, col.gameObject.transform.position.z) - gameObject.transform.position ;
							col.gameObject.GetComponent<Player_Status>().TakeDamage(directionOfCollision);
							col.gameObject.GetComponent<Player_Status>().SeverLinkToIdole();

	//						Vector3 directionOfCollision = col.gameObject.transform.position - gameObject.transform.position ;
	//						if(col.gameObject.GetComponent<Player_Status>()._IsInvincible)
	//						col.gameObject.GetComponent<FatPlayerScript>().ChangeSize(_playerSizeMultiplicator);
							return;
						}
						//si je cogne une fronde friable
					}else if(col.gameObject.tag.Equals("FrondeFriable")){
						col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
						return;

						//si je cogne un décor unlinkable mais destructible
					}
					else 		if(col.gameObject.tag.Equals("CaserneKO")){
						col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
						return;
					}
					else if(col.gameObject.tag.Equals("UnlinkableDestructible")){
						col.gameObject.GetComponent<ObjectStats>().TakeDamage(damageArbre);
						//w_Sounds.PlaySoundOneShot("Barrière impact");
						return;
					}else if(col.gameObject.tag.Equals("WoodBlock")){
						col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
						return;

						//si je cogne un arbre
					}else if(col.gameObject.tag.Equals("Arbre")){
						col.gameObject.GetComponent<ObjectStats>().TakeDamage(damageArbre);
//						Debug.Log("case 1");
						return;
					}
					else if(col.gameObject.tag.Equals("Unlinkable")){
						col.gameObject.GetComponent<CaserneStats>().TakeDamage(damageCaserne);
					}
					//si je cogne un ennemi
					else if(col.gameObject.tag.Equals("Ennemy")){
						//Faiblar
						if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyA_AI){
							col.gameObject.GetComponent<BasicEnnemy>().TakeDamage(damageEnnemy);
							return;
						}
						//barak
						if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyB_AI){
							col.gameObject.GetComponent<BasicEnnemy>().TakeDamage(damageEnnemy);
							col.gameObject.GetComponent<Rigidbody>().AddForce(-col.transform.forward * 50000.0f);
							return;
						}
						//Coloss
						if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyC_Ai){
							col.gameObject.GetComponent<BasicEnnemy>().TakeDamage(damageEnnemy);
							return;
						}
						//Ingénieur
						if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyD_AI){
							col.gameObject.GetComponent<BasicEnnemy>().TakeDamage(damageEnnemy);
							return;
						}
					}
					//si je cogne un ennemi mort
					else if(col.gameObject.tag.Equals("Ragdoll")){
						col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
					}
				}
				//SI c'est l'autre la fronde
			}else if(col.gameObject.GetComponent<Sticky>()!=null){
				Sticky stickyCollided = col.gameObject.GetComponent<Sticky>();
				//si lié ou lié depuis peu
				if(stickyCollided.v_numberOfLinks!=0|| stickyCollided.wasLinkedNotLongAgo==true){
					//si cette fronde va vite
					if(stickyCollided._Velocity>col.gameObject.GetComponent<DamageDealer>()._necessaryVelocity){
						//si une fronde friable me cogne
						if(col.gameObject.tag.Equals("FrondeFriable")){
							col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
							return;
							//si un arbre me cogn
						}
						else if(col.gameObject.tag.Equals("CaserneKO")){
							col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
							return;
						}
						else if(col.gameObject.tag.Equals("Arbre")){
							col.gameObject.GetComponent<ObjectStats>().TakeDamage(damageArbre);
							return;
							//si un ennemi me cogne
						}else if(col.gameObject.tag.Equals("Ennemy")){
							//Faiblar
							if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyA_AI){
								col.gameObject.GetComponent<BasicEnnemy>().Health -= 1;
								return;
							}
							//Ingénieur
							else if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyD_AI){
								col.gameObject.GetComponent<BasicEnnemy>().Health -= 1;
								return;
							}
						}
					}
				}
			}
		}

//		//si je suis du décor unlinkable et potentiellement friable
		if(gameObject.tag.Equals("UnlinkableDestructible") || gameObject.tag.Equals("Static") || col.gameObject.tag.Equals("WoodBlock")){
			if(col.gameObject.GetComponent<Sticky>()!=null){
				Sticky stickyCollided = col.gameObject.GetComponent<Sticky>();
				//si lié ou lié depuis peu
				if(stickyCollided.v_numberOfLinks!=0|| stickyCollided.wasLinkedNotLongAgo==true){
					//si cette fronde va vite
					if(stickyCollided._Velocity>col.gameObject.GetComponent<DamageDealer>()._necessaryVelocity){
						//si un arbre me cogne
						if(col.gameObject.tag.Equals("Arbre")){
							col.gameObject.GetComponent<ObjectStats>().TakeDamage(damageArbre);
							return;
							//si un ennemi me cogne
						}else if(col.gameObject.tag.Equals("Ennemy")){
							//Faiblar
							if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyA_AI){
								col.gameObject.GetComponent<BasicEnnemy>().TakeDamage(damageEnnemy);
								return;
							}
							//Ingénieur
							if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyD_AI){
								col.gameObject.GetComponent<BasicEnnemy>().TakeDamage(damageEnnemy);
								return;
								
							}
							//si un ennemi ko me cogne
						}
						else if(col.gameObject.tag.Equals("Ragdoll")){
							col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
						}

						//BUG
						//SCENE 2 
						if(gameObject.tag.Equals("Static") == false){
							if(col.gameObject.GetComponent<ObjectStats>() != null){
								col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
							}
						}
					}
				}
			}
		}

		//si je suis du décor
		if(gameObject.tag.Equals("Arbre")){
			if(mySticky!=null && mySticky.v_numberOfLinks!=0|| mySticky!=null && mySticky.wasLinkedNotLongAgo==true){
				//si mon décor va vite
				if(myRB.velocity.magnitude>_necessaryVelocity){
					//si je cogne une fronde friable
					if(col.gameObject.tag.Equals("FrondeFriable")){
						col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
						return;
						//si je cogne du décor
					}
					else if(col.gameObject.tag.Equals("CaserneKO")){
						col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
						return;
					}
					else if(col.gameObject.tag.Equals("Arbre")){
						col.gameObject.GetComponent<ObjectStats>().TakeDamage(damageArbre);
						return;
						//si je cogne un décor unlinkable ou interrupteur friable
					}else if(col.gameObject.tag.Equals("UnlinkableDestructible") || col.gameObject.tag.Equals("WoodBlock")){
						col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
						return;
						//si je cogne un ennemi
					}
					else if(col.gameObject.tag.Equals("Unlinkable")){
						col.gameObject.GetComponent<CaserneStats>().TakeDamage(damageCaserne);
					}
					else if(col.gameObject.tag.Equals("Ennemy")){
						//Faiblar
						if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyA_AI){
							col.gameObject.GetComponent<BasicEnnemy>().TakeDamage(damageEnnemy);
							return;
						}
						//barak
						if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyB_AI){
							col.gameObject.GetComponent<BasicEnnemy>().TakeDamage(damageEnnemy);
							col.gameObject.GetComponent<Rigidbody>().AddForce(-col.transform.forward * 50000.0f);
							return;
							
						}
						//Coloss
						if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyC_Ai){
							col.gameObject.GetComponent<BasicEnnemy>().TakeDamage(damageEnnemy);
							return;
							
						}
						//Ingénieur
						if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyD_AI){
							col.gameObject.GetComponent<BasicEnnemy>().TakeDamage(damageEnnemy);
							return;
							
						}
					}		//si je cogne un ennemi mort
					else if(col.gameObject.tag.Equals("Ragdoll")){
						col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
						return;
					}
				}
				//si l'autre objet va vite
			}else if(col.gameObject.GetComponent<Sticky>()!=null){
				Sticky stickyCollided = col.gameObject.GetComponent<Sticky>();
				//si lié ou lié depuis peu
				if(stickyCollided.v_numberOfLinks!=0|| stickyCollided.wasLinkedNotLongAgo==true){
					//si cet autre objet va vite
					//BUG
					//SCENE 2
					if(stickyCollided._Velocity>col.gameObject.GetComponent<DamageDealer>()._necessaryVelocity){
						//si une fronde friable me cogne
						if(col.gameObject.tag.Equals("FrondeFriable")){
							col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
						}

						else if(col.gameObject.tag.Equals("CaserneKO")){
							col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
							return;
						}

						//si un décor me cogne
						else if(col.gameObject.tag.Equals("Arbre")){
							col.gameObject.GetComponent<ObjectStats>().TakeDamage(damageArbre);
						}
						//si un ennemi me touche
						else if(col.gameObject.tag.Equals("Ennemy")){
							//Faiblar
							if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyA_AI){
								col.gameObject.GetComponent<BasicEnnemy>().TakeDamage(damageEnnemy);
								return;
							}
							//Ingénieur
							if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyD_AI){
								col.gameObject.GetComponent<BasicEnnemy>().TakeDamage(damageEnnemy);
								return;
								
							}
						}		//si je cogne un ennemi mort
						else if(col.gameObject.tag.Equals("Ragdoll")){
							col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
							return;
						}
					}
				}
			}
		}

		//si je suis un ennemi
		if(gameObject.tag.Equals("Ennemy")){
			//si je touche l'idole
			if(col.gameObject.tag.Equals("Idole")){
				col.gameObject.GetComponent<Idole_Status>().TakeDamage(1);
				return;
				//si je touche un joueur
			}else if(col.gameObject.tag.Equals("Player")){
				Vector3 directionOfCollision = new Vector3(col.gameObject.transform.position.x, col.gameObject.transform.position.y + 10f, col.gameObject.transform.position.z) - gameObject.transform.position ;
				col.gameObject.GetComponent<Player_Status>().TakeDamage(directionOfCollision);
				//CECI PETE LE LIEN DUN POTE SI L ENNEMI A ETE FRONDE
				if(mySticky!=null && mySticky.v_numberOfLinks!=0|| mySticky!=null && mySticky.wasLinkedNotLongAgo==true){
					if(myRB.velocity.magnitude>_necessaryVelocity){
						col.gameObject.GetComponent<Player_Status>().SeverLinkToIdole();
					}
				}
				return;
			}else if(col.gameObject.tag.Equals("FrondeFriable")){
				col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
				return;
				//si je touche un bloc en bois
			}
			else if(col.gameObject.tag.Equals("CaserneKO")){
				col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
				return;
			}
			else if(col.gameObject.tag.Equals("WoodBlock")){
				col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
				return;
			}
			else if(col.gameObject.tag.Equals("Ragdoll")){
				col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
				return;
			}

			//si je suis une fronde
			if(mySticky!=null && mySticky.v_numberOfLinks!=0|| mySticky!=null && mySticky.wasLinkedNotLongAgo==true){
				if(myRB.velocity.magnitude>_necessaryVelocity){
					//si je touche une barriere
					if(col.gameObject.tag.Equals("UnlinkableDestructible")){
						col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
						return;
						//si je touche du décor
					}else if(col.gameObject.tag.Equals("Arbre")){
						col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
						return;
						//si je touche un ennemy
					}
					else if(col.gameObject.tag.Equals("Unlinkable")){
						col.gameObject.GetComponent<CaserneStats>().TakeDamage(damageCaserne);
					}
					else if(col.gameObject.tag.Equals("Ennemy")){
						//Faiblar
						if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyA_AI){
							col.gameObject.GetComponent<BasicEnnemy>().TakeDamage(damageEnnemy);
							return;
						}
						//barak
						if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyB_AI){
							col.gameObject.GetComponent<BasicEnnemy>().TakeDamage(damageEnnemy);
							col.gameObject.GetComponent<Rigidbody>().AddForce(-col.transform.forward * 50000.0f);
							return;
							
						}
						//Coloss
						if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyC_Ai){
							col.gameObject.GetComponent<BasicEnnemy>().TakeDamage(damageEnnemy);
							return;
							
						}
						//Ingénieur
						if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyD_AI){
							col.gameObject.GetComponent<BasicEnnemy>().TakeDamage(damageEnnemy);
							return;
							
						}
						//si je touche un ennemiKO
					}
//					else if(col.gameObject.tag.Equals("Ragdoll")){
//						col.gameObject.GetComponent<Idole_Status>().TakeDamage(1);
//						return;
//					}
				}
				//si l'autre est une fronde
			}else if(col.gameObject.GetComponent<Sticky>()!=null){
				Sticky stickyCollided = col.gameObject.GetComponent<Sticky>();
				//si lié ou lié depuis peu
				if(stickyCollided.v_numberOfLinks!=0|| stickyCollided.wasLinkedNotLongAgo==true){
					//s'il va vite
					if(stickyCollided._Velocity>col.gameObject.GetComponent<DamageDealer>()._necessaryVelocity){
						if(col.gameObject.tag.Equals("Arbre")){
							col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
							return;
							//si je touche un ennemy
						}else if(col.gameObject.tag.Equals("Ennemy")){
							//Faiblar
							if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyA_AI){
								col.gameObject.GetComponent<BasicEnnemy>().TakeDamage(damageEnnemy);
								return;
							}
							//Ingénieur
							if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyD_AI){
								col.gameObject.GetComponent<BasicEnnemy>().TakeDamage(damageEnnemy);
								return;
								
							}
							return;
							//si je touche un ennemiKO
						}else if(col.gameObject.tag.Equals("Ragdoll")){
							col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
							return;
						}
					}
				}
			}
		}

		//si je suis un ennemi ko
		if(gameObject.tag.Equals("Ragdoll")){
			if(mySticky!=null && mySticky.v_numberOfLinks!=0|| mySticky!=null && mySticky.wasLinkedNotLongAgo==true){
				//si mon ragdoll va vite
				if(myRB.velocity.magnitude>_necessaryVelocity){
					//si je cogne une fronde friable
					if(col.gameObject.tag.Equals("FrondeFriable")){
						col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
						return;
						//si je cogne un interrupteur friable
					}
					else if(col.gameObject.tag.Equals("CaserneKO")){
						col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
						return;
					}
					else if(col.gameObject.tag.Equals("WoodBlock")){
						col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
						return;
						//si je cogne une barriere etc
					}else if(col.gameObject.tag.Equals("UnlinkableDestructible")){
						col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
						return;
						//si je cogne du décor
					}else if(col.gameObject.tag.Equals("Arbre")){
						col.gameObject.GetComponent<ObjectStats>().TakeDamage(damageArbre);
						return;
						//si je cogne un ennemi
					}
					else if(col.gameObject.tag.Equals("Unlinkable")){
						col.gameObject.GetComponent<CaserneStats>().TakeDamage(damageCaserne);
					}
					else if(col.gameObject.tag.Equals("Ennemy")){
						//Faiblar
						if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyA_AI){
							col.gameObject.GetComponent<BasicEnnemy>().TakeDamage(damageEnnemy);
							return;
						}
						//barak
						if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyB_AI){
							col.gameObject.GetComponent<BasicEnnemy>().TakeDamage(damageEnnemy);
							col.gameObject.GetComponent<Rigidbody>().AddForce(-col.transform.forward * 50000.0f);
							return;
							
						}
						//Coloss
						if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyC_Ai){
							col.gameObject.GetComponent<BasicEnnemy>().TakeDamage(damageEnnemy);
							return;
							
						}
						//Ingénieur
						if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyD_AI){
							col.gameObject.GetComponent<BasicEnnemy>().TakeDamage(damageEnnemy);
							return;
							
						}
						return;
						//si je cogne un ragdoll
					}else if(col.gameObject.tag.Equals("Ragdoll")){
						col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
						return;
					}
				}
			}else if(col.gameObject.GetComponent<Sticky>()!=null){
				Sticky stickyCollided = col.gameObject.GetComponent<Sticky>();
				//si lié ou lié depuis peu
				if(stickyCollided.v_numberOfLinks!=0|| stickyCollided.wasLinkedNotLongAgo==true){
					//si ce ragdoll va vite
					if(stickyCollided._Velocity>col.gameObject.GetComponent<DamageDealer>()._necessaryVelocity){
						//si une fronde friable s'écrase sur moi 
						if(col.gameObject.tag.Equals("FrondeFriable")){
							return;
							//si un interrupteur friable s'écrase sur moi 
						}
						else 		if(col.gameObject.tag.Equals("CaserneKO")){
							col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
							return;
						}
						else if(col.gameObject.tag.Equals("WoodBlock")){
							return;
							//si du décor s'écrase sur moi
						}else if(col.gameObject.tag.Equals("Arbre")){
							return;
							//si un ennemi s'écrase sur moi
						}else if(col.gameObject.tag.Equals("Ennemy")){
							//Faiblar
							if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyA_AI){
								col.gameObject.GetComponent<BasicEnnemy>().Health -= 1;
								return;
							}
							//Ingénieur
							if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyD_AI){
								col.gameObject.GetComponent<BasicEnnemy>().Health -= 1;
								return;
								
							}
							return;
							//si un radgoll s'écrase sur moi
						}else if(col.gameObject.tag.Equals("Ragdoll")){
							return;
						}
					}
				}
			}
		}

		//si je suis un projectile de canon
		if(gameObject.tag.Equals("Weapon")){
			//si je touche l'idole
			if(col.gameObject.tag.Equals("Idole")){
				col.gameObject.GetComponent<Idole_Status>().TakeDamage(1);
				return;
				//si je touche un joueur
			}else if(col.gameObject.tag.Equals("Player")){
//				Vector3 directionOfCollision = col.gameObject.transform.position - gameObject.transform.position ;
//				col.gameObject.GetComponent<FatPlayerScript>().ChangeSize(_playerSizeMultiplicator);
				//si j'ai un tireur et que ce tireur n'est pas le joueur que je touche
				if(gameObject.GetComponent<TurretProjectile>()._playerWhoShotMe!=null && gameObject.GetComponent<TurretProjectile>()._playerWhoShotMe != col.gameObject){
					Vector3 directionOfCollision = new Vector3(col.gameObject.transform.position.x, col.gameObject.transform.position.y + 10f, col.gameObject.transform.position.z) - gameObject.transform.position ;
					col.gameObject.GetComponent<Player_Status>().TakeDamage(directionOfCollision);
					col.gameObject.GetComponent<Player_Status>().SeverLinkToIdole();
					return;
				}
				//si je touche une fronde friable
			}else if(col.gameObject.tag.Equals("FrondeFriable")){
				col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
				return;
				//si je touche un décor unlinkable
			}
			else 		if(col.gameObject.tag.Equals("CaserneKO")){
				col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
				return;
			}
			else if(col.gameObject.tag.Equals("UnlinkableDestructible")){
				col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
				return;
				//si je touche un interrupteur friable
			}else if(col.gameObject.tag.Equals("WoodBlock")){
				col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
				return;
				//si je touche du décor
			}
			else if(col.gameObject.tag.Equals("Arbre")){
				col.gameObject.GetComponent<ObjectStats>().TakeDamage(damageArbre);
				return;
				//si je touche un ennemi
			}
			else if(col.gameObject.tag.Equals("Unlinkable")){
				col.gameObject.GetComponent<CaserneStats>().TakeDamage(damageCaserne);
			}
			else if(col.gameObject.tag.Equals("Ennemy")){
				//Faiblar
				if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyA_AI){
					col.gameObject.GetComponent<BasicEnnemy>().TakeDamage(damageEnnemy);
					return;
				}
				//barak
				if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyB_AI){
					col.gameObject.GetComponent<BasicEnnemy>().TakeDamage(damageEnnemy);
					col.gameObject.GetComponent<Rigidbody>().AddForce(-col.transform.forward * 50000.0f);
					return;
					
				}
				//Coloss
				if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyC_Ai){
					col.gameObject.GetComponent<BasicEnnemy>().TakeDamage(damageEnnemy);
					return;
					
				}
				//Ingénieur
				if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyD_AI){
					col.gameObject.GetComponent<BasicEnnemy>().TakeDamage(damageEnnemy);
					return;
					
				}
				return;
				//si je touche un ragdoll
			}else if(col.gameObject.tag.Equals("Ragdoll")){
				col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
				return;
			}
		}

		//si je suis une caserne vivante
		if(gameObject.tag.Equals("Unlinkable")){
			if(col.gameObject.GetComponent<Sticky>()!=null){
				Sticky stickyCollided = col.gameObject.GetComponent<Sticky>();
				//si lié ou lié depuis peu
				if(stickyCollided.v_numberOfLinks!=0|| stickyCollided.wasLinkedNotLongAgo==true){
					//si cette fronde va vite
					if(stickyCollided._Velocity>col.gameObject.GetComponent<DamageDealer>()._necessaryVelocity){
						//si un arbre me cogne
						if(col.gameObject.tag.Equals("Arbre")){
							col.gameObject.GetComponent<ObjectStats>().TakeDamage(damageArbre);
							return;
							//si un ennemi me cogne
						}else if(col.gameObject.tag.Equals("Ennemy")){
							//Faiblar
							if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyA_AI){
								col.gameObject.GetComponent<BasicEnnemy>().TakeDamage(damageEnnemy);
								return;
							}
							//Ingénieur
							if (col.gameObject.GetComponent<BasicEnnemy>() is EnnemyD_AI){
								col.gameObject.GetComponent<BasicEnnemy>().TakeDamage(damageEnnemy);
								return;
								
							}
							//si un ennemi ko me cogne
						}
						else if(col.gameObject.tag.Equals("Ragdoll")){
							col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
						}
						
						if(gameObject.tag.Equals("Static") == false){
							col.gameObject.GetComponent<ObjectStats>().TakeDamage(1);
							
						}
					}
				}
			}
		}
	}
}
