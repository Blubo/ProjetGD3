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
		}else{
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
            Instantiate(_ToSpawn, transform.position+ new Vector3(Random.Range(2.0f, 6.0f), 0.0f, Random.Range(-3.0f, 3.0f)), Quaternion.identity);
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
