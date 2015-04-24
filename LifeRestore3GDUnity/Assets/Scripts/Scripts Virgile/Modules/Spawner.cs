using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    [SerializeField]
    private GameObject _ToSpawn;

    [SerializeField]
    private float _TimeToSpawn;
    private float _Timer;

    [SerializeField]
    private int _NumberToSpawn;

    [SerializeField]
    private bool _Activated, _needAura;

	[Tooltip("This spawner EnemiesManager")]
	public GameObject EnemiesManager;

	// Use this for initialization
	void Start () {
        _Timer = _TimeToSpawn;
	}
	
	// Update is called once per frame
	void Update () {
        _Timer -= 1.0f * Time.deltaTime;

		if(_needAura == true){
	        if (_Timer <= 0.0f && _Activated)
	        {
	            Spawn();
	            _Timer = _TimeToSpawn;
	            _Activated = false;
	        }
		}else if(_needAura == false){
			if (_Timer <= 0.0f)
			{
				Spawn();
				_Timer = _TimeToSpawn;
			}
		}
	}

    void Spawn()
    {
        for (int i = 0; i < _NumberToSpawn; i++)
        {
            GameObject spawned = Instantiate(_ToSpawn, transform.position+ new Vector3(Random.Range(2.0f, 6.0f), 0.0f, Random.Range(-3.0f, 3.0f)), Quaternion.identity) as GameObject;
			//on assigne à ces nouveaux ennemis le pointer d'ennemis de la salle/zone où ils se trouvent
			if(_ToSpawn.GetComponent<EnemyPointer>()!=null){
				spawned.GetComponent<EnemyPointer>().MyEnemiesManager = EnemiesManager;
			}
        }
    }

    void InAura()
    {
       if (!_Activated)
       {
             _Activated = true;
        }
    }

}
