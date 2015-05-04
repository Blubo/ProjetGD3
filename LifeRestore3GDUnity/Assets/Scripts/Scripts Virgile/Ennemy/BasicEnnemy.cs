using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicEnnemy : MonoBehaviour {

  public int Health;
  public float WalkSpeed;
  public float RushSpeed;
  public int AttackValue;
  public bool IsLeader;
  public float DistanceAllowed;
  public float RangeAttack;

  public List<Collider> _potentialTargets, _Targets, _CloseTargets;
  public float TimerCheckTarget;
  [HideInInspector]
  public float timerTemp;

  public Transform Target;
  public NavMeshAgent _Nav;

  public void Initiation()
  {
    if (gameObject.GetComponent<NavMeshAgent>() == null)
    {
      gameObject.AddComponent<NavMeshAgent>();
      _Nav = gameObject.GetComponent<NavMeshAgent>();
    }
  }

  //Lorsque l'ennemi se prend des dommages
  public void TakeDamage(){
    Health -= 1;
  }

  //A la mort de l'ennemi
  public void Death()
  {
   //Désactive les renderers etc 

    //Fait droper les collectibles

    //Active le ragdoll ? 

    //Si l'ennemy est dans un groupe alors le retire de ce groupe( par parent)
    if (gameObject.transform.parent != null)
    {
      Transform Parent =  gameObject.transform.parent;
      Parent.GetComponent<Group_AI>()._Composition.Remove(this);
      Parent.GetComponent<Group_AI>().ChooseLeader();
    }
    Destroy(gameObject);
  }

  //Drop de collectible
  private void DropCollectible()
  {
    //Drop les collectibles 

    //Détruit le gameobject ? 
  }

  //Attaque de base 
  public IEnumerator Attack(int Value)
  {
    Collider[] Attacked = Physics.OverlapSphere(transform.position + transform.forward , 1.0f);
    //Faire les effets et toussa
    yield return new WaitForSeconds(2);
    //On laisse le temps au joueur de réagir
    //L'attaque se passe
    for (int i = 0; i < Attacked.Length; i++)
    {
      if (Attacked[i].gameObject.tag == "Player")
      {
        Debug.Log("Player receive damage");
      }

      if (Attacked[i].gameObject.tag == "Idole")
      {
        Debug.Log("Idole Receive Damage");
      }
    }
    StopCoroutine("Attack");
  }

  private void OnDrawGizmos(){
      Gizmos.color = Color.red;
     //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
      Gizmos.DrawWireSphere(transform.position + transform.forward, 1.0f);
  }
}