using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnnemyC_Ai : BasicEnnemy
{

  void Start()
  {
    Health = 5;
    WalkSpeed = 2.0f;
    RushSpeed = 5.0f;
    //
    DistanceAllowed = 15.0f;
    RangeAttack = 2.0f;
    _DelaiAtk = 5;
    AtkSphereRange = 1.0f;


    TimerCheckTarget = 2.0f;
    timerTemp = 2.0f;

    Initiation();
    //Animator 
    _Anim = transform.GetComponentInChildren<Animator>();
  }

  void Update()
  {
    //Tentative pour trouver une target 
    if (TimerCheckTarget > 0)
    {
      TimerCheckTarget -= 1.0f * Time.deltaTime;
    }
    else if (TimerCheckTarget <= 0)
    {
      CheckForTargets();
      TimerCheckTarget = timerTemp;
    }

    UpdateTargets();
    //Mort de l'ennemi
    if (Health <= 0)
    {
      Death();
    }

    //Si la target a été retrouvé 
    if (Target != null)
    {
      Rush(Target);
    }
    else { Wait(); }

    //
    if(_potentialTargets.Count>0){
      Target = _potentialTargets[0].transform;
    }
    else { Target = null; }
  }

  //En attendant de trouver une target
  void Wait()
  {
    //Rester sur place
    _Nav.destination = transform.position;
    //Jouer L'animation idle
    _Anim.Play("Idle_Coloss");
  }

  //Fonce sur la target
  public void Rush(Transform Target)
  {

    if (Vector3.Distance(transform.position, Target.position) <= RangeAttack)
    {
      _Nav.destination = transform.position;
      StartCoroutine("Attack", AttackValue);
      _Anim.Play("Deplacement_Coloss_3");
    }
    else
    {
     // _Anim.Play("Animation Deplacement Barak");
      _Nav.destination = Target.position;
      _Nav.speed = WalkSpeed;

    }
  }

  //Zone de détection
  void CheckForTargets()
  {
    _potentialTargets = new List<Collider>(Physics.OverlapSphere(transform.position, 20.0f));
    UpdateTargets();
  }

  void UpdateTargets()
  {
    for (int i = 0; i < _potentialTargets.Count; i++)
    {
      if (_potentialTargets[i].gameObject.tag != "Player" && _potentialTargets[i].gameObject.tag != "Idole")
      {
        _potentialTargets.Remove(_potentialTargets[i]);
      }
    }
    if (TimerCheckTarget < 1.0f)
    {
      _Targets = _potentialTargets;
    }
  }
}
