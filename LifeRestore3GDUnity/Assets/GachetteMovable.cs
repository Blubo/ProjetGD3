using UnityEngine;
using System.Collections;

public class GachetteMovable : MonoBehaviour {
	[Header("Canon Body")]
	[Tooltip("The canon Body this gachette is linked to")]
	[SerializeField]
	private GameObject canonBody;

	[Space(5f)]
	[Header("Canon Shooter")]
	[Tooltip("The canon Shooter this gachette is linked to")]
	[SerializeField]
	private GameObject canonShooter;
	
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
		grapped=false;
		uncocked=false;
	}
	
	void Update () {
		if(gameObject.GetComponent<Sticky>().v_numberOfLinks!=0){
			grapped=true;
			gameObject.GetComponent<Rigidbody>().isKinematic=false;
			canonBody.GetComponent<Rigidbody>().isKinematic=true;
		}else{
			grapped=false;
			//gameObject.GetComponent<Rigidbody>().isKinematic=true;
			canonBody.GetComponent<Rigidbody>().isKinematic=false;
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
					if(canonShooter.GetComponent<TurretShooting>().isCanon==true){
						canonShooter.SendMessage("Shoot");
					}
				}
			}else{
				//sinon, tu reviens vers le pt de départ
				gameObject.GetComponent<Rigidbody>().AddForce( (OriginalPlacement.position-transform.position).normalized*returnForce*Time.deltaTime);
			}
		}
	}
}
