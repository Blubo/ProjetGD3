using UnityEngine;
using System.Collections;

public class AnimationOnly : MonoBehaviour {

  private Animator _Anim;

	void Start () {
    //Animator 
    _Anim = transform.GetComponentInChildren<Animator>();
    _Anim.Play("Animation Idle Crocmagnon");
	}
}
