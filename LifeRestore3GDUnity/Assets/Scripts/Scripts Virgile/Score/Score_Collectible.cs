using UnityEngine;
using System.Collections;

public class Score_Collectible : MonoBehaviour {

    [SerializeField]
    private float _value;

    private ScoreManager _ScoreManager;

    void Awake()
    {
        _ScoreManager = Camera.main.GetComponent<ScoreManager>();
    }

    void OnCollisionEnter(Collision _collision)
    {
        Debug.Log("1");
        if (_collision.gameObject.tag == "Player")
        {
            Debug.Log("2");
            string _name = _collision.gameObject.name;
            _ScoreManager.Increase_score(_name, _value);
            //Destruction du collectible après le calcul 
            Destroy(gameObject);
        }
    }
}
