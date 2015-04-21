using UnityEngine;
using System.Collections;

public class Block_SpawnCollectible : MonoBehaviour {
    [SerializeField]
    private GameObject _Little, _Medium, _Big;

    [SerializeField]
    private int _NumberLittle, _NumberMedium, _NumberBig;

	void OnDestroy(){
		SpawnCollectible();
	}

	public void SpawnCollectible () {
        for (int i = 0; i < _NumberLittle; i++)
        {
            Instantiate(_Little, gameObject.transform.position +new Vector3(Random.Range(-2.0f, 2.0f), 0.0f, Random.Range(-2.0f, 2.0f)), Quaternion.identity );
        }

        for (int i = 0; i < _NumberMedium ; i++)
        {
            Instantiate(_Medium, gameObject.transform.position+new Vector3(Random.Range(-2.0f, 2.0f), 0.0f, Random.Range(-2.0f, 2.0f)), Quaternion.identity );
        }

        for (int i = 0; i < _NumberBig; i++)
        {
            Instantiate(_Big, gameObject.transform.position+new Vector3(Random.Range(-2.0f, 2.0f), 0.0f, Random.Range(-2.0f, 2.0f)), Quaternion.identity);
        }

	}   
}
