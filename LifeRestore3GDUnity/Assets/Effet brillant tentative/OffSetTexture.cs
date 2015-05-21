using UnityEngine;
using System.Collections;

public class OffSetTexture : MonoBehaviour {
	
	[SerializeField]
	float Xspeed, Yspeed;

	[SerializeField]
	int materialIndex = 0;

	Vector2 uvAnimationRate = new Vector2();
	public string textureName = "_MainTex";
	
	Vector2 uvOffset = Vector2.zero;

	private Material myMat;

	[SerializeField]
	private float frequency;
	private float timer;

	// Use this for initialization
	void Start () {
		timer= 0f;
		myMat = gameObject.GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
		timer+= Time.deltaTime;

		uvAnimationRate = new Vector2(Xspeed, Yspeed);
		
		//		uvOffset += (uvAnimationRate * Time.deltaTime);
		uvOffset += (uvAnimationRate*Time.deltaTime);
		
		if(GetComponent<Renderer>().enabled )
		{
			GetComponent<Renderer>().materials[materialIndex].SetTextureOffset(textureName, uvOffset);
		}

//		if(timer>frequency){
//			timer=0f;
//			StartCoroutine(Offset());
//			
//		}
	}

//	IEnumerator Offset(){
//		uvAnimationRate = new Vector2(Xspeed, Yspeed);
//		
////		uvOffset += (uvAnimationRate * Time.deltaTime);
//		uvOffset += (uvAnimationRate);
//
//		if(GetComponent<Renderer>().enabled )
//		{
//			GetComponent<Renderer>().materials[materialIndex].SetTextureOffset(textureName, uvOffset);
//		}
//		yield return null;
//	}
}
