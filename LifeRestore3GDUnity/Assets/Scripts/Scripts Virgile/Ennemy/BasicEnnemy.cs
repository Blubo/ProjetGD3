using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicEnnemy : MonoBehaviour {

  public Sticky _MySticky;
  private BasicEnnemy _MyType;
  public SoundManagerHeritTest _Sound;

  public int Health;
  public float WalkSpeed;
  public float RushSpeed;
	public int AttackValue, playerDamage;
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

	public GameObject attackEffect;
	public Transform _instantiateur;
	protected float internalTimer;
	protected bool activatedAttack = false;

  //**Only for ingé **
  public bool _SoundTir;
  int Progression = -1;
  public GameObject _Bombe, _Prefab;
  public Transform _BombePlacement;
  [HideInInspector]
  public Vector3 _targetposition;

	[SerializeField]
	protected GameObject destructionAnimation;

  public bool _locked, IsRunning;
  //***---***
  public void Initiation()
  {
    _Sound = Camera.main.GetComponent<SoundManagerHeritTest>();
    _MyType = gameObject.GetComponent<BasicEnnemy>();

    if (gameObject.GetComponent<Sticky>() != null)
    {
      _MySticky = gameObject.GetComponent<Sticky>();
    }
    
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

    if (Time.timeSinceLevelLoad > 1.0f)
    {
      PlaySoundOnSpawn();
    }

    IsAttacking = false;
  }

  //Lorsque l'ennemi se prend des dommages
  public void TakeDamage(int ValueDamageTaken){
    Health -= ValueDamageTaken;
    if (_MyType is EnnemyB_AI)
    {
      _Sound.PlaySoundOneShot("Ennemi barak blessure");
    }
    if (_MyType is EnnemyD_AI)
    {
      _Sound.PlaySoundOneShot("Ennemi barak blessure");
      _Nav.speed = 1.0f;
    }
  }

  private void PlaySoundOnSpawn()
  {
    if (_MyType is EnnemyA_AI)
    {
      _Sound.PlaySoundOneShot("Ennemi standard spawn");
    }
    else
    {
      if (_MyType is EnnemyB_AI)
      {
        _Sound.PlaySoundOneShot("Ennemi barak spawn");
      }
      else
      {
        if (_MyType is EnnemyD_AI)
        {
          _Sound.PlaySoundOneShot("Ennemi ingenieur spawn");
        }
      }
    }
  }

  private void PlaySoundOnDeath(){
    if (_MyType is EnnemyA_AI)
    {
      _Sound.PlaySoundOneShot("Ennemi standart mort");
    }else
    {
      if (_MyType is EnnemyB_AI)
      {
        _Sound.PlaySoundOneShot("Ennemi barak mort");
      }
      else
      {
        if (_MyType is EnnemyD_AI)
        {
          _Sound.PlaySoundOneShot("Ennemi ingenieur mort");
        }
      }
    }
  }

  //A la mort de l'ennemi
  public void Death()
  {
    PlaySoundOnDeath();
    gameObject.GetComponent<NavMeshAgent>().enabled = false;
    //Désactiver ce qu'il faut
    gameObject.GetComponent<BoxCollider>().enabled = false;
   //Faire les effets FX etc
		if(destructionAnimation != null) Instantiate(destructionAnimation, gameObject.transform.position, Quaternion.AngleAxis(-90, Vector3.right));


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
//		if( attackEffect.GetComponent<ParticleSystem>().duration){
//			GameObject particule = Instantiate(attackEffect, _instantiateur.transform.position, Quaternion.identity )as GameObject;
//		}

    Collider[] Attacked = Physics.OverlapSphere(transform.position + transform.forward, AtkSphereRange);
    //Faire les effets et toussa
    yield return new WaitForSeconds(_DelaiAtk);
    //On laisse le temps au joueur de réagir
    //L'attaque se passe
    for (int i = 0; i < Attacked.Length; i++)
    {
      if (Attacked[i] != null && Attacked[i].gameObject.tag == "Player" && _instantiateur != null)
      {
				GameObject particule = Instantiate(attackEffect, _instantiateur.transform.position, Quaternion.identity )as GameObject;
				Attacked[i].gameObject.GetComponent<Player_Status>().TakeDamage(transform.position, playerDamage);

//				Attacked[i].gameObject.SendMessage("TakeDamage", transform.position, playerDamage);
      }

      if (Attacked[i] != null && Attacked[i].gameObject.tag == "Idole")
      {
				//GameObject particule = Instantiate(attackEffect, _instantiateur.transform.position, Quaternion.identity )as GameObject;

        Attacked[i].gameObject.SendMessage("TakeDamage", Value);
      }
    }
    StopCoroutine("Attack");
  }

  public IEnumerator AttackInge(GameObject Bombe)
  {
    IsRunning = true;
   // Bombe.transform.localScale = Vector3.Lerp(Bombe.transform.localScale, new Vector3(1f, 1f, 1f), 0.9f);
    yield return new WaitForSeconds(0.0f);
		if(Bombe!=null){
			if(Bombe.transform.parent!=null){
				Bombe.transform.parent = null;
			}
		}

    //Position du point entre les deux 
    Vector3 MidPoint = new Vector3((_targetposition.x + transform.position.x) / 2.0f, (_targetposition.y + transform.position.y) / 2.0f, (_targetposition.z + transform.position.z) / 2.0f);
    MidPoint += Vector3.up * 8.0f;
    //Lancement de la Bombe entre les points
    if (Vector3.Distance(Bombe.transform.position, transform.position) < 10.0f)
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
      _SoundTir = false;
      _Bombe = null;
      Bombe.GetComponent<SphereCollider>().isTrigger = false;
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