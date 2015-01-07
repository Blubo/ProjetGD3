using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

public class ShootHookLouis5Janv : MonoBehaviour {
	
	bool playerIndexSet = false;
	public PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;

	//de tete, j'ai trouvé 3 méthodes pour tirer le lien depuis la "surface" de l'avatar
	//1)des gameObejct vide sur lesquels on met les scripts de tir, qui tournent selon une formule mathématiques le long d'un cercle centré sur le centre de l'avatar
	//ceci posait des pbs un peu plus loin toutefois, comment rendre le lien "joli" une fois crée ? je voudrais qu'il reste aligné entre le centre de l'avatar et la cible, et pas entre un point à la surface de l'avatar et la cible
	//2)rendre le centre de l'avatar transparent grace à un shader spécial, une sorte de "masque" (j'en ai trouvé un exemple mais j'ai testé la 3eme méthode avant de tester celle-ci)
	//3)trouver par le calcul à quel endroit du segment [centre;grappin] se trouve la surface du cercle, et par rapport à ce point, placer le lien au milieu, et le scale etc
	//j'ai donc choisi la méthode 3, ce qui implique tirer le projectile depuis cet endroit en question (point d'intersetion du "forward" du joueur et du cercle, en gros)

	private float _timer = 1.5f;
	public float _SpeedBullet;
	public GameObject v_Bullet;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		prevState = state;
		state = GamePad.GetState(playerIndex);

		_timer += 1 *Time.deltaTime;
		if(_timer>=1.5f){
			if(state.ThumbSticks.Right.X >0.5 || state.ThumbSticks.Right.X <-0.5||state.ThumbSticks.Right.Y >0.5||state.ThumbSticks.Right.Y<-0.5){
				if(prevState.Buttons.RightShoulder == ButtonState.Released && state.Buttons.RightShoulder == ButtonState.Pressed){
					DoShoot();
				}
			}
		}
	}

	void DoShoot(){
		//on instantie le tir sur la surface de l'avatar

		GameObject newBullet = Instantiate(v_Bullet, transform.TransformPoint(0f,0f,4f), transform.rotation) as GameObject;
		Rigidbody rb = newBullet.GetComponent<Rigidbody>();
		if (rb != null)	rb.AddForce(gameObject.transform.forward* _SpeedBullet);
		newBullet.GetComponent<GrappleComebackLouis5Janv>()._myShooter=gameObject;

		_timer = 0;
	}
}
