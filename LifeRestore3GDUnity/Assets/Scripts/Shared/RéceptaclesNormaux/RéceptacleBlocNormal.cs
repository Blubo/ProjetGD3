using UnityEngine;
using System.Collections;

public class RéceptacleBlocNormal : MonoBehaviour {

	//this for exterior consequence
	[SerializeField]
	[Tooltip("Insert gameObjects affected by this réceptacle")]
	private GameObject activatedItem;

	[Tooltip("Infinite uses or just one")]
	[SerializeField]
	private bool limitedUses;

	[Tooltip("Number of uses if limited")]
	[SerializeField]
	private int numberLimitedUses;

	[Tooltip("Will this be desactivated if bloc vire du receptacle")]
	[SerializeField]
	private bool canBeDesactivated;

	//the number of times this has been activated
	private int activatedCounter;

	[Range(1,4)]
	[SerializeField]
	private int size;

	// Use this for initialization
	void Start () {
		activatedCounter=0;
	}

	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		//on a besoin d'activer l'objet dès qu'un bloc normal rentre dans le collider
		//pour commencer, on teste simplement si l'objet est de layer CanBreak
		//il faut parer à toute eventualité: qu'arrive-t-il aux objets posés dedans?
		//que ce soit les blocs normaux, avant et après une première activation, l'idole, joueur, ennemis, etc

		//de la meme manière, doit-on permetre un usage infini ou limité de ce réceptacle standard?
		//si limité, tuer le code ET fermer l'accès ?
//		if(col.gameObject.layer.Equals("CanBreak")){
		if(col.gameObject.layer == 17){
			if(col.gameObject.GetComponent<BlocInterrupteur>().size == size){
				if((limitedUses==true && activatedCounter<numberLimitedUses)||limitedUses==false){
				//PLAY ONE SHOT FMOD ICI POUR REUSSIR A METTRE UN BLOC DANS UN RECEPTACLE NORMAL

					activatedCounter+=1;
					activatedItem.SendMessage("Activated");
					if(activatedItem.GetComponent<MultipleActivation>()==null){
						Camera.main.GetComponent<SoundManagerHeritTest>().PlaySoundOneShot("Interrupteur activer");
					}
				}
			}
		}
	}

	void OnTriggerExit(Collider col){
		if(canBeDesactivated == true){
			//on a besoin d'activer l'objet dès qu'un bloc normal rentre dans le collider
			//pour commencer, on teste simplement si l'objet est de layer CanBreak
			//il faut parer à toute eventualité: qu'arrive-t-il aux objets posés dedans?
			//que ce soit les blocs normaux, avant et après une première activation, l'idole, joueur, ennemis, etc
			
			//de la meme manière, doit-on permetre un usage infini ou limité de ce réceptacle standard?
			//si limité, tuer le code ET fermer l'accès ?
			//		if(col.gameObject.layer.Equals("CanBreak")){
			if(col.gameObject.layer == 17){
				if(col.gameObject.GetComponent<BlocInterrupteur>().size == size){
					Debug.Log("gnééée");
					activatedCounter-=1;
					activatedItem.SendMessage("Deactivated");
					//JOUER SON DE DESACTIVATION?
	//				if(activatedItem.GetComponent<MultipleActivation>()==null){
	//					Camera.main.GetComponent<SoundManagerHeritTest>().PlaySoundOneShot("Interrupteur activer");
	//				}
				}
			}
		}
	}
}
