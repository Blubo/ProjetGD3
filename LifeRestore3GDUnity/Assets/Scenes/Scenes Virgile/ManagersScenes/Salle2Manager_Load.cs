using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

public class Salle2Manager_Load : MonoBehaviour
{
	bool playerIndexSet = false;
	public PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;
	 private AsyncOperation async;
	
	private int CountPlayers, CountIdole;
	private bool _EndLaunched;
	
	public GameObject _Zone;
  private ManagerLvl _LevelManager;

  private GameObject[] _Players;

  void Start()
  {
    if (GameObject.Find("Manager"))
    {
      _LevelManager = GameObject.Find("Manager").GetComponent<ManagerLvl>();
    }

    _Players = GameObject.FindGameObjectsWithTag("Player");

		CountPlayers = 0;
		CountIdole = 0;
		
		_EndLaunched = false;
		 //StartCoroutine("LoadAnotherLvl");
	}

 /* IEnumerator LoadAnotherLvl()
  {
    async = Application.LoadLevelAdditiveAsync(2);
    async.allowSceneActivation = false;
    Debug.Log("Loading complete");
    yield return async;
  }*/
	
	void Update()
	{
		prevState = state;
		state = GamePad.GetState(playerIndex);

		if (CountPlayers == 3 && CountIdole == 1 && !_EndLaunched)

//		if ((CountPlayers == 3 && CountIdole == 1 && !_EndLaunched) || state.Buttons.Start == ButtonState.Pressed)
		{
			StartCoroutine("RoomFinished");
			_EndLaunched = true;
		}
	}

  public IEnumerator RoomFinished()
  {
    //Changement de couleur de la zone 
    _Zone.GetComponent<Renderer>().material.color = Color.green;
    //Attente courte pour montrer la couleur
    //Chargement du niveau suivant
    //async.allowSceneActivation = true;

    for (int i = 0; i < _Players.Length; i++)
    {
      if (_Players[i].transform.FindChild("PlumeAlpha"))
      {
        _Players[i].transform.FindChild("PlumeAlpha").gameObject.GetComponent<AlphaManager>().OnEndLevel();
      }
    }
    yield return new WaitForSeconds(5.0f);
    Application.LoadLevel(3);
    _LevelManager.CheckForThings();
  }

  // On regarde les allers retours des objets dans la zone d'arrivée
  void OnTriggerEnter(Collider Col)
  {
    if (Col.gameObject.tag == "Player")
    {
      CountPlayers += 1;
    }

    if (Col.gameObject.tag == "Idole")
    {
      CountIdole += 1;
    }
  }

  void OnTriggerExit(Collider Col)
  {
    if (Col.gameObject.tag == "Player")
    {
      CountPlayers -= 1;
    }

    if (Col.gameObject.tag == "Idole")
    {
      CountIdole -= 1;
    }
  }
}
