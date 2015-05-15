using UnityEngine;
using System.Collections;

public class ReceptacleKey : MonoBehaviour {

	[SerializeField]
	private GameObject posCenter, receptacleRotate;

	[Tooltip("This is the time limit to open the keyhole")]
	[SerializeField]
	private float allowedTime;
	private float timer = 0f;

	private bool idoleInRec = false;
	private Quaternion initReceptRot;
	private Vector3 initReceptPos;

	[HideInInspector]
	public bool idoleCanBePulled = true;

	[Tooltip("This is the angle limit to open the keyhole")]
	[SerializeField]
	private float keyholeIsOpened;

	//la rotation de l'idole quand elle rentre dans le réceptacle, et à chaque frame
	private Vector3 idoleInitRot, idoleRotAtLastFrame;
	private float rotationCounter=0f;

	//utilisé pour fréquence de clignotement
	//(la fréquence de clignotement est actuellement 0,2, non publique, hard coded)
	private float _blinkTimer;

	//on stocke la couleur de début pour pouvoir blink
	private Color idoleColor;

	//cette booléenne est à utiliser pour les actions qui résultent de la rotation completée
	//en particulier, elle peut servir à fixer si un réceptacle est réutilisable ou non
	[HideInInspector]
	public bool rotationComplete = false;

	//this for choosing if this receptacle is reusable
	[Tooltip("Check to make One Use Only")]
	[SerializeField]
	private bool OneUseOnly;

	//on accède à stillUsable dans ActivatedSpawnByRéceptacle
	[HideInInspector]
	public bool stillUsable=true;

	//this for the time between two uses of a reusable receptacle
	//this for the visual feedback on the reusable receptacle when it is activated
	[SerializeField]
	[Tooltip("The time between two usages of the receptacle")]
	private float reusableTimer;
	private float internalTimer=0f;

	//this for exterior consequence
	[SerializeField]
	[Tooltip("Insert gameObjects affected by this réceptacle")]
	private GameObject activatedItem;

	//the original material
	private Color myVisualInitColor;

	private GameObject idole;

	// Use this for initialization
	void Start () {
//		myVisualInitColor=gameObject.transform.Find("VisuelReceptacle").GetComponent<Renderer>().material.color;
		initReceptRot = receptacleRotate.transform.rotation;
		initReceptPos = receptacleRotate.transform.position;
		_blinkTimer=0;
	}
	
	// Update is called once per frame
	void Update () {
		receptacleRotate.transform.position = initReceptPos;
		if(idoleInRec == true){
			timer+=Time.deltaTime;
		}

		if(timer>allowedTime && idoleInRec == false){
			timer=0f;
		}

		if(rotationComplete==true && OneUseOnly==false){
			internalTimer+=Time.deltaTime;
			receptacleRotate.transform.rotation = Quaternion.Lerp(receptacleRotate.transform.rotation, initReceptRot, Time.deltaTime/reusableTimer);
			//on fait rotate l'idole pour la remettre à sa rotation initiale quand elle sort du réceptacle
			//pour l'instant, on dirait qu'utiliser la rotation initiale du réceptacle fait le taf.
			//au pire, on utilise la rotation initiale de l'idole en faisant gaffe à ce que ce ne soit pas un Zero du à un reset
			if(idole!=null){
				idole.transform.rotation = Quaternion.Lerp(idole.transform.rotation, initReceptRot, Time.deltaTime/reusableTimer);
			}
		}

		if(internalTimer>=reusableTimer){
			rotationComplete=false;
//			gameObject.transform.Find("VisuelReceptacle").GetComponent<Renderer>().material.color=myVisualInitColor;
			internalTimer=0f;
		}

//		Debug.Log("rotation counter is "+rotationCounter);
	}

	void OnTriggerEnter(Collider col){
//		Debug.Log("one use only is "+OneUseOnly);
//		Debug.Log("rotation complete is "+rotationComplete);
		if(stillUsable==true){
			if(OneUseOnly == false || (OneUseOnly==true && rotationComplete==false)){
				//SI L IDOLE TOUCHE 
				if(col.gameObject.tag.Equals("Idole")){
					//ON JOUE LE SON FMOD DE SNAP DE L IDOLE SUR LE RECEPTACLE IDOLE
					Camera.main.GetComponent<SoundManagerHeritTest>().PlaySoundOneShot("Interrupteur snap");


					idole=col.gameObject;
					idoleInRec = true;
					//on stocke la rotation de l'idole au moment où
					//elle rentre dans le réceptacle
					//suite dans le TriggerStay
					idoleInitRot = col.gameObject.transform.eulerAngles;
					idoleRotAtLastFrame = idoleInitRot;
//					idoleColor = col.gameObject.GetComponent<Renderer>().material.color;
				}
			}
		}
	}

	void OnTriggerStay(Collider col){
		if(stillUsable==true){
			if(OneUseOnly == false || (OneUseOnly==true && rotationComplete==false)){
				if(col.gameObject.tag.Equals("Idole")){
					if(timer<=allowedTime){
						//le moveTowards pour pour empecher l'idole de se TP sur le point de "clé"
		//				col.gameObject.transform.position = Vector3.MoveTowards(col.gameObject.transform.position, posCenter.transform.position, 0.5f);
						col.gameObject.transform.position = posCenter.transform.position;
						//les contraintes ici pour garantir qu'on puisse tourner en Y et uniquement en Y
						col.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY ;
						//changer les stats pr que ca ressemble à un gros bloc situé à coté avec lequel ca marchait plutot bien
						col.GetComponent<Rigidbody>().mass=100;
						col.GetComponent<Rigidbody>().angularDrag = 10;

						//quand l'idole entre dans le réceptacle, on l'empeche de rotate "dans le négatif"
						if( col.gameObject.transform.eulerAngles.y< idoleInitRot.y ){
							col.gameObject.transform.eulerAngles = new Vector3(col.gameObject.transform.eulerAngles.x, idoleInitRot.y, col.gameObject.transform.eulerAngles.z);
						}

						//empecher de faire plus d'un tour complet?
						//ne marche pas
		//				if( col.gameObject.transform.eulerAngles.y > 360 ){
		//					col.gameObject.transform.eulerAngles = new Vector3(col.gameObject.transform.eulerAngles.x, 360, col.gameObject.transform.eulerAngles.z);
		//				}

						//ROTATION DU RECEPTACLE
						//en modifiant directement les euler
						//en utilisant Rotate
						//on rotate à chaque frame de la différence entre la position actuelle et la position à la frame précédente
						rotationCounter=col.gameObject.transform.eulerAngles.y - idoleInitRot.y;
						if(rotationCounter<360 && rotationCounter > 0){
							receptacleRotate.transform.Rotate(Vector3.up, col.gameObject.transform.eulerAngles.y - idoleRotAtLastFrame.y);
						}

						//ROTATION TERMINEE
						if(col.gameObject.transform.eulerAngles.y - idoleInitRot.y>=keyholeIsOpened){
							//first attempt at freezing rotations
							col.gameObject.transform.eulerAngles = new Vector3(col.gameObject.transform.eulerAngles.x, keyholeIsOpened+idoleInitRot.y, col.gameObject.transform.eulerAngles.z);
		//					col.gameObject.GetComponent<Rigidbody>().isKinematic =true;
							col.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ ;

							rotationComplete=true;
							activatedItem.SendMessage("Activated");

							//METTRE PLAY ONE SHOT FMOD ICI POUR RECEPTACLE IDOLE ROTATIONNE ENTIEREMENT


							//AVEC L AJOUT DE ONEUSEONLY, on teste si rotation est complete, et si oui, on ne refait pas le TriggerStay donc on ne reset jamais les valeurs ci-dessous de la maniere élégante
							//(maniere élégante = timer>allowedTimer)
							//donc on refait en dur
							//remettre les stats de base si jamais on les a modifiées à la main avant
							col.GetComponent<Rigidbody>().mass=10;
							col.GetComponent<Rigidbody>().angularDrag = 35;
							
							//on reset les valeurs enregistrées de rotations de maniere à ce que ca déconne pas quand on réutilisera ce réceptacle
							idoleRotAtLastFrame = Vector3.zero;
							idoleInitRot = Vector3.zero;
							col.gameObject.GetComponent<Renderer>().material.color=idoleColor;
							rotationCounter=0f;
							//ici, mes ajouts personnels
							//des features qui me semblent utiles:
							//un feedback visuel sur l'idole?
	//						Blink(col.gameObject);
							//un arret du freeze de l'idole si la rotation est terminée?
							timer=allowedTime;
							//arret timer et CE feedback là (clignotement de l'idole pendant le temps restant où elle est freezée une fois rotation terminée) sont évidemment pas compatibles
							//un feedback visuel sur le réceptacle?
//							gameObject.transform.Find("VisuelReceptacle").GetComponent<Renderer>().material.color = Color.red;
						}

						//on stocke la rotation de l'idole à cette frame
						idoleRotAtLastFrame = col.gameObject.transform.eulerAngles;

					//si on est trop lent à faire la manip
					}else{
						//remettre les contraintes "normales"... du coup ca implique que l'objet est constraint en X, Y et Z par défaut
						col.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ ;
						//remettre les stats de base si jamais on les a modifiées à la main avant
						col.GetComponent<Rigidbody>().mass=10;
						col.GetComponent<Rigidbody>().angularDrag = 35;

						//on reset les valeurs enregistrées de rotations de maniere à ce que ca déconne pas quand on réutilisera ce réceptacle
						idoleRotAtLastFrame = Vector3.zero;
						idoleInitRot = Vector3.zero;
						col.gameObject.GetComponent<Renderer>().material.color=idoleColor;
						rotationCounter=0f;

						//on reset également la rotation du réceptacle
						//si rotation incomplète
						if(rotationComplete == false){
							receptacleRotate.transform.rotation = Quaternion.Lerp(receptacleRotate.transform.rotation, initReceptRot, Time.deltaTime); 
							if(idole!=null){
//								idole.transform.rotation = Quaternion.Lerp(idole.transform.rotation, initReceptRot, Time.deltaTime/reusableTimer);
								idole.gameObject.transform.eulerAngles = new Vector3(idole.gameObject.transform.eulerAngles.x, idoleInitRot.y, idole.gameObject.transform.eulerAngles.z);

							}
						}	
					}
				}
			}
		}
	}

	void OnTriggerExit(Collider col){
		if(col.gameObject.tag.Equals("Idole")){
			idoleInRec = false;
			col.GetComponent<Rigidbody>().mass=1;
			col.GetComponent<Rigidbody>().drag = 1;
			col.GetComponent<Rigidbody>().angularDrag = 35;
		}
	}

	void Blink(GameObject objectToBlink){
		_blinkTimer+=Time.deltaTime;
		if(_blinkTimer>0.2f){
			if(objectToBlink.GetComponent<Renderer>().material.color==Color.white){
				objectToBlink.GetComponent<Renderer>().material.color=idoleColor;
			}else{
				objectToBlink.GetComponent<Renderer>().material.color=Color.white;
			}
			_blinkTimer=0f;
		}
	}
}
