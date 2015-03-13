using UnityEngine;
using System.Collections;

public class Block_SpawnCollectible : MonoBehaviour {
    [SerializeField]
    private GameObject _Little, _Medium, _Big;

    [SerializeField]
    private int _NumberLittle, _NumberMedium, _NumberBig;

	void OnEnable () {
        for (int i = 0; i < _NumberLittle; i++)
        {
            Instantiate(_Little, gameObject.transform.position, Quaternion.identity );
        }

        for (int i = 0; i < _NumberMedium ; i++)
        {
            Instantiate(_Medium, gameObject.transform.position, Quaternion.identity );
        }

        for (int i = 0; i < _NumberBig; i++)
        {
            Instantiate(_Big, gameObject.transform.position, Quaternion.identity);
        }

	}   
}
