using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicEnnemy : MonoBehaviour {

  public int Health;
  public float WalkSpeed;
  public float RushSpeed;
  public int AttackValue;
  public bool IsLeader, IsAttacking, _InDanger;
  public float DistanceAllowed, ZoneDanger;
  public float RangeAttack, AtkSphereRange;
  public int _DelaiAtk, CDAtk;

  public List<Collider> _potentialTargets, _Targets, _CloseTargets;
  public float TimerCheckTarget;
  [HideInInspector]
  public float timerTemp;

  public Block_SpawnCollectible _SpawnerCollectible;
  public GameObject _Ragdoll, _TargetFurie;

  public Transform Target;
  public NavMeshAgent _Nav;
  [HideInInspector]
  public Animator _Anim;

  public bool Furie;

  //**Only for ingé **
  int Progression = -1;
  public GameObject _Bombe, _Prefab;
  public Transform _BombePlacement;
  [HideInInspector]
  public Vector3 _targetposition;

  public bool _locked, IsRunning;
  //***---***
  public void Initiation()
  {

    if (gameObject.GetComponent<SphereCollider>() != null)
    {
      SphereCollider _ZoneDanger = gameObject.GetComponent<SphereCollider>();
      _ZoneDanger.radius = ZoneDanger;
      if (!_ZoneDanger.isTrigger)
      {
        _ZoneDanger.isTrigger = true;
      }
    }

    _SpawnerCollectible = gameObject.GetComponent<Block_SpawnCollectible>();

    if (gameObject.GetComponent<NavMeshAgent>() == null)
    {
      gameObject.AddComponent<NavMeshAgent>();
      _Nav = gameObject.GetComponent<NavMeshAgent>();
    }

    IsAttacking = false;
  }

  //Lorsque l'ennemi se prend des dommages
  public void TakeDamage(int ValueDamageTaken){
    Health -= ValueDamageTaken;
  }

  //A la mort de l'ennemi
  public void Death()
  {
    gameObject.GetComponent<NavMeshAgent>().enabled = false;
    //Désactiver ce qu'il faut
    gameObject.GetComponent<BoxCollider>().enabled = false;
   //Faire les effets FX etc

    //Fait droper les collectibles
    DropCollectible();
    //Fait pop l'objet Ragdoll/Cadavre
    if (_Ragdoll != null)
    {
      Instantiate(_Ragdoll, transform.position+transform.up, Quaternion.identity);
    }
    //Si l'ennemy est dans un groupe alors le retire de ce groupe( par parent)
    if (gameObject.transform.parent!= null && gameObject.transform.parent.GetComponent<Group_AI>() != null)
    {
      Transform Parent =  gameObject.transform.parent;
      Parent.GetComponent<Group_AI>()._Composition.Remove(this);
    }
    //Enfin on détruit l'objet
    Destroy(gameObject);
  }

  //Drop de collectible
  private void DropCollectible()
  {
    //Drop les collectibles 
    _SpawnerCollectible.SpawnCollectible();
  }

  //Attaque de base 
  public IEnumerator Attack(int Value)
  {
    Collider[] Attacked = Physics.OverlapSphere(transform.position + transform.forward, AtkSphereRange);
    //Faire les effets et toussa
    yield return new WaitForSeconds(_DelaiAtk);
    //On laisse le temps au joueur de réagir
    //L'attaque se passe
    for (int i = 0; i < Attacked.Length; i++)
    {
      if (Attacked[i] != null && Attacked[i].gameObject.tag == "Player")
      {
        Attacked[i].gameObject.SendMessage("TakeDamage", transform.position);
      }

      if (Attacked[i] != null && Attacked[i].gameObject.tag == "Idole")
      {
        Attacked[i].gameObject.SendMessage("TakeDamage", Value);
      }
    }
    StopCoroutine("Attack");
  }

  public IEnumerator AttackInge(GameObject Bombe)
  {
    IsRunning = true;
    yield return new WaitForSeconds(0.0f);
    Bombe.transform.parent = null;
    //Position de la target à viser 
    Transform LandingPoint = Target;
    //Position du point entre les deux 
    Vector3 MidPoint = new Vector3((_targetposition.x + transform.position.x) / 2.0f, (_targetposition.y + transform.position.y) / 2.0f, (_targetposition.z + transform.position.z) / 2.0f);
    MidPoint += Vector3.up * 8.0f;
    //Lancement de la Bombe entre les points
    if (Vector3.Distance(Bombe.transform.position, transform.position) < 3.0f)
    {
      Progression = 0;
    }
    if (Progression == 0)
    {
      //Point A>B 
      Bombe.transform.position = Vector3.MoveTowards(Bombe.transform.position, MidPoint, 1.0f);
      if (Vector3.Distance(Bombe.transform.position, MidPoint) < 1.0f)
      {
        Progression = 1;
      }
    }else if (Progression == 1)
    {
      //Point B>C
      Bombe.transform.position = Vector3.MoveTowards(Bombe.transform.position, _targetposition, 1.0f);
    }

    if(Vector3.Distance(Bombe.transform.position, _targetposition)<1.0f){
      _Bombe = null;
      Bombe.GetComponent<BoxCollider>().isTrigger = false;
      Rigidbody _BombeRigi = Bombe.GetComponent<Rigidbody>();
      _BombeRigi.useGravity = enabled;
      _BombeRigi.isKinematic = false;

      _locked = false;
      StopCoroutine("AttackInge");
    }
    //
    IsRunning = false;
  }
}