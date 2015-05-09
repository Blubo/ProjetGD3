﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Group_AI : MonoBehaviour {

  //Le ou les individus composants le groupe
  public List<BasicEnnemy> _Composition;
  //Leader du groupe (le centre)
  public BasicEnnemy _Leader;

  void Start()
  {
    ChooseLeader();
  }

	void Update () {
    if (_Leader == null)
    {
      ChooseLeader();
    }

    if (_Leader._Targets.Count > 0)
    {
      FindTarget();
    }
    else
    {
      ForgetTarget();
    }
	}
  //Cherche et envoie les infos sur la target que le groupe doit viser
  void FindTarget()
  {
      for (int i = 0; i < _Composition.Count; i++)
      {
        if (_Leader._Targets[0].transform != null)
        {
          _Composition[i].Target = _Leader._Targets[0].transform;
        }
      }
  }

  void ForgetTarget()
  {
    for (int i = 0; i < _Composition.Count; i++)
    {
        _Composition[i].Target = null;
    }
  }

  //Définit l'un des éléments du groupe comme le "chef" du groupe
  public void ChooseLeader()
  {
    //Cherche pour l'élément le plus haut gradé dans le groupe
    for (int i = 0; i < _Composition.Count; i++)
    {
      if (_Composition[i] is EnnemyB_AI && _Leader == null)
      {
        _Leader = _Composition[i];
      }
    }
    //Si aucun des éléments n'était un haut gradé on prend un des faibles
    if (_Leader == null)
    {
      _Leader = _Composition[0];
    }

    PrepareLeader();
  }

  //Place les zones de détection sur l'élément leader
  void PrepareLeader()
  {
    _Leader.IsLeader = true;
    _Leader.GetComponent<SphereCollider>().enabled = true;
  }
}
