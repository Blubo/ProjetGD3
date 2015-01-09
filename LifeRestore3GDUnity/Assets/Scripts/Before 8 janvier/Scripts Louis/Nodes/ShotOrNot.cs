using UnityEngine;
using System.Collections;

public class ShotOrNot : MonoBehaviour {

	public bool v_shot=false;
	[HideInInspector]
	public GameObject v_whoShouldILinkTo;
	private Color _Red = Color.green;
	private Color _blue = Color.blue;
	private Vector3 _myPos;

	void Start () {
		//LineRenderer permet de créer une ligne entre 2 ou + points (la ligne n'est pas forcément droite de ce fait)
		LineRenderer _link = gameObject.AddComponent<LineRenderer> ();
		//On peut utiliser n'importe quel material pour rendre la ligne (on peut faire des trucs illuminés ou autres)
		//Ceci dit avec un diffuse la couleur est noire problème de lumière surement je verrais 
		_link.material = new Material (Shader.Find ("Particles/Additive"));
		//On peut définir les couleurs, eh oui "les"
		//Une pour le début de la ligne et une pour la fin (sur chaque segment)
		_link.SetColors (_Red, _Red);
		//Largeur de la ligne, moyen de la changer en gametime, neat!
		_link.SetWidth (0.5f, 0.5f);
		//Ca c'est le nombre de vertexs par lequels la ligne va passer
		//Dans notre cas on veut entre deux objets donc A>B = 2 vertexs. on pourra ajouter des vertexs pour "relier" d'autres objets (Array)
		_link.SetVertexCount (0);
		//(sais pas pourquoi cette ligne mais on fait avec)
		//_link =  gameObject.AddComponent<LineRenderer> ();
	}

	void Update(){
		_myPos=gameObject.transform.position;
		if(v_shot==true){
			LineRenderer _link = GetComponent<LineRenderer> ();
//			_link.SetVertexCount (_Playerlinked._whoLinkedMe.Count+1);
			_link.SetVertexCount (2);
			//_link.SetPosition(1, v_whoShouldILinkTo.transform.position);
			_link.SetPosition(0, gameObject.transform.position);
			_link.SetPosition(1, v_whoShouldILinkTo.transform.position);

			//			if (_Playerlinked._whoLinkedMe.Count != 0) {
//				for (int i = 0; i < _Playerlinked._whoLinkedMe.Count ; i++) {
//					if( _Playerlinked._whoLinkedMe[i] != null){
//						_link.SetPosition(i, _Playerlinked._whoLinkedMe[i].transform.position);
//					}
//					_link.SetPosition(1,gameObject.transform.position );
//				}
//			}
		}
	}

}
