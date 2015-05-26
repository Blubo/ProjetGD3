using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Spawner : MonoBehaviour {

	[SerializeField]
	private List<GameObject> _MobPack;
	[SerializeField]
	private float Timer;
	[SerializeField]
	private bool _LimitedStocks;
	[SerializeField]
	private int _Stocks;
	[SerializeField]
	private GameObject _visualFx;

	private CaserneStats _Stats;

	private float TimerTemp;
	private int StocksTemp;

	[SerializeField]
	private GameObject countDownObject;
	private Text countDownText;
	private Vector3 TextInitScale;
	private float textTimer;

	void Awake(){
		countDownText = countDownObject.GetComponent<Text>();
		TextInitScale = countDownObject.transform.localScale;
		_Stats = gameObject.GetComponent<CaserneStats>();
//		TimerTemp = 0.0f;
		TimerTemp = Timer;
		textTimer = 0;
		StocksTemp = 0;
	}

	void Update(){
		textTimer+= Time.deltaTime;
		if(textTimer>=1){
			textTimer = 0;
//			countDownObject.transform.localScale  = TextInitScale;
		}

		countDownText.text = ((int)TimerTemp).ToString();
//		countDownObject.transform.localScale *= textTimer*Time.deltaTime;
		DecreaseTimer();
		if (TimerTemp <= 0.0f){
			Spawn();
			TimerTemp = Timer;
		}

		if (!_Stats.isProducing){
			this.enabled = false;
			countDownText.enabled = false;
		}
	}

	void DecreaseTimer(){
		TimerTemp-= 1.0f*Time.deltaTime;
	}

	void Spawn(){
		if (_LimitedStocks){
			if(StocksTemp <= _Stocks){
			//Pop de la vague d'ennemi associé 
			}
			//On incrémente
			StocksTemp += 1;
		}
		else
		{
			//Pop d'un élément du tableau/ random ou choisi
			Instantiate(_MobPack[0], transform.position + transform.forward * 5.0f+transform.up *2.0f, Quaternion.identity);
			Instantiate(_visualFx, transform.position + transform.forward * 3.0f + transform.up, Quaternion.identity);
		}
	}
}
