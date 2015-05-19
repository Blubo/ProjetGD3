using UnityEngine;
using System.Collections;

public class DoorConditionSign : MonoBehaviour {

	[SerializeField]
	private float viewRange;

	[SerializeField]
	private LayerMask myDetectionLayer;

	[SerializeField]
	private GameObject indication;
	private Renderer indicationRenderer;

	private OpenTheDoor myOpenTheDoor;

	// Use this for initialization
	void Start () {
		myOpenTheDoor = GetComponent<OpenTheDoor>();
		indicationRenderer = indication.GetComponent<Renderer>();
		indicationRenderer.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {

		if(Detection() == true){
			if(myOpenTheDoor.closedAlready == true){
				indicationRenderer.enabled =true;
			}else{
				indicationRenderer.enabled =false;
			}
		}else{
			indicationRenderer.enabled =false;
		}
	}

	bool Detection(){
		Collider[] colliders = Physics.OverlapSphere(transform.position, viewRange, myDetectionLayer);
		foreach(Collider find in colliders){

			if(find != null){
				return true;
			}
		}
		return false;
	}

	void ChangeRendererState(Renderer rend){
		rend.enabled =!rend.enabled;
	}

}
