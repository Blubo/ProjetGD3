using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnnemyA_AI : BasicEnnemy {

	void Start () {
    Health = 1;
    WalkSpeed = 5.0f;
    RushSpeed = 7.0f;
    DistanceAllowed = 15.0f;
    RangeAttack = 2.0f;

    TimerCheckTarget = 0.0f;
    timerTemp = 2.0f;

    Initiation();
	}
	
	void Update () {
    //Mort de l'ennemi
    if(Health <= 0)
    {
      Death();
    }
    //Si l'ennemi est un leader 
    if (IsLeader)
    {
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
      Rush(Target);
    }
    else { Wait(); }
	}

  //En attendant de trouver une target
  void Wait()
  {
    //gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    if (IsLeader)
    {
      _Nav.destination = transform.position;
    }
    else if (!IsLeader && Vector3.Distance(gameObject.transform.position, transform.parent.GetComponent<Group_AI>()._Leader.transform.position) < DistanceAllowed)
    {
      _Nav.destination = transform.parent.GetComponent<Group_AI>()._Leader.transform.position;
    }
  }

  //Fonce sur la target
  public void Rush(Transform Target)
  {
     _Nav.destination = Target.position;
     _Nav.speed = WalkSpeed;

    if(Vector3.Distance(transform.position, Target.position)<= RangeAttack){
      _Nav.destination = transform.position;
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
    _Nav.SetDestination(transform.parent.GetComponent<Group_AI>()._Leader.transform.position);
    _Nav.speed = RushSpeed;
  }
}
