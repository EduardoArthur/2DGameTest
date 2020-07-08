using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour {

	Animator animator;
	public SpriteRenderer spriterenderer;
	Object bulletref;
	bool attack = false;

	// Use this for initialization
	void Start () {

		animator = GetComponent<Animator> ();
		spriterenderer = GetComponent<SpriteRenderer> ();
		bulletref = Resources.Load ("Kirby_star1");
		StartCoroutine(Attack());
	}

	IEnumerator Attack(){

		yield return new WaitForSeconds (2);
		attack = true;
		animator.Play ("Death");
		for (int i = 0; i < 10; i++) {
			GameObject bullet = (GameObject)Instantiate (bulletref);
			BulletScript bulletS = bullet.GetComponent<BulletScript> ();
			bulletS.bulletspeed = -3;
			bullet.transform.position = new Vector3 (transform.position.x, transform.position.y - .3f, -1);
			yield return new WaitForSeconds (.1f);
		}
		killself ();

	}

	void killself(){
		Destroy (gameObject);
	}

	// Update is called once per frame
	void Update () {
	}
}
