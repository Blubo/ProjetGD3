using UnityEngine;
using System.Collections;

public class OpenTheDoor : MonoBehaviour {

	[HideInInspector]
	public bool receivedOrderToOpen = false, receivedOrderToClose = false;
	private bool openedAlready = false, closedAlready = false;

	[Tooltip("To where will the door open")]
	[SerializeField]
	private GameObject _openedLocation;

	[Tooltip("To where will the door close")]
	[SerializeField]
	private GameObject _closedLocation;

	[Tooltip("How fast will the door open")]
	[SerializeField]
	private float doorSpeed;

	private Vector3 initPos;

	[SerializeField]
	private GameObject nuagePorte;

	// Use this for initialization
	void Start () {
		initPos = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(receivedOrderToOpen==true){
			PleaseOpenDoor();
		}

		if(receivedOrderToClose==true){
			PleaseCloseDoor();
		}

//		if(openedAlready==true){
//			receivedOrderToOpen=false;
//		}
//
//		if(closedAlready==true){
//			receivedOrderToClose=false;
//		}
	}

	void PleaseOpenDoor(){
		//SON FMOD ICI POUR L OUVERTURE DE LA PORTE
		//LE SON FAIT N IMPORTE QUOI!!!
		Camera.main.GetComponent<SoundManagerHeritTest>().PlaySoundOneShot("Porte ouverture");


		gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, _openedLocation.transform.position, Time.deltaTime*doorSpeed);
		if(gameObject.transform.position == _openedLocation.transform.position){
//			openedAlready = true;
//			closedAlready = false;
			receivedOrderToOpen=false;
//			Debug.Log("1");
		}
	}

	void PleaseCloseDoor(){
		//SON FMOD ICI POUR L OUVERTURE DE LA PORTE
		//LE SON FAIT N IMPORTE QUOI!!!

		Camera.main.GetComponent<SoundManagerHeritTest>().PlaySoundOneShot("Porte ouverture");
		
		
		gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, _closedLocation.transform.position, Time.deltaTime*doorSpeed);
		if(gameObject.transform.position == _closedLocation.transform.position){
//			closedAlready = true;
//			openedAlready = false;

			receivedOrderToClose=false;
//			Debug.Log("2");
		}
	}

	void Activated(){
//		Debug.Log("open");
		receivedOrderToOpen=true;
		if(receivedOrderToClose==true) receivedOrderToClose = false;
	}

	void Deactivated(){
//		Debug.Log("close");
		receivedOrderToClose=true;
		if(receivedOrderToOpen==true) receivedOrderToOpen = false;

	}
}
