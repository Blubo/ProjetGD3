using UnityEngine;
using System.Collections;

public class PlayerGrowth : MonoBehaviour {

	public float _time;
	private int _ActualForce;
	private float _growth;
	private Vector3 _GrowthFactor;
	public Vector3 _zeroLinkSize, sizePlusOne;
	private Vector3 _oneLinkSize, _twoLinkSize, _threeLinkSize, _fourLinkSize, _fiveLinkSize, _sixLinkSize, _sevenLinkSize, _eightLinkSize;

	// Use this for initialization
	void Start () {
		_oneLinkSize = _zeroLinkSize+sizePlusOne;
		_twoLinkSize = _zeroLinkSize+sizePlusOne*2;
		_threeLinkSize = _zeroLinkSize+sizePlusOne*3;
		_fourLinkSize = _zeroLinkSize+sizePlusOne*4;
		_fiveLinkSize = _zeroLinkSize+sizePlusOne*5;
		_sixLinkSize = _zeroLinkSize+sizePlusOne*6;
		_sevenLinkSize = _zeroLinkSize+sizePlusOne*7;
		_eightLinkSize = _zeroLinkSize+sizePlusOne*8;
	}
	
	// Update is called once per frame
	void Update () {
		_ActualForce = GetComponent<LinkStrenght>()._LinkCommited;

		//TOUTES CES VALEURS SONT RENTREES EN DUR, A L OEIL, ET SONT PROBABLEMENT A MODIFIER UNE FOIS LES ASSETS MODIFIES :/
		switch (_ActualForce) {
		case 0:
			_GrowthFactor = _zeroLinkSize;
			_growth = 1f;
			break;

		case 1:
			_GrowthFactor = _oneLinkSize;
			_growth = 10f;
			break;

		case 2:
			_GrowthFactor = _twoLinkSize;
			_growth = 14f;
			break;

		case 3:
			_GrowthFactor = _threeLinkSize;
			_growth = 19f;
			break;

		case 4:
			_GrowthFactor = _fourLinkSize;
			_growth = 24f;
			break;

		case 5:
			_GrowthFactor = _fiveLinkSize;
			_growth = 29f;
			break;

		case 6:
			_GrowthFactor = _sixLinkSize;
			_growth = 34f;
			break;

		case 7:
			_GrowthFactor = _sevenLinkSize;
			_growth = 39f;
			break;

		case 8:
			_GrowthFactor = _eightLinkSize;
			_growth = 44f;
			break;
		}
		gameObject.GetComponent<ShootF>().v_sizeRatio = Mathf.Lerp(gameObject.GetComponent<ShootF>()._initSizeRatio, _growth, _time);
		gameObject.transform.localScale = Vector3.Lerp (transform.localScale, _GrowthFactor, _time);
	}
}