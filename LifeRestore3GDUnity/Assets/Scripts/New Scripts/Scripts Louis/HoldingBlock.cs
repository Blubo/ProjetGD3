using UnityEngine;
using System.Collections;

public class HoldingBlock : MonoBehaviour {

    private Sticky _blocs;
    [SerializeField]
    private float _Timer;

	[SerializeField]
	private Material _firstMat, _endMat;
	[SerializeField]
	private float colorSpeed, resetSpeed, duration, returnDuration;
	private float lerp;

	// Use this for initialization
	void Start () {
        _blocs = GetComponent<Sticky>();
	}
	
	// Update is called once per frame
	void Update () {
//	    if(_blocs.v_numberOfLinks >= 4){
//            _Timer -= 1.0f * Time.deltaTime;
//        }
//
//        if(_Timer <= 0 ){
//            Debug.Log("Stuff happens!");
//            gameObject.renderer.material.color = Color.green;
//        }

		//FIRST LOUIS
		if(_blocs.v_numberOfLinks != 0){
		

			lerp += Time.deltaTime*_blocs.v_numberOfLinks;
			gameObject.renderer.material.Lerp(gameObject.renderer.material, _endMat, lerp/colorSpeed);
		}else{
			lerp -= Time.deltaTime*resetSpeed;
			gameObject.renderer.material.Lerp(gameObject.renderer.material, _firstMat, (resetSpeed*Time.deltaTime)/colorSpeed);
		}



		if(lerp>= colorSpeed){
			Debug.Log("now!!");
			Debug.Log("timer is "+lerp);
		}

		lerp = Mathf.Clamp(lerp, 0, colorSpeed);

		//SECOND LOUIS

//		if(_blocs.v_numberOfLinks != 0){
//			
//			if(lerp<1){
//				lerp+=(Time.deltaTime*_blocs.v_numberOfLinks)/duration;
//			}
//			gameObject.renderer.material.Lerp(gameObject.renderer.material, _endMat, lerp);
//		}else{
//			if(lerp>0){
//				lerp-=(Time.deltaTime)/returnDuration;
//			}
//			gameObject.renderer.material.Lerp(gameObject.renderer.material, _firstMat, lerp);
//		}
//		
//		
//		
//		if(lerp>= 1){
//			Debug.Log("now!!");
//			Debug.Log("timer is "+lerp);
//		}
//		
//		lerp = Mathf.Clamp(lerp, 0, 1);
	}
}
