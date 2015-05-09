using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//Ingénieur
public class EnnemyD_AI : BasicEnnemy
{
  bool _locked = false;
  void Start()
  {
    //On dit que la première bombe est celle déjà placée sur la tête
    //_Bombe = transform.Find("")

    Health = 5;
    WalkSpeed = 2.0f;
    RushSpeed = 5.0f;

    DistanceAllowed = 15.0f;
    RangeAttack = 2.0f;
    _DelaiAtk = 3;
    AtkSphereRange = 1.0f;

    TimerCheckTarget = 2.0f;
    timerTemp = 2.0f;

    Initiation();


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

    //est ce qu'un joueur est proche de moi 


    //Est ce que j'ai la bombe
    if (_Bombe == null)
    {
      ReloadBomb();
    }

    //Si la target a été retrouvé 
    if (Target != null)
    {
      _Nav.ResetPath();
      //Animation De bombe 
      //Beware Below
      if (_Bombe != null)
      {
        StartCoroutine("AttackInge", _Bombe);
      }
    }
    else { Wait(); }
    if (_potentialTargets.Count > 0 && _locked == false)
    {
      Target = _potentialTargets[0].transform;
      _targetposition = _potentialTargets[0].transform.position;
      _locked = true;
    }
    else if (_potentialTargets.Count ==0) { Target = null; }
  }

  //En attendant de trouver une target
  void Wait()
  {
    //Rester sur place
    _Nav.destination = transform.position;
    //Jouer L'animation idle
  }

  //refaire une bombe et la faire associer au joueur
  void ReloadBomb()
  {
    GameObject newBomb =  Instantiate(_Prefab, _BombePlacement.position, Quaternion.identity) as GameObject;
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
    _potentialTargets = new List<Collider>(Physics.OverlapSphere(transform.position, 50.0f));
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
