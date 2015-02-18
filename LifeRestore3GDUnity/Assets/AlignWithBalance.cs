using UnityEngine;
using System.Collections;

public class AlignWithBalance : MonoBehaviour {

	public GameObject v_balance;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.up = v_balance.transform.forward;
	}
}
