using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class PodiumScore : MonoBehaviour {


	//REMOVE
	//on met la vitesse de montée, par ordre croissant
	public float[] timers;
	
//	[SerializeField]
	//on met les hauteurs à laquelle on veut que les jauges s'élevent, DANS L ORDRe DU PLUS BAS AU PLUS HAUT
	private float[] heightsArray;
	[SerializeField]
	private float maxHeight;
	private float maxScore;
	//on mets les jauges des joueurs dedans
	[SerializeField]
	private GameObject[] playerGauges;
	
	private Vector3[] startPosArray, endPosArray;
	[HideInInspector]
	public int[] scores, scoresUsedForTexts;
	private int greenInt, redInt, blueInt;
	[SerializeField]
	private int scoreG, scoreR, scoreB;
	private bool[] _isLerping;
	private float _timeStartedLerping;
	private Text[] texts;
	private float accumulator;

	private bool isThereAnAlpha = false, isAlphaLerping = false, startedAlpha = false, addedAlphaBonus = false, alphaTimeSet = false;
	private int ScoreDuJoueurAlpha, AlphaPlayerScoreBeforeBonus, AlphaPlayerScoreAfterBonus, AlphaPlayerNumber, alphaPosInArrays;
	private float _timeStartedAlphaLerping, internalWaitTimer;
	[SerializeField]
	private float alphaRiseTimer, maxHeightAlpha;
	private Vector3 startPosAlpha;
	private MenuEnd myMenuEnd;
	[SerializeField]
	private float waitTimeBetweenScoreAndAlpha;

	void Awake(){

	}

	// Use this for initialization
	void Start () {
		myMenuEnd = Camera.main.GetComponent<MenuEnd>();

		if(myMenuEnd._alphaManager != null) isThereAnAlpha = true;
		if(isThereAnAlpha) AlphaPlayerNumber = myMenuEnd._alphaManager.posessor.GetComponent<Player_Status>().playerNumber;

		PlayerPrefs.SetInt("ScoreGreen", scoreG);
		PlayerPrefs.SetInt("ScoreRed", scoreR);
		PlayerPrefs.SetInt("ScoreBlue", scoreB);

		//REMOVE
		//on remplit scores avec les scores
		scores = new int[]{PlayerPrefs.GetInt("ScoreGreen"), PlayerPrefs.GetInt("ScoreRed"), PlayerPrefs.GetInt("ScoreBlue")};
		//on remplit le tableau de textes
		texts = new Text[]{playerGauges[0].transform.Find("CaseVert/Canvas/ScoreGreen").GetComponent<Text>(), playerGauges[1].transform.Find("CaseRouge/Canvas/ScoreRed").GetComponent<Text>(),playerGauges[2].transform.Find("CaseBleu/Canvas/ScoreBlue").GetComponent<Text>()};
		//par ailleurs, on mets nos trois Int utilisés dans podiumTextScore dans scoresUsedForTexts
		scoresUsedForTexts = new int[]{greenInt, redInt, blueInt};
		//on range scores par ordre croissant et les autres tableaux dans le meme ordre
		Array.Sort(scores, playerGauges);
		Array.Sort(scores, scoresUsedForTexts);
		Array.Sort(scores, texts);
		Array.Reverse(texts);

		//on conserve la position d'origine de chacun par ordre croissant
		startPosArray = new Vector3[]{playerGauges[0].transform.position, playerGauges[1].transform.position, playerGauges[2].transform.position};
		//on stocke le score max pour faire monter les jauges en fonction de la hauteur maximale, à laquelle le joueur avec le plus de score montera toujours
		//on fera ensuite myscore/maxscore, pour obtenir le ratio par lequel on doit multiplier la hauteur max, et ca donne la hauteur à laquelle chaque joueur doit monter selon son score
//		maxScore = scores[2];
		if(isThereAnAlpha){
			maxScore = scores[2]+myMenuEnd._alphaManager.scoreBonusFlat;
		}else{
			maxScore = scores[2];
		}

		float firstHeightRatio = scores[2]/maxScore;
		float secondHeightRatio = scores[1]/maxScore;
		float thirdHeightRatio = scores[0]/maxScore;

		heightsArray = new float[3];
		heightsArray[2] = maxHeight * firstHeightRatio;
		heightsArray[1] = maxHeight * secondHeightRatio;
		heightsArray[0] = maxHeight * thirdHeightRatio;

		//on calcule la position d'arrivée de chacun, selon son classement (le premier dans le tableau allant moins haut, donc recevant heightArray[0], le second heightArray[1] etc
		endPosArray = new Vector3[]{new Vector3(startPosArray[0].x, startPosArray[0].y+heightsArray[0], startPosArray[0].z),
			new Vector3(startPosArray[1].x, startPosArray[1].y+heightsArray[1], startPosArray[1].z),
			new Vector3(startPosArray[2].x, startPosArray[2].y+heightsArray[2], startPosArray[2].z)};
		_timeStartedLerping = Time.time;
		_isLerping = new bool[]{true, true, true};
	}
	
	// Update is called once per frame
	void Update () {
		accumulator+=Time.deltaTime;

//		if (Input.GetMouseButtonDown(0)) {
//			lerping = true;
//			progress = 0f;
//		}
//		if (lerping) {
//			progress += Time.deltaTime * speed;
//			Debug.Log(Mathf.Lerp(1, 1000, progress));
//			if (progress >= 1f) {
//				lerping = false;
//			}
//		}

		if(_isLerping[0] == false && _isLerping[1] == false && _isLerping[2] == false) isAlphaLerping = true;

		for (int i = 0; i < scores.Length; i++) {

			if(_isLerping[i])
			{
				//We want percentage = 0.0 when Time.time = _timeStartedLerping
				//and percentage = 1.0 when Time.time = _timeStartedLerping + timeTakenDuringLerp
				//In other words, we want to know what percentage of "timeTakenDuringLerp" the value
				//"Time.time - _timeStartedLerping" is.
				float timeSinceStarted = Time.time - _timeStartedLerping;
				float percentageComplete = timeSinceStarted / timers[i];

				if(scoresUsedForTexts[i]<scores[i]){
					scoresUsedForTexts[i] = (int)Mathf.Lerp(0, scores[i], percentageComplete);
				}

				playerGauges[i].transform.position = Vector3.Lerp(startPosArray[i], endPosArray[i], percentageComplete);

//				transform.position = Vector3.Lerp (_startPosition, _endPosition, percentageComplete);
				
				//When we've completed the lerp, we set _isLerping to false
				if(percentageComplete >= 1.0f)
				{
					_isLerping[i] = false;
					if(isThereAnAlpha == false)myMenuEnd.AllowInputs();

				}
			}else{
				if(isThereAnAlpha){
					if(isAlphaLerping == true){
						if(startedAlpha == false){
							if(addedAlphaBonus == false){
								
								if(AlphaPlayerNumber == 1){
									AlphaPlayerScoreBeforeBonus = PlayerPrefs.GetInt("ScoreGreen");
									AlphaPlayerScoreAfterBonus = PlayerPrefs.GetInt("ScoreGreen") + myMenuEnd._alphaManager.scoreBonusFlat;
								}else if(AlphaPlayerNumber == 2){
									AlphaPlayerScoreBeforeBonus = PlayerPrefs.GetInt("ScoreRed");
									AlphaPlayerScoreAfterBonus = PlayerPrefs.GetInt("ScoreRed") + myMenuEnd._alphaManager.scoreBonusFlat;

								}else if(AlphaPlayerNumber == 3){
									AlphaPlayerScoreBeforeBonus = PlayerPrefs.GetInt("ScoreBlue");
									AlphaPlayerScoreAfterBonus = PlayerPrefs.GetInt("ScoreBlue") + myMenuEnd._alphaManager.scoreBonusFlat;

								}
								myMenuEnd.AlphaBonus();
								addedAlphaBonus = true;
							}
							
							if(AlphaPlayerScoreBeforeBonus == scores[i]){

								alphaPosInArrays = i;
								startPosAlpha = playerGauges[i].transform.position;

								ScoreDuJoueurAlpha = AlphaPlayerScoreBeforeBonus;
		
//								_timeStartedAlphaLerping = Time.time;
								startedAlpha = true;
							}
						}else{
							internalWaitTimer+=Time.deltaTime;
							if(internalWaitTimer>waitTimeBetweenScoreAndAlpha*3){
								if(alphaTimeSet ==false){
									Debug.Log("internal waiter is "+internalWaitTimer);
									alphaTimeSet = true;
									_timeStartedAlphaLerping = Time.time;

								}
								float timeSinceStarted = Time.time - _timeStartedAlphaLerping;
								float percentageComplete = timeSinceStarted / alphaRiseTimer;
								if( ScoreDuJoueurAlpha < AlphaPlayerScoreAfterBonus){
									ScoreDuJoueurAlpha  = (int)Mathf.Lerp(AlphaPlayerScoreBeforeBonus, AlphaPlayerScoreAfterBonus, percentageComplete);
								}
								
								playerGauges[alphaPosInArrays].transform.position = Vector3.Lerp(startPosAlpha, new Vector3(startPosAlpha.x, startPosAlpha.y+ maxHeight, startPosAlpha.z), percentageComplete);

								if(percentageComplete >= 1.0f)
								{
									isAlphaLerping = false;
									myMenuEnd.AllowInputs();
								}
							}
						}
					}
				}
			}

//			if(scoresUsedForTexts[i]<scores[i]){
//				scoresUsedForTexts[i] = Mathf.Lerp(0, scores[i], 
////				scoresUsedForTexts[i]+=1;
//			}
			if(isThereAnAlpha){
				if(isAlphaLerping == false){
					texts[i].text = scoresUsedForTexts[i].ToString();
					if(startedAlpha == true){
						texts[alphaPosInArrays].text = ScoreDuJoueurAlpha.ToString();

					}
				}else{
					texts[i].text = scoresUsedForTexts[i].ToString();

					texts[alphaPosInArrays].text = ScoreDuJoueurAlpha.ToString();
				}
			}else{
				texts[i].text = scoresUsedForTexts[i].ToString();
			}
		}


		//but: faire monter graduellement le score de chaque joueur
		//pour ca
		//il faut d'une part une variable temporaire de score de chaque joueur qu'on incrémente et qu'on render sur la scene
		//d'autre part le vrai score de chaque joueur
		//encore d'autre part, il faut que ce score finisse d'augmenter quand la barre du joueur attend son max
		//autrement dit, le score doit s'incrémenter d'une valeur proportionelle à l'avancement/progression/vitesse de la barre
		//ex: à chaque frame, scoreJoueur+=(VraiScoreJoueur/tempsDeMontéeDeLaJauge);
	}

	IEnumerator WaitAndAlphaEffect()
	{
		//La bombe attend X pour exploser
		yield return new WaitForSeconds(waitTimeBetweenScoreAndAlpha);

	}
}
