using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MyChain : MonoBehaviour {

	public int v_chainSize;
	public GameObject v_maillon;
	public List<Vector3> _nextPosArray = new List<Vector3>();

	// Use this for initialization
	void Start () {
//		_nextPosArray.Capacity=v_chainSize;
//
//		_nextPosArray[0]=Vector3.zero;
		_nextPosArray[0]=gameObject.transform.position;

//		_nextPosArray.Add(gameObject.transform.position);
		for (int i = 0; i < v_chainSize; i++) {
			GameObject newMaillon = Instantiate(v_maillon, _nextPosArray[i] , transform.rotation) as GameObject;
			//Rigidbody rb = newMaillon.GetComponent<Rigidbody>();
			newMaillon.name = "Capsule "+i;
			_nextPosArray.Add( newMaillon.transform.Find("AncreArriere").transform.position);
			_nextPosArray[i]=newMaillon.transform.Find("AncreArriere").transform.position;
			Debug.Log(i + "" + newMaillon.transform.Find("AncreArriere").transform.position);
		}
	}
}
