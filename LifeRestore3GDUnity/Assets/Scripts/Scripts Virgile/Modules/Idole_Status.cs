using UnityEngine;
using System.Collections;

public class Idole_Status : MonoBehaviour {

    [SerializeField]
    public int _Life;
	private int maxLife;
    private Sticky _LinkOnit;

	[SerializeField]
	private GameObject endSplashScreen;

	private bool switchedFromCleanToBroken = false;
//	[SerializeField]
	private GameObject lifeGauge;
	private Animator gaugeAnimator;

	void Start () {
		lifeGauge = Camera.main.transform.Find("LifeGauge").gameObject;
		if(lifeGauge.GetComponent<Animator>()!=null) gaugeAnimator = lifeGauge.GetComponent<Animator>();
		maxLife = _Life;
	    _LinkOnit = GetComponent<Sticky>();
	}
	
	void Update () {
//		lifeGauge.GetComponent<Animator>().SetInteger("IdoleHP", _Life);
		gaugeAnimator.SetInteger("IdoleHP", _Life);
//		myAnimator.SetInteger("IdoleHP", _Life);

		if(Input.GetKeyDown(KeyCode.Space)){
			TakeDamage();
//			Debug.Log("hp is "+_Life);
//			Debug.Log("anim hp is "+lifeGauge.GetComponent<Animator>().GetInteger("IdoleHP"));
		}

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

    void TakeDamage()
    {
      Clignotement();
      _Life -= 1;
    }

    void Death() { 
        //si les points de vies de l'aura tombent à zero on lance le game Over
		endSplashScreen.GetComponent<Renderer>().enabled = true;

		Destroy(gameObject);
    }

    public IEnumerator Clignotement()
    {
      for (int i = 0; i < 9; i++)
      {
        gameObject.GetComponentInChildren<Renderer>().enabled = !gameObject.GetComponentInChildren<Renderer>().enabled;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponentInChildren<Renderer>().enabled = true;
      }
    }

}
