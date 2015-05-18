using UnityEngine;
using System.Collections;

public class ActivatedDestroy : MonoBehaviour {

	void Start(){
	}

	void Activated(){
		Destroy(gameObject);
	}

	void Deactivated(){
		
	}
}
