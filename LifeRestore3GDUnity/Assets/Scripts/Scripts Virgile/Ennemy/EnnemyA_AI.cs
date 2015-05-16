using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnnemyA_AI : BasicEnnemy {

	void Start () {
    Health = 1;
    WalkSpeed = 4.0f;
    RushSpeed = 6.0f;
    //distance max que à laquelle les ennemis peuvent aller depuis le leader
    DistanceAllowed = 5.0f;
    //Zone dans laquelle le joueur est attaqué priotairement
    ZoneDanger = 5.0f;
    RangeAttack = 2.0f;
    _DelaiAtk = 2;
    AtkSphereRange = 1.0f;

    //Animator 
    _Anim = transform.GetComponentInChildren<Animator>();

    TimerCheckTarget = 0.0f;
    timerTemp = 2.0f;

    Initiation();
    //
	}
	
	void Update () {
    if (Furie)
    {
      Rush(_TargetFurie.transform);
    }
    //Mort de l'ennemi
    if(Health <= 0)
    {
      Death();
    }
    //Si l'ennemi est un leader 
    if (IsLeader)
    {
      //Target
      UpdateTargets();
      if (TimerCheckTarget > 0)
      {
        TimerCheckTarget -= 1.0f * Time.deltaTime;
      }
      else if (TimerCheckTarget <= 0)
      {
        CheckForTargets();
        TimerCheckTarget = timerTemp;
      }
    }

    //Si l'ennemi n'est pas le leader
    if (!IsLeader)
    {
      if(Vector3.Distance(gameObject.transform.position, transform.parent.GetComponent<Group_AI>()._Leader.transform.position) >= DistanceAllowed){
        FindLeader();
        Target = null;
      }

    }

    //Si la target a été retrouvé 
    if (Target != null)
    {
      transform.LookAt(Target);
      Rush(Target);
    }
    else { Wait(); }
	}

  //En attendant de trouver une target
  void Wait()
  {
    if (IsLeader && !Furie)
    {
      _Nav.ResetPath();
      //Faire anim idle
      _Anim.Play("Animation Idle Crocmagnon");
    }
    else if (!IsLeader && Vector3.Distance(gameObject.transform.position, transform.parent.GetComponent<Group_AI>()._Leader.transform.position) > 1.0f)
    {
      if (Vector3.Distance(gameObject.transform.position, transform.parent.GetComponent<Group_AI>()._Leader.transform.position) > DistanceAllowed && Vector3.Distance(gameObject.transform.position, transform.parent.GetComponent<Group_AI>()._Leader.transform.position) > 1.0f)
      {
        FindLeader();
      }
      else
      {    
        //Anim idle
        _Anim.Play("Animation Idle Crocmagnon");
        //
        _Nav.ResetPath();
      }
    }
  }

  //Fonce sur la target
  public void Rush(Transform Target)
  {
     _Nav.destination = Target.position;
     _Nav.speed = RushSpeed;

    if(Vector3.Distance(transform.position, Target.position)<= RangeAttack){
      _Nav.ResetPath();
      //
      _Anim.Play("Animation Attaque Crocmagnon");
      StartCoroutine("Attack", AttackValue);
    }
  }

  //Zone de détection
  void CheckForTargets()
  {
    _potentialTargets = new List<Collider>(Physics.OverlapSphere(transform.position, 20.0f));
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
      if (TimerCheckTarget <1.0f)
      {
        _Targets = _potentialTargets;
      }
  }

  //Retrouver le chemin le leader
  void FindLeader()
  {
    //
    _Anim.Play("Animation Deplacement Crocmagnon");
    transform.LookAt(transform.parent.GetComponent<Group_AI>()._Leader.transform);

    _Nav.SetDestination(transform.parent.GetComponent<Group_AI>()._Leader.transform.position);
    _Nav.speed = RushSpeed;
  }

  //DangerZone
 /* void OnTriggerStay(Collider _colli)
  {
    if (IsLeader)
    {
      if (!_InDanger)
      {
        if (_colli.gameObject.tag == "Player" || _colli.gameObject.tag == "Idole")
        {
          Target = _colli.transform;
          _InDanger = true;
        }
      }

      if (_InDanger)
      {
        if (_colli.gameObject.tag != "Player" && _colli.gameObject.tag != "Idole")
        {
          UpdateTargets();
          _InDanger = false;
        }
      }
    }
  }*/
}
