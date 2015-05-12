using UnityEngine;
using System.Collections;

public class Idole_Status : MonoBehaviour {

    [SerializeField]
    public int _Life;

    private Sticky _LinkOnit;

	void Start () {
	    _LinkOnit = GetComponent<Sticky>();
	}
	
	void Update () {
        if (_Life <= 0){
            Death();
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
