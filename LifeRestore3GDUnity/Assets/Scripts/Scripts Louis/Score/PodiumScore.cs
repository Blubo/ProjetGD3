using UnityEngine;
using System.Collections;

public class PodiumScore : MonoBehaviour {

	[SerializeField]
	private GameObject jaugeGreen, jaugeRed, jaugeBlue;
	private Vector3 initGreen, initRed, initBlue;

	[SerializeField]
	private Transform grTarget1, grTarget2, grTarget3;
	[SerializeField]
	private Transform redTarget1, redTarget2, redTarget3;
	[SerializeField]
	private Transform blueTarget1, blueTarget2, blueTarget3;

	[SerializeField]
	private float timerFirst, timerSecond, timerThird;

	// Use this for initialization
	void Start () {
		initGreen = jaugeGreen.transform.position;
		initRed = jaugeRed.transform.position;
		initBlue = jaugeBlue.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//si vert premier
		if(PlayerPrefs.GetInt("ScoreGreen")>PlayerPrefs.GetInt("ScoreRed") && PlayerPrefs.GetInt("ScoreGreen")>PlayerPrefs.GetInt("ScoreBlue")){
			StartCoroutine(MoveObject(jaugeGreen.transform, initGreen, grTarget1.transform.position, timerFirst));
			//si rouge second
			if(PlayerPrefs.GetInt("ScoreRed")>PlayerPrefs.GetInt("ScoreBlue")){
				StartCoroutine(MoveObject(jaugeRed.transform, initRed, redTarget2.transform.position, timerSecond));
				StartCoroutine(MoveObject(jaugeBlue.transform, initBlue, blueTarget3.transform.position, timerThird));
				return;
			}else{
				//si bleu second
				StartCoroutine(MoveObject(jaugeRed.transform, initRed, redTarget3.transform.position, timerThird));
				StartCoroutine(MoveObject(jaugeBlue.transform, initBlue, blueTarget2.transform.position, timerSecond));
				return;
			}
			return;
		}

		//si rouge premier
		if(PlayerPrefs.GetInt("ScoreRed")>PlayerPrefs.GetInt("ScoreGreen") && PlayerPrefs.GetInt("ScoreRed")>PlayerPrefs.GetInt("ScoreBlue")){
			StartCoroutine(MoveObject(jaugeRed.transform, initRed, redTarget1.transform.position, timerFirst));
			//si vert second
			if(PlayerPrefs.GetInt("ScoreGreen")>PlayerPrefs.GetInt("ScoreBlue")){
				StartCoroutine(MoveObject(jaugeGreen.transform, initGreen, grTarget2.transform.position, timerSecond));
				StartCoroutine(MoveObject(jaugeBlue.transform, initBlue, blueTarget3.transform.position, timerThird));
				return;
			}else{
				//si bleu second
				StartCoroutine(MoveObject(jaugeGreen.transform, initGreen, grTarget3.transform.position, timerThird));
				StartCoroutine(MoveObject(jaugeBlue.transform, initBlue, blueTarget2.transform.position, timerSecond));
				return;
			}
			return;
		}

		//si bleu premier
		if(PlayerPrefs.GetInt("ScoreBlue")>PlayerPrefs.GetInt("ScoreGreen") && PlayerPrefs.GetInt("ScoreBlue")>PlayerPrefs.GetInt("ScoreRed")){
			StartCoroutine(MoveObject(jaugeBlue.transform, initBlue, blueTarget1.transform.position, timerFirst));
			//si vert second
			if(PlayerPrefs.GetInt("ScoreGreen")>PlayerPrefs.GetInt("ScoreRed")){
				StartCoroutine(MoveObject(jaugeGreen.transform, initGreen, grTarget2.transform.position, timerSecond));
				StartCoroutine(MoveObject(jaugeRed.transform, initRed, redTarget3.transform.position, timerThird));
				return;
			}else{
				//si rouge second
				StartCoroutine(MoveObject(jaugeGreen.transform, initGreen, grTarget3.transform.position, timerThird));
				StartCoroutine(MoveObject(jaugeRed.transform, initRed, redTarget2.transform.position, timerSecond));
				return;
			}
			return;
		}
	}

	IEnumerator MoveObject (Transform thisTransform, Vector3 startPos, Vector3 endPos, float time){
		float i = 0;
		float rate = 1/time;
		while (i < 1) {
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp(startPos, endPos, i);
			yield return null; 
		}
	}
}
