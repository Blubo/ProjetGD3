using UnityEngine;
using System.Collections;

public class Block_SpawnCollectible : MonoBehaviour {
    [SerializeField]
    private GameObject _Little, _Medium, _Big, _PlumeAlpha;

    private bool spawn;
    public bool _PopPlumeAlpha;

    [SerializeField]
    private int _NumberLittle, _NumberMedium, _NumberBig;

	[SerializeField]
	private float minSpread, maxSpread, minHeight, maxHeight, explosionForce;

  private ManagerLvl _LevelManager;

	void Start(){
    if (GameObject.Find("Manager") != null)
    {
      _LevelManager = GameObject.Find("Manager").GetComponent<ManagerLvl>();
    }
	}

	public void SpawnCollectible () {
//		for (int i = 0; i < _NumberLittle; i++){
//			Instantiate(_Little, new Vector3(gameObject.transform.position.x+Random.Range(-2.0f, 2.0f), 0.0f, gameObject.transform.position.z+Random.Range(-2.0f, 2.0f)), _Little.transform.rotation);
//		}
//
//		for (int i = 0; i < _NumberMedium ; i++){
//			Instantiate(_Medium, new Vector3(gameObject.transform.position.x+Random.Range(-2.0f, 2.0f), 0.0f, gameObject.transform.position.z+Random.Range(-2.0f, 2.0f)), _Medium.transform.rotation);
//		}
//
//		for (int i = 0; i < _NumberBig; i++){
//			Instantiate(_Big, new Vector3(gameObject.transform.position.x+Random.Range(-2.0f, 2.0f), 0.0f, gameObject.transform.position.z+Random.Range(-2.0f, 2.0f)), _Big.transform.rotation);
//		}

		for (int i = 0; i < _NumberLittle; i++){
			GameObject spawned = Instantiate(_Little, gameObject.transform.position, _Little.transform.rotation) as GameObject;
			Vector3 randomDirection = new Vector3(gameObject.transform.position.x+Random.Range(minSpread, maxSpread), Random.Range(minHeight, maxHeight), gameObject.transform.position.z+Random.Range(minSpread, maxSpread));

			Vector3 dir = randomDirection - spawned.transform.position;
			Rigidbody spawnedRB = spawned.AddComponent<Rigidbody>();
			spawnedRB.AddForce(dir * explosionForce);
		}
		
		for (int i = 0; i < _NumberMedium ; i++){
			GameObject spawned = Instantiate(_Medium, gameObject.transform.position, _Medium.transform.rotation) as GameObject;
			Vector3 randomDirection = new Vector3(gameObject.transform.position.x+Random.Range(minSpread, maxSpread), Random.Range(minHeight, maxHeight), gameObject.transform.position.z+Random.Range(minSpread, maxSpread));
			
			Vector3 dir = randomDirection - spawned.transform.position;
			Rigidbody spawnedRB = spawned.AddComponent<Rigidbody>();
			spawnedRB.AddForce(dir * explosionForce);
		}
		
		for (int i = 0; i < _NumberBig; i++){
			GameObject spawned = Instantiate(_Big, gameObject.transform.position, _Big.transform.rotation) as GameObject;
			Vector3 randomDirection = new Vector3(gameObject.transform.position.x+Random.Range(minSpread, maxSpread), Random.Range(minHeight, maxHeight), gameObject.transform.position.z+Random.Range(minSpread, maxSpread));
			
			Vector3 dir = randomDirection - spawned.transform.position;
			Rigidbody spawnedRB = spawned.AddComponent<Rigidbody>();
			spawnedRB.AddForce(dir * explosionForce);
		}
    if (_PopPlumeAlpha)
    {
      GameObject spawned = Instantiate(_PlumeAlpha, new Vector3(transform.position.x, _PlumeAlpha.transform.position.y, transform.position.z), _PlumeAlpha.transform.rotation) as GameObject;
      spawned.gameObject.name = "PlumeAlpha";
    }
    if (_LevelManager != null)
    {
      _LevelManager.CheckForThings();
    }

	}

  void Activated()
  {
    SpawnCollectible();
    spawn = true;
  }
}
