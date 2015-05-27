using UnityEngine;
using System.Collections;

public class OpenTheDoor : MonoBehaviour {

	[HideInInspector]
	public bool receivedOrderToOpen = false, receivedOrderToClose = false, openedAlready = false, closedAlready = false;

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
	private ParticleSystem nuagePorteParticles;

	private float openingTimer, closingTimer;

	// Use this for initialization
	void Start () {
		closedAlready=true;
		openingTimer = 0f;
		closingTimer = 0f;
		nuagePorteParticles = nuagePorte.GetComponent<ParticleSystem>();
		nuagePorteParticles.playOnAwake = true;
		nuagePorte.GetComponent<Renderer>().enabled = false;
		initPos = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(receivedOrderToOpen==true){
			closedAlready = false;

			PleaseOpenDoor();
		}

		if(receivedOrderToClose==true){
			openedAlready = false;

			PleaseCloseDoor();
		}
		if(gameObject.transform.position != _closedLocation.transform.position) closedAlready=false;


	}

	void PleaseOpenDoor(){
		//SON FMOD ICI POUR L OUVERTURE DE LA PORTE
		//LE SON FAIT N IMPORTE QUOI!!!
//		Camera.main.GetComponent<SoundManagerHeritTest>().PlaySoundOneShot("Porte ouverture");

//		gameObject.transform.position = Vector3.Lerp(gameObject.transform.position , _openedLocation.transform.position, Time.deltaTime*doorSpeed);
		openingTimer+=Time.deltaTime;
//		gameObject.transform.position = Vector3.Lerp(_closedLocation.transform.position , _openedLocation.transform.position, Time.deltaTime*doorSpeed);
		nuagePorte.GetComponent<Renderer>().enabled = true;
		openingTimer+=Time.deltaTime;
		gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position,_openedLocation.transform.position, doorSpeed*Time.deltaTime);

		if(gameObject.transform.position == _openedLocation.transform.position){
//		if(openingTimer>=doorSpeed){
			openedAlready = true;
			openingTimer = 0f;
			nuagePorte.GetComponent<Renderer>().enabled = false;

			receivedOrderToOpen=false;
		}
	}

	void PleaseCloseDoor(){
		//SON FMOD ICI POUR L OUVERTURE DE LA PORTE
		//LE SON FAIT N IMPORTE QUOI!!!

//		Camera.main.GetComponent<SoundManagerHeritTest>().PlaySoundOneShot("Porte ouverture");
		closingTimer+=Time.deltaTime;

		nuagePorte.GetComponent<Renderer>().enabled = true;
//		gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, _closedLocation.transform.position, Time.deltaTime*doorSpeed);
//		gameObject.transform.position = Vector3.Lerp(_openedLocation.transform.position, _closedLocation.transform.position, Time.deltaTime*doorSpeed);
		gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position,_closedLocation.transform.position, doorSpeed*Time.deltaTime);
		if(gameObject.transform.position == _closedLocation.transform.position){
//		if(closingTimer>=doorSpeed){
			closedAlready = true;

			closingTimer = 0f;
			nuagePorte.GetComponent<Renderer>().enabled = false;

			receivedOrderToClose=false;
//			Debug.Log("2");
		}
	}

	void Activated(){
//		Debug.Log("open");
//		Camera.main.GetComponent<SoundManagerHeritTest>().PlaySoundOneShot
		receivedOrderToOpen=true;
		receivedOrderToClose = false;
	}

	void Deactivated(){
//		Debug.Log("close");
		receivedOrderToClose=true;
		receivedOrderToOpen = false;
	}


	IEnumerator MoveObject (Transform thisTransform, Vector3 startPos, Vector3 endPos, float time){
		float i = 0;
		float rate = 1/time;
		while (i < 1) {
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp(startPos, endPos, i);
			yield return null; 
		}
	}

}
