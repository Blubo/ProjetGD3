using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player_Status : MonoBehaviour {

    public GameObject _Idole;

    [SerializeField]
    private float  _TimerInvincible;

    public bool _IsInvincible;

	private ScoreManager _ScoreManager;

	public int playerNumber;
//
//	[SerializeField]
//	private int numberToDrop;

	[SerializeField]
	private GameObject collectibleToDrop, enormColl, bigColl, midColl, smallColl;

	[HideInInspector]
	public GameObject linkedObject;
//	[HideInInspector]
//	public bool linkedToIdole = false;

	[SerializeField]
	private GameObject hitAnimation;

	private AlphaPlayers myAlphaPlayer;
	[SerializeField]
	private float _playerSizeDecreaseOnHit;
	private ShootF myShootF;
	private Animator myAvatarAnimator;

	[HideInInspector]
	public int myScore;

	[SerializeField]
	private int pointsDividerToSpawn;

	void Start () {
		myAvatarAnimator = transform.Find("Avatar/Body").GetComponent<Animator>();

		_Idole = GameObject.Find("Idole");
		myShootF = GetComponent<ShootF>();
		myAlphaPlayer = GetComponent<AlphaPlayers>();
		_ScoreManager = Camera.main.GetComponent<ScoreManager>();
		_IsInvincible = false;
 	 }

	void Update(){
		if(playerNumber == 1){
			myScore = PlayerPrefs.GetInt("ScoreGreen");
		}else if(playerNumber == 2){
			myScore = PlayerPrefs.GetInt("ScoreRed");
		}else if(playerNumber == 3){
			myScore = PlayerPrefs.GetInt("ScoreBlue");
		}
	}

	public void TakeDamage(Vector3 directionToUse, int damage)
    {
      if (!_IsInvincible)
      {
			myAvatarAnimator.SetBool("Damage", true);

//			GetComponent<FatPlayerScript>().ChangeSize(_playerSizeDecreaseOnHit);

			myAlphaPlayer.DropTheFeather(directionToUse);
			Camera.main.GetComponent<SoundManagerHeritTest>().PlaySoundOneShot("Ovo blessure");
			if(playerNumber == 1){
				hitAnimation.GetComponent<ParticleSystem>().startColor = Color.green;
			}else if(playerNumber == 2){
				hitAnimation.GetComponent<ParticleSystem>().startColor = Color.red;
			}else if(playerNumber == 3){
				hitAnimation.GetComponent<ParticleSystem>().startColor = Color.blue;
			}
			Instantiate(hitAnimation, gameObject.transform.position, Quaternion.identity);

//			(damageTaken);
        switch (playerNumber)
        {

          case 1:
				_ScoreManager.Score_Vert -= _ScoreManager.Score_Vert * damage/100;
				DropCollectible(_ScoreManager.Score_Vert * damage/100);
            break;
          case 2:
				_ScoreManager.Score_Rouge -= _ScoreManager.Score_Rouge * damage/100;
				DropCollectible(_ScoreManager.Score_Rouge * damage/100);
				break;
          case 3:
				_ScoreManager.Score_Bleu -= _ScoreManager.Score_Bleu * damage/100;
				DropCollectible(_ScoreManager.Score_Bleu * damage/100);
            break;
        }
        StartCoroutine("Clignotement");
        StartCoroutine("Invincible");
      }
		
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
        _IsInvincible = true;
        yield return new WaitForSeconds(_TimerInvincible);
		myAvatarAnimator.SetBool("Damage", false);
        _IsInvincible = false;
    }

	void DropCollectible(int PointsToLose){

		/*
		 * int totalSeconds = 453;
		int minutes = totalSeconds / 60;
		int remainingSeconds = totalSeconds % 60;
		*/

		PointsToLose /= pointsDividerToSpawn;

		int remainingPoints = new int();

		int EnormousToDrop = new int();
		int BigToDrop = new int();
		int MidToDrop = new int();
		int SmallToDrop = new int();

		BigToDrop = PointsToLose / 10;
		remainingPoints = PointsToLose % 10;

		for (int i = 0; i < BigToDrop; i++) {
			GameObject dropped = Instantiate(bigColl, new Vector3(gameObject.transform.position.x+Random.Range(-2.0f, 2.0f), gameObject.transform.position.y, gameObject.transform.position.z+Random.Range(-2.0f, 2.0f)), bigColl.transform.rotation)as GameObject;
		}

		MidToDrop = remainingPoints / 5;
		remainingPoints = remainingPoints %5;

		for (int i = 0; i < MidToDrop; i++) {
			GameObject dropped = Instantiate(midColl, new Vector3(gameObject.transform.position.x+Random.Range(-2.0f, 2.0f), gameObject.transform.position.y, gameObject.transform.position.z+Random.Range(-2.0f, 2.0f)), midColl.transform.rotation)as GameObject;
		}

		SmallToDrop = remainingPoints;
		Debug.Log("small to drop "+ SmallToDrop);
		for (int i = 0; i < SmallToDrop; i++) {
			GameObject dropped = Instantiate(smallColl, new Vector3(gameObject.transform.position.x+Random.Range(-2.0f, 2.0f), gameObject.transform.position.y, gameObject.transform.position.z+Random.Range(-2.0f, 2.0f)), smallColl.transform.rotation)as GameObject;
		}
	}

	public void SeverLinkToIdole(){
		if(myShootF._target != null){
			if(myShootF._target == _Idole){
				myShootF.DetachLink(0);
			}
		}
	}

}