using UnityEngine;
using System.Collections;

public class Salle1Manager : MonoBehaviour {

  private int CountPlayers, CountIdole;

	void Start () {
	  CountPlayers = 0;
    CountIdole = 0;
	}
	
	void Update () {
	  if(CountPlayers == 3 && CountIdole == 1){

    }
	}

  p

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
