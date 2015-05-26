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

	[SerializeField]
	private int damage, numberToDrop;

	[SerializeField]
	private GameObject collectibleToDrop;

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
	
	void Start () {
		myAvatarAnimator = transform.Find("Avatar/Body").GetComponent<Animator>();

		_Idole = GameObject.Find("Idole");
		myShootF = GetComponent<ShootF>();
		myAlphaPlayer = GetComponent<AlphaPlayers>();
		_ScoreManager = Camera.main.GetComponent<ScoreManager>();
		_IsInvincible = false;
 	 }

    public void TakeDamage(Vector3 directionToUse)
    {
      if (!_IsInvincible)
      {
			myAvatarAnimator.SetBool("Damage", true);

			GetComponent<FatPlayerScript>().ChangeSize(_playerSizeDecreaseOnHit);

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

        DropCollectible();
        switch (playerNumber)
        {

          case 1:
            _ScoreManager.Score_Vert -= damage;
            break;

          case 2:
            _ScoreManager.Score_Rouge -= damage;
            break;

          case 3:
            _ScoreManager.Score_Bleu -= damage;
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

	void DropCollectible(){
		for (int i = 0; i < numberToDrop; i++) {
			GameObject dropped = Instantiate(collectibleToDrop, new Vector3(gameObject.transform.position.x+Random.Range(-2.0f, 2.0f), gameObject.transform.position.y, gameObject.transform.position.z+Random.Range(-2.0f, 2.0f)), collectibleToDrop.transform.rotation)as GameObject;
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