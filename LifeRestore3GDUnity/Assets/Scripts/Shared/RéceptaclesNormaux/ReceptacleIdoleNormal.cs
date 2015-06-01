using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReceptacleIdoleNormal : MonoBehaviour {

	//this for exterior consequence
	[SerializeField]
	[Tooltip("Insert gameObjects affected by this réceptacle")]
	private List<GameObject> activatedItem;

	[Tooltip("Infinite uses or just one")]
	[SerializeField]
	private bool limitedUses;
	
	[Tooltip("Number of uses if limited")]
	[SerializeField]
	private int numberLimitedUses;
	
	//the number of times this has been activated
	private int activatedCounter;

	[SerializeField]
	[Tooltip("Insert gameobjects supposed to change appearance when this is activated")]
	private List<GameObject> myActivatedFeedbacks;

	private Color[] myActivatedFeedbacksColors;

	// Use this for initialization
	void Start () {
		myActivatedFeedbacksColors = new Color[myActivatedFeedbacks.Count];

		for (int l = 0; l < myActivatedFeedbacks.Count; l++) {
			myActivatedFeedbacksColors[l] = myActivatedFeedbacks[l].GetComponent<Renderer>().material.color;
		}

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
		if(col.gameObject.tag.Equals("Idole")){
			
			if((limitedUses==true && activatedCounter<numberLimitedUses)||limitedUses==false){
				//				Debug.Log("activated");
				//PLAY ONE SHOT FMOD ICI POUR REUSSIR A METTRE UN BLOC DANS UN RECEPTACLE NORMAL
				
				activatedCounter+=1;
				MadeMyJob();

				for (int i = 0; i < activatedItem.Count; i++) {
          if (activatedItem[i] != null)
          {
            activatedItem[i].BroadcastMessage("Activated");
          }

					if(activatedItem[i].GetComponent<MultipleActivation>()==null){
						Camera.main.GetComponent<SoundManagerHeritTest>().PlaySoundOneShot("Interrupteur activer");
					}
				}
			}
		}
	}

	void MadeMyJob(){
		for (int i = 0; i < myActivatedFeedbacks.Count; i++) {
			myActivatedFeedbacks[i].GetComponent<Renderer>().material.color = Color.green;
		}
	}

	void UndidMyJob(){
		for (int i = 0; i < myActivatedFeedbacks.Count; i++) {
			myActivatedFeedbacks[i].GetComponent<Renderer>().material.color = myActivatedFeedbacksColors[i];
		}
	}
}
