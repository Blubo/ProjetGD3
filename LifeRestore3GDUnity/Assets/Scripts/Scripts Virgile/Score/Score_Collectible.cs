using UnityEngine;
using System.Collections;

public class Score_Collectible : MonoBehaviour {

    [SerializeField]
    private int _value, _Multiplicator;

    private ScoreManager _ScoreManager;

    void Awake()
    {
        _ScoreManager = Camera.main.GetComponent<ScoreManager>();
        gameObject.transform.localScale = gameObject.transform.localScale+new Vector3(_Multiplicator, _Multiplicator, _Multiplicator);

    }

    void OnCollisionEnter(Collision _collision)
    {

		//SI CE COLLECTIBLE EST TOUCHE PAR UN JOUEUR
        if (_collision.gameObject.tag == "Player")
        {
			//ALORS ON AUGMENTE LE SCORE DU JOUEUR DE "VALUE"
            string _name = _collision.gameObject.name;
            _ScoreManager.Increase_score(_name, _value*_Multiplicator);
            //Destruction du collectible après le calcul 

			//ET ON JOUE LE SON DE COLLECTE AVANT DE DETRUIRE LE COLLECTIBLE



			//ON DETRUIT MAINTENANT LE COLLECTIBLE
            Destroy(gameObject);
        }
    }
}
