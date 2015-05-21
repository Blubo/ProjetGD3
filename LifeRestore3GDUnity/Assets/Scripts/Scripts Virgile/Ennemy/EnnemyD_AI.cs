using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//Ingénieur
public class EnnemyD_AI : BasicEnnemy
{
  private bool _FinishCoroutine = true;

  void Start()
  {

    Health = 5;
    WalkSpeed = 2.0f;
    RushSpeed = 5.0f;

    DistanceAllowed = 15.0f;
    RangeAttack = 5.0f;
    _DelaiAtk = 0;
    AtkSphereRange = 1.0f;

    TimerCheckTarget = 0.0f;
    timerTemp = 5.0f;

    Initiation();
    //Animator 
    _Anim = transform.GetComponentInChildren<Animator>();
  }

  void Update()
  {
   FindTarget();
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

    //est ce qu'un joueur est proche de moi 


    //Est ce que j'ai la bombe
    if (_Bombe == null)
    {
      _Anim.Play("Animation Idle Crocmagnon");
        ReloadBomb();
        StartCoroutine("WaitForThoseSecs", 2.0f);
    }

    //Si la target a été retrouvé 
    if (Target != null)
    {
      if (Target.tag == "Idole" || Target.tag == "Player")
      {
        _Nav.ResetPath();
        //Beware Below
        if (_Bombe != null && _FinishCoroutine == true)
        {
          _Anim.Play("Animation Lancer Ingé");
          StartCoroutine("AttackInge", _Bombe);
        }
      }
      else {
        _locked = false;
        CheckForTargets();
        Wait(); }
      }
  }

  //En attendant de trouver une target
  void Wait()
  {
    //Rester sur place
    _Nav.destination = transform.position;
    //Jouer L'animation idle
    _Anim.Play("Animation Idle Crocmagnon");
  }

  //refaire une bombe et la faire associer au joueur
  void ReloadBomb()
  {
    GameObject newBomb =  Instantiate(_Prefab, _BombePlacement.position, Quaternion.identity) as GameObject;
    newBomb.transform.parent = transform.Find("Ennemis_Ingé/Tête/Placement");
    //newBomb.transform.parent = transform.find("");
    _Bombe = newBomb;
  }

  //Fuir les gens 
  void Flee()
  {
    
  }

  //Zone de détection
  void CheckForTargets()
  {
    _potentialTargets = new List<Collider>(Physics.OverlapSphere(transform.position, 30.0f));
    UpdateTargets();
  }

  void UpdateTargets()
  {
    for (int i = 0; i < _potentialTargets.Count; i++)
    {
      if (_potentialTargets[i] != null)
      {
        if (_potentialTargets[i].gameObject.tag == "Player" || _potentialTargets[i].gameObject.tag == "Idole")
        {
          _Targets.Add(_potentialTargets[i]);
        }
      }
 
    }
  }

  void FindTarget()
  {
    if (_Targets.Count > 0 && _locked == false)
    {
      for (int i = 0; i < _Targets.Count; i++)
      {
        if (_Targets[i].gameObject.tag == "Idole")
        {
          Target = _Targets[i].transform;
          _targetposition = _Targets[i].transform.position;
          _locked = true;
        }
        else
        {
          Target = _Targets[0].transform;
          _targetposition = _Targets[0].transform.position;
          _locked = true;
        }
      }
    }
    else if (_Targets.Count == 0) { 
      Target = null;
      _locked = false;
    }
  }

  private IEnumerator WaitForThoseSecs(float Secs)
  {
    _FinishCoroutine = false;
    yield return new WaitForSeconds(Secs);
    _FinishCoroutine = true;
  }
}
