using UnityEngine;
using System.Collections;

public class OpenTheDoor : MonoBehaviour {

	[HideInInspector]
	public bool receivedOrderToOpen = false;
	private bool openedAlready = false;

	[Tooltip("To where will the door open")]
	[SerializeField]
	private GameObject _openedLocation;

	[Tooltip("How fast will the door open")]
	[SerializeField]
	private float doorSpeed;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(receivedOrderToOpen==true){
			PleaseOpenDoor();
		}

		if(openedAlready==true){
			receivedOrderToOpen=false;
		}
	}

	void PleaseOpenDoor(){
		//SON FMOD ICI POUR L OUVERTURE DE LA PORTE
		Camera.main.GetComponent<SoundManagerHeritTest>().PlaySoundOneShot("Porte ouverture");


		gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, _openedLocation.transform.position, Time.deltaTime*doorSpeed);
		if(gameObject.transform.position == _openedLocation.transform.position){
			openedAlready = true;
//			Debug.Log("fdp2");
		}
	}

	void Activated(){
		receivedOrderToOpen=true;
	}
}
