using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inimigo : MonoBehaviour {

	/*
	private int health = 10;

	//materials

	private Material matwhite;
	private Material matdefault;
	SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
		matwhite = Resources.Load ("WhiteFlash", typeof(Material)) as Material;
		matdefault = sr.material;
	}
	
	private void OnTriggerEnter2D(Collider2D collision){
		if (collision.CompareTag ("Bullet") || collision.gameObject.name == "AttackHitBox") {
			if (collision.CompareTag ("Bullet")) {
				Destroy (collision.gameObject);
			}
			sr.material = matwhite;
			health--;
			if (health <= 0) {
				killself ();
			} else {
				Invoke ("resetmaterial", .1f);
			}
		} 
	}

	void resetmaterial(){
		sr.material = matdefault; 
	}

	private void killself(){
		Destroy (gameObject);
	}
	*/

	//SANESS
	private int health = 10;

	//materials

	private Material matwhite;
	private Material matdefault;
	SpriteRenderer sr;
	Object Blasterref;
	Rigidbody2D rd2d;
	bool Badtime = false;

	[SerializeField]
	GameObject player;


	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
		matwhite = Resources.Load ("WhiteFlash", typeof(Material)) as Material;
		matdefault = sr.material;
		rd2d = GetComponent<Rigidbody2D> ();
		Blasterref = Resources.Load ("SansRage");
	}

	private void OnTriggerEnter2D(Collider2D collision){
		if (collision.gameObject.name == "AttackHitBox") {
			sr.material = matwhite;
			health--;
			if (health <= 0) {
				killself ();
			} else {
				Invoke ("resetmaterial", .1f);
			}
			if (!Badtime) {
				BadTime ();
				Badtime = true;
			}
		} 
	}

	void resetmaterial(){
		sr.material = matdefault; 
	}

	private void killself(){
		Destroy (gameObject);
	}

	void Update(){
		
	}

	void Teleport(){

		rd2d.position = new Vector2 (UnityEngine.Random.Range(player.transform.position.x - 3,player.transform.position.x + 3) , player.transform.position.y);
		GameObject Blaster = (GameObject)Instantiate (Blasterref);
		Blaster BlasterS = Blaster.GetComponent<Blaster> ();
		Blaster.transform.position = new Vector3 (transform.position.x, transform.position.y + 1.7f , -1);
		Invoke ("Teleport", 1);

	}

	void BadTime(){
		
		rd2d.position = new Vector2 (UnityEngine.Random.Range(player.transform.position.x - 3,player.transform.position.x + 3) , player.transform.position.y);

		Teleport ();

		

		

	}
}
