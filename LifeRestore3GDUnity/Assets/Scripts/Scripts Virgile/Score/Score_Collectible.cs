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
        if (_collision.gameObject.tag == "Player")
        {
            string _name = _collision.gameObject.name;
            _ScoreManager.Increase_score(_name, _value*_Multiplicator);
            //Destruction du collectible après le calcul 
            Destroy(gameObject);
        }
    }
}
