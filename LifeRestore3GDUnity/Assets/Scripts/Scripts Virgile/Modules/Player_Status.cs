using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player_Status : MonoBehaviour {

    [SerializeField]
    public float _Life;
    public GameObject _Idole;
    private int _LifeInt;

    [SerializeField]
    private float _TimerDeath, _Maxlife, _OriginalMoveSpeed, _MaxTimerDeath, _TimerInvincible;

    private bool _Desactivated;
    private GameManager _GameManager;
	private ScoreManager _ScoreManager;

	[SerializeField]
	private int playerNumber;

	[SerializeField]
	private int damage, numberToDrop;

	[SerializeField]
	private GameObject collectibleToDrop;

	void Start () {
        _Desactivated = false;
        _OriginalMoveSpeed = gameObject.GetComponent<MovementScript5Janv>().v_movementSpeed;
		_ScoreManager = Camera.main.GetComponent<ScoreManager>();
        _GameManager = Camera.main.GetComponent<GameManager>();
	}
	

	void Update () {
        //Passage de la vie en Int
        _LifeInt = (int)_Life;

        if (_Life>_Maxlife)
        {
            _Life = _Maxlife;
        }

        //Si la vie tombe à zero 
	    if(_LifeInt<= 0){
            Deactivate();
        }
        else if (_LifeInt > 0 && _Desactivated ==true)
        {
            Activate();
        }


	}

    public void InAura() {
        _Life += 1.0f * Time.deltaTime;
    }

    void Deactivate() {
        //
        _Desactivated = true;
        //L'avatar ne peut pas bouger mais peut quand même tirer des liens (?)
        gameObject.GetComponent<MovementScript5Janv>().v_movementSpeed = 0.0f;
        //Pas de dash non plus
       // gameObject.GetComponent<Dash>().enabled = false;
        //Décompte jusqu'à la mort 
        _TimerDeath -= 1.0f * Time.deltaTime;
        if(_TimerDeath <=0.0f){
            Respawn();
        }
        //Mort direct si le joueur appuie sur une touche
    }

    void Activate(){
        gameObject.GetComponent<MovementScript5Janv>().v_movementSpeed = _OriginalMoveSpeed;
        //gameObject.GetComponent<Dash>().enabled = true;
        //gameObject.GetComponentInChildren<Renderer>().enabled = true;
        _Desactivated = false;
    }

    public void TakeDamage()
    {
		DropCollectible();
		switch (playerNumber) {
		case 1:
			_ScoreManager.Score_Vert-=damage;
			break;
		case 2:
			_ScoreManager.Score_Rouge-=damage;

			break;
		case 3:
			_ScoreManager.Score_Bleu-=damage;
			break;
		default:
			break;
		}

      StartCoroutine("Clignotement");
    }

    void Respawn() { 
        //Disparition du joueur 
        gameObject.GetComponentInChildren<Renderer>().enabled = false;
        //Replacement à coté de l'idole
        gameObject.transform.position = _Idole.transform.position + new Vector3(1.0f, 0.0f, 0.0f);

        StartCoroutine("Clignotement");
        //Remise à niveau des stats du joueur
        _Life = _Maxlife;
        _TimerDeath = _MaxTimerDeath;
        //Perte de vie des joueurs en général
        _GameManager._TeamLifes -= 1;
        //Clignotement + invincibilité ? 
    }

    public IEnumerator Clignotement()
    {
        for (int i = 0; i < 2; i++)
        {
         Renderer[] renderer = gameObject.transform.Find("Avatar").GetComponentsInChildren<MeshRenderer>() ;
       /*  foreach (Renderer rend in renderer)
         {
           rend.enabled = !rend.enabled;
           yield return new WaitForSeconds(0.0f);
           rend.enabled = true;
         }*/

         foreach (Renderer rend in renderer)
         {
           rend.enabled = false;
         }
         yield return new WaitForSeconds(0.0f);

         foreach (Renderer rend in renderer)
         {
           rend.enabled = true;
         }

          //gameObject.GetComponentInChildren<Renderer>().enabled = !gameObject.GetComponentInChildren<Renderer>().enabled;
          
        //    yield return new WaitForSeconds(0.1f);
        //    gameObject.transform.Find("Avatar").GetComponentInChildren<MeshRenderer>().enabled = true;
        }
    }

    public IEnumerator Invincible()
    {
        GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(_TimerInvincible);
        GetComponent<BoxCollider>().enabled = true;
    }

	void DropCollectible(){
		for (int i = 0; i < numberToDrop; i++) {
			GameObject dropped = Instantiate(collectibleToDrop, new Vector3(gameObject.transform.position.x+Random.Range(-2.0f, 2.0f), gameObject.transform.position.y, gameObject.transform.position.z+Random.Range(-2.0f, 2.0f)), collectibleToDrop.transform.rotation)as GameObject;
		}
	}

}