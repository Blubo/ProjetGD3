using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AdoptComportment : MonoBehaviour {

	//public GameObject v_node1, v_node2, v_node3;
	public List<Transform> v_nodes;
	public List<Texture> _possibleTextures;
	private List<GameObject> _whoIsConnectedToMeColor;

	private float _green, _red, _blue;
	public float v_damageConstant, v_sizeConstant, _colorConstant;
	private int _takeDamage, _takeSize, _takeColor, _numberOfLinksDone;

	[HideInInspector]
	public float v_bossHP = 500f;

	private Color _myColor;
	public GameObject v_enemyFace;

	// Use this for initialization
	void Start () {
		_whoIsConnectedToMeColor = new List<GameObject>();
		foreach(Transform child in transform){
			v_nodes.Add(child);
		}
		_numberOfLinksDone=0;
		_takeDamage=0;
		_takeSize=0;
		_takeColor=0;
		_myColor=v_enemyFace.gameObject.renderer.material.color;
		_green=gameObject.renderer.material.color.g;
		_red=gameObject.renderer.material.color.r;
		_blue=gameObject.renderer.material.color.b;
	}
	
	// Update is called once per frame
	void Update () {

//		if(_takeDamage!=0){
//			
//		}
//
//		if(_takeSize!=0){
//			
//		}
//
//		if(_takeColor!=0){
//			
//		}

		//damage
		//le boss perd des pv à chaque frame en fonction du nombre de node de "damage" qu'il recoit
		//et d'une constante choisie arbitrairement
		//si 0 nodes de dégats, alors 0 de dégats
		//si bcoup de nodes de dégats, alors bcoup de dégats
		v_bossHP -= _takeDamage*Time.deltaTime*v_damageConstant;

		//size
		//le boss grandit à chaque frame en fonction du nombre de node de "size" qu'il recoit
		//et d'une constante choisie arbitrairement
		//si 0 nodes de size, alors 0 de croissance
		//si bcoup de nodes de size, alors grande coirssance
		gameObject.transform.localScale += new Vector3(v_sizeConstant, v_sizeConstant, v_sizeConstant)*_takeSize;
		v_enemyFace.transform.localScale += new Vector3(v_sizeConstant, v_sizeConstant, v_sizeConstant)*_takeSize*0.5f;

		//_myColor=new Color(Mathf.Clamp(_red, 0, 1), Mathf.Clamp(_green, 0, 1), Mathf.Clamp(_blue, 0, 1));
		//_myColor+=new Color(0.1f, 0.1f, 0.1f);
		if(_whoIsConnectedToMeColor!=null ){
			for (int i = 0; i < _whoIsConnectedToMeColor.Count; i++) {
				_myColor.r += _whoIsConnectedToMeColor[i].renderer.material.color.r*_colorConstant;
				_myColor.g += _whoIsConnectedToMeColor[i].renderer.material.color.g*_colorConstant;
				_myColor.b += _whoIsConnectedToMeColor[i].renderer.material.color.b*_colorConstant;

			}
		}
		v_enemyFace.renderer.material.color=_myColor;
	}

	void OnTriggerEnter(Collider collided){
		if (collided.transform.GetComponent<PelletScript>()!=null) {
			if(collided.transform.tag.Equals("Pellet")){

				//ici, on cherche quel node était selectionné quand le joueur a tiré
				//donc on choppe la bullet, on choppe la personne qui a tiré la bullet, puis on choppe l'information en quesiton
				//sur le script situé sur L ENFANT de celui qui l'a tirée
				if(collided.transform.GetComponent<PelletScript>().v_whoShotMe.GetComponentInChildren<ChangeNode>().v_selectedNode==1){
					if(v_nodes[_numberOfLinksDone].GetComponent<ShotOrNotEnemy>().v_shot==false){
						v_nodes[_numberOfLinksDone].GetComponent<ShotOrNotEnemy>().v_shot=true;
						if(collided.transform.GetComponent<PelletScript>().v_whoShotMe.GetComponentInChildren<ChangeNode>().v_selectedNodeObject.GetComponent<ShotOrNot>().v_shot==false){

							collided.transform.GetComponent<PelletScript>().v_whoShotMe.GetComponentInChildren<ChangeNode>().v_selectedNodeObject.GetComponent<ShotOrNot>().v_shot=true;
							collided.transform.GetComponent<PelletScript>().v_whoShotMe.GetComponentInChildren<ChangeNode>().v_selectedNodeObject.GetComponent<ShotOrNot>().v_whoShouldILinkTo = v_nodes[_numberOfLinksDone].Find("Plane").gameObject;
							Debug.Log("take damage");
							v_nodes[_numberOfLinksDone].Find("Plane").renderer.material.mainTexture =  _possibleTextures[1];

							_numberOfLinksDone+=1;
							_takeDamage+=1;
						}
					}
				}

				if(collided.transform.GetComponent<PelletScript>().v_whoShotMe.GetComponentInChildren<ChangeNode>().v_selectedNode==2){
					if(v_nodes[_numberOfLinksDone].GetComponent<ShotOrNotEnemy>().v_shot==false){
						v_nodes[_numberOfLinksDone].GetComponent<ShotOrNotEnemy>().v_shot=true;
						if(collided.transform.GetComponent<PelletScript>().v_whoShotMe.GetComponentInChildren<ChangeNode>().v_selectedNodeObject.GetComponent<ShotOrNot>().v_shot==false){

							collided.transform.GetComponent<PelletScript>().v_whoShotMe.GetComponentInChildren<ChangeNode>().v_selectedNodeObject.GetComponent<ShotOrNot>().v_shot=true;
							collided.transform.GetComponent<PelletScript>().v_whoShotMe.GetComponentInChildren<ChangeNode>().v_selectedNodeObject.GetComponent<ShotOrNot>().v_whoShouldILinkTo = v_nodes[_numberOfLinksDone].Find("Plane").gameObject;

							Debug.Log("change size");
							v_nodes[_numberOfLinksDone].Find("Plane").renderer.material.mainTexture =  _possibleTextures[2];

							_numberOfLinksDone+=1;
							_takeSize+=1;
						}
					}
				}

				if(collided.transform.GetComponent<PelletScript>().v_whoShotMe.GetComponentInChildren<ChangeNode>().v_selectedNode==3){
					if(v_nodes[_numberOfLinksDone].GetComponent<ShotOrNotEnemy>().v_shot==false){
						v_nodes[_numberOfLinksDone].GetComponent<ShotOrNotEnemy>().v_shot=true;
						if(collided.transform.GetComponent<PelletScript>().v_whoShotMe.GetComponentInChildren<ChangeNode>().v_selectedNodeObject.GetComponent<ShotOrNot>().v_shot==false){

							collided.transform.GetComponent<PelletScript>().v_whoShotMe.GetComponentInChildren<ChangeNode>().v_selectedNodeObject.GetComponent<ShotOrNot>().v_shot=true;
							collided.transform.GetComponent<PelletScript>().v_whoShotMe.GetComponentInChildren<ChangeNode>().v_selectedNodeObject.GetComponent<ShotOrNot>().v_whoShouldILinkTo = v_nodes[_numberOfLinksDone].Find("Plane").gameObject;

							Debug.Log("change color");
							v_nodes[_numberOfLinksDone].Find("Plane").renderer.material.mainTexture =  _possibleTextures[3];
							_whoIsConnectedToMeColor.Add(collided.transform.GetComponent<PelletScript>().v_whoShotMe);
							//_whoIsConnectedToMeColor.Add(gameObject);
							//Destroy(collided.transform.GetComponent<PelletScript>().v_whoShotMe);
							//_whoIsConnectedToMeColor.Add(gameObject);
							_numberOfLinksDone+=1;
							_takeColor+=1;
						}
					}
				}
			}
		}
	}
}
