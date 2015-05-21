using UnityEngine;
using System.Collections;

public class Idole_Status : MonoBehaviour {

    [SerializeField]
    public int _Life;
	private int maxLife;
    private Sticky _LinkOnit;

    [SerializeField]
    private float TimerInvincibility;

	[SerializeField]
	private GameObject endSplashScreen;

	private bool switchedFromCleanToBroken = false;
//	[SerializeField]
	private GameObject lifeGauge;
	private Animator gaugeAnimator;

	private IdoleCallHelp myIdoleCallHelp;

	void Start () {
		myIdoleCallHelp = GetComponent<IdoleCallHelp>();
		lifeGauge = Camera.main.transform.Find("LifeGauge").gameObject;
		if(lifeGauge.GetComponent<Animator>()!=null) gaugeAnimator = lifeGauge.GetComponent<Animator>();
		maxLife = _Life;
	    _LinkOnit = GetComponent<Sticky>();
	}
	
	void Update () {
//		lifeGauge.GetComponent<Animator>().SetInteger("IdoleHP", _Life);
		gaugeAnimator.SetInteger("IdoleHP", _Life);
//		myAnimator.SetInteger("IdoleHP", _Life);

	    if (_Life <= 0){
            Death();
        }

		if(_Life <= maxLife*0.5f && switchedFromCleanToBroken == false){
			switchedFromCleanToBroken = true;
			Renderer[] rendererClean = gameObject.transform.Find("Idole_Clean").GetComponentsInChildren<Renderer>() ;
			foreach (Renderer rend in rendererClean){
				rend.enabled = false;

			}
			Renderer[] rendererBroken = gameObject.transform.Find("Idole_Broken").GetComponentsInChildren<Renderer>() ;
			foreach (Renderer rend in rendererBroken){
				rend.enabled = true;
			}
		}

       /* if (_LinkOnit.v_numberOfLinks >= 1)
        {
           Transform _Child =  transform.FindChild("Aura");
           _Child.GetComponent<SphereCollider>().enabled = true;
        }
        else if (_LinkOnit.v_numberOfLinks <= 0)
        {
            Transform _Child = transform.FindChild("Aura");
            if (_Child.GetComponent<SphereCollider>() != null)
            {
                _Child.GetComponent<SphereCollider>().enabled = false;
            }
        }*/
	}

    void OnTriggerStay(Collider _other) {
            //Si l'objet dans la zone de l'idole peut interagir avec, alors elle le fait
        if(GetComponentInChildren<SphereCollider>().enabled == true){
            if (_other.gameObject.layer == LayerMask.NameToLayer("Usable"))
            {
                _other.SendMessage("InAura");
            }
        }
    }

    void TakeDamage(int Value)
    {
//		myIdoleCallHelp.CallHelp();
      _Life -= Value;
      StartCoroutine("Clignotement");
      StartCoroutine("Invincible");
    }

    void Death() { 
        //si les points de vies de l'aura tombent à zero on lance le game Over
		if(endSplashScreen!= null)endSplashScreen.GetComponent<Renderer>().enabled = true;

		Destroy(gameObject);
    }

    public IEnumerator Invincible()
    {
      GetComponent<CapsuleCollider>().enabled = false;
      yield return new WaitForSeconds(TimerInvincibility);
      GetComponent<CapsuleCollider>().enabled = true;
    }

    public IEnumerator Clignotement()
    {
      if (!switchedFromCleanToBroken){
        for (int i = 0; i < 9; i++)
        {
          Renderer[] renderer = gameObject.transform.Find("Idole_Clean").GetComponentsInChildren<MeshRenderer>();
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
      }else
      {
        for (int i = 0; i < 9; i++)
        {
          Renderer[] renderer = gameObject.transform.Find("Idole_Broken").GetComponentsInChildren<MeshRenderer>();
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

    }

}
