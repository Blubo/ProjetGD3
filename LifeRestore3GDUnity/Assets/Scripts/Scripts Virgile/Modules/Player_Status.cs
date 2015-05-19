using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player_Status : MonoBehaviour {

    public GameObject _Idole;

    [SerializeField]
    private float  _TimerInvincible;

	private ScoreManager _ScoreManager;

	[SerializeField]
	private int playerNumber;

	[SerializeField]
	private int damage, numberToDrop;

	[SerializeField]
	private GameObject collectibleToDrop;

	void Start () {
		_ScoreManager = Camera.main.GetComponent<ScoreManager>();
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
		}
       StartCoroutine("Invincible");
      StartCoroutine("Clignotement");
    }

    public IEnumerator Clignotement()
    {
        for (int i = 0; i < 5; i++)
        {
         Renderer[] renderer = gameObject.transform.Find("Avatar").GetComponentsInChildren<MeshRenderer>() ;
         foreach (Renderer rend in renderer)
         {
           rend.enabled = false;
         }
         yield return new WaitForSeconds(0.1f);

         foreach (Renderer rend in renderer)
         {
           rend.enabled = true;
         }
         yield return new WaitForSeconds(0.1f);
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