using UnityEngine;
using System.Collections;

public class AlphaManager : MonoBehaviour {

	[HideInInspector]
	public bool featherIsTaken = false;

	[HideInInspector]
	public GameObject posessor;

  private string possessorName;
	public int scoreBonusFlat;

	private Vector3 initScale, heldScale;

	void Start(){
		initScale = gameObject.transform.localScale;
//		heldScale = initScale * 1f;

	}

  void OnLevelWasLoaded()
  {
    if (PlayerPrefs.GetString("possessorName") != null)
    {

      posessor = GameObject.Find(PlayerPrefs.GetString("possessorName"));

      if (posessor != null)
      {
        GetComponent<Collider>().isTrigger = true;
        gameObject.transform.position = posessor.transform.Find("Avatar/Body/AlphaHolder").transform.position;
        gameObject.transform.rotation = posessor.transform.Find("Avatar/Body/AlphaHolder").transform.rotation;
        //			gameObject.transform.localScale = heldScale;
        gameObject.transform.parent = posessor.transform;

        gameObject.transform.localScale = initScale * 4;

        transform.Find("plumeAlpha_rayon").GetComponent<ParticleSystem>().Stop();
      }
    }
  }

  public void OnEndLevel()
  {
    possessorName = posessor.name;
    PlayerPrefs.SetString("possessorName", possessorName);
    posessor = null;
    gameObject.transform.parent = null;
  }

	// Update is called once per frame
	void Update () {
		if(posessor != null){
			GetComponent<Collider>().isTrigger =true;
			gameObject.transform.position = posessor.transform.Find("Avatar/Body/AlphaHolder").transform.position;
			gameObject.transform.rotation = posessor.transform.Find("Avatar/Body/AlphaHolder").transform.rotation;
//			gameObject.transform.localScale = heldScale;
			gameObject.transform.parent = posessor.transform;

			gameObject.transform.localScale = initScale*4;

			transform.Find("plumeAlpha_rayon").GetComponent<ParticleSystem>().Stop();

		}else{
//			gameObject.transform.localScale = initScale;
			if(transform.Find("plumeAlpha_rayon").GetComponent<ParticleSystem>().isStopped == true){
				transform.Find("plumeAlpha_rayon").GetComponent<ParticleSystem>().Play();
			}
		}
	}
}
