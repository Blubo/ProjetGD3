using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CameraFollow : MonoBehaviour {

	public List<GameObject>players= new List<GameObject>();
	private List<float>distances = new List<float>();

	public GameObject v_cameraFocus;
	public float bottom;  // Bottom edge seen by the camera
	public float lead; // amount to lead the target
	public float above; // amount above the target pivot to see
	public float minView, maxView;
	private float _playerEcartement;
	private GameObject _farthestOne, _farthestTwo;
	public float cameraAltitude;

	void Update() {
		/*float height = v_player1.position.y + above - bottom;
		height = Mathf.Max(height, minView);
		height /= 2.0f;
		camera.orthographicSize = height;
		transform.position = new Vector3(v_player1.position.x + lead, bottom + height, -10f); */

		for (int i = 0; i < players.Count; i++) {
			distances.Add( Vector3.Distance(players[i].transform.position, v_cameraFocus.transform.position));
		}

		float max1 = new float();
		float max2 = new float();

		TwoHighest(out max1, out max2, distances);

		for (int i = 0; i < distances.Count; i++) {
			if(distances[i] == max1){
				_farthestOne = players[i];
			}else if(distances[i] == max2){
				_farthestTwo = players[i];
			}
		}

		_playerEcartement=Vector3.Distance(_farthestOne.transform.position, _farthestTwo.transform.position);
		Debug.Log("écartement "+ _playerEcartement);
		float height = _playerEcartement + above - bottom;
		height = Mathf.Max(height, minView);
		height = Mathf.Min(height, maxView);
		
		height /= 2.0f;
		GetComponent<Camera>().orthographicSize = height;
		transform.position = new Vector3(v_cameraFocus.transform.position.x, cameraAltitude, v_cameraFocus.transform.position.z);

		distances.Clear();
	}

	void TwoHighest(out float highest, out float second, List<float> floats) {
		highest = Mathf.NegativeInfinity;
		second = Mathf.NegativeInfinity;
		for (int i = 0; i < floats.Count; i++) {
			if (floats[i] >= highest) {
				second = highest;
				highest = floats[i];
			}
			else if (floats[i] > second) {
				second = floats[i];    
			}
		}
	}
}
