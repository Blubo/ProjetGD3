using UnityEngine;
using System.Collections;

public class LevierMovable : MonoBehaviour {
	[SerializeField]
	[Tooltip("Insert gameObject affected by this levier")]
	private GameObject activatedItem;

	[Tooltip("Infinite uses or just one")]
	[SerializeField]
	private bool limitedUses;
	
	[Tooltip("Number of uses if limited")]
	[SerializeField]
	private int numberLimitedUses;
	
	//the number of times this has been activated
	private int activatedCounter;

	[Space(5f)]
	
	[Tooltip("Original = default position, Maximum = écartement maximal de gachette quand tirée")]
	[Header("Gachette specs")]
	[SerializeField]
	private Transform OriginalPlacement;
	[SerializeField]
	private Transform maximumPlacement;
	
	[Space(5f)]
	
	[Tooltip("the speed at which the gachette goes back to initPos")]
	[SerializeField]
	private float returnForce;
	
	//is this object grabbed by a player?
	private bool grapped;
	private bool uncocked;
	void Start () {
		activatedCounter=0;

		grapped=false;
		uncocked=false;
	}
	
	void Update () {
		if(gameObject.GetComponent<Sticky>().v_numberOfLinks!=0){
			grapped=true;
			gameObject.GetComponent<Rigidbody>().isKinematic=false;
		}else{
			grapped=false;
			//gameObject.GetComponent<Rigidbody>().isKinematic=true;
		}
		
		//la distance entre la gachette et son point de départ
		float Distance = Vector3.Distance(transform.position, OriginalPlacement.position);
		
		//si la distance entre gachette et pt de départ>la distance max autorisée
		//on "clamp" la gachette sur la distance max
		if(Distance>Vector3.Distance(maximumPlacement.position, OriginalPlacement.position)){
			uncocked=true;
			gameObject.transform.position = maximumPlacement.position;
		}
		
		//UNIQUEMENT SI GACHETTE LIBRE DE HOOKHEAD
		//possiblement non voulu par le GD?
		if(grapped == false){
			//si très proche de pt de départ, alors on la pose dessus et on reset la situationa avec kinematic=true
			if(Distance<0.1f){
				gameObject.transform.position = OriginalPlacement.position;
				gameObject.GetComponent<Rigidbody>().isKinematic=true;
				if(uncocked==true){
					uncocked=false;

					if((limitedUses==true && activatedCounter<numberLimitedUses)||limitedUses==false){
						activatedCounter+=1;

						activatedItem.SendMessage("Activated");
						if(activatedItem.GetComponent<MultipleActivation>()==null){
							Camera.main.GetComponent<SoundManagerHeritTest>().PlaySoundOneShot("Interrupteur activer");
						}
					}
				}
			}else{
				//sinon, tu reviens vers le pt de départ
				gameObject.GetComponent<Rigidbody>().AddForce( (OriginalPlacement.position-transform.position).normalized*returnForce*Time.deltaTime);
			}
		}
	}
}
