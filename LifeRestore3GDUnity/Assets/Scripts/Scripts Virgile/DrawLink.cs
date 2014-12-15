using UnityEngine;
using System.Collections;

public class DrawLink : MonoBehaviour {

	//Script pour dessiner une ligne (pour les nuls)
	private Color _Red = Color.green;
	private Color _blue = Color.blue;

	private Shoot _shooter;

	void Awake(){
		_shooter = GetComponent<Shoot> ();
	}

	// Use this for initialization
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
		_link.SetVertexCount (2);
		//(sais pas pourquoi cette ligne mais on fait avec)
		//_link =  gameObject.AddComponent<LineRenderer> ();
	}

	 void Update(){
		LineRenderer _link = GetComponent<LineRenderer> ();

		//On ne peut pas définir le point d'arrivée et de fin de la ligne à la place de ça on signal où se situe chaque point de la ligne
		//Surement moyen de faire un tableau ou un truc pour automatiser la chose
		_link.SetPosition (0, transform.position);
		_link.SetPosition (1, _shooter._target.transform.position);
	}
}
