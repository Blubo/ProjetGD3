using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {
	
	public AudioSource zik;
	
	// Use this for initialization
	void Start () {
		zik.GetComponent<AudioSource>().Play();

	}
	
	// Update is called once per frame
	void Update () {

	}
}
