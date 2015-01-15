using UnityEngine;
using System.Collections;

public class SoundBlock : MonoBehaviour {

	public AudioSource _Song;
	private bool _playsong;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<Sticky> ().v_numberOfLinks != 0) {
			if (_Song.isPlaying == false) {
				_Song.Play ();
			}
		} else {
			_Song.Pause ();
		}
	}
}
