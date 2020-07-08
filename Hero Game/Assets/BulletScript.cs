using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
	Rigidbody2D reggie;
	public SpriteRenderer spriterenderer;

	public int bulletspeed;

	// Use this for initialization
	void Start () {

		reggie = GetComponent<Rigidbody2D> ();
		spriterenderer = GetComponent<SpriteRenderer> ();
		Invoke ("DestroySelf", .8f);

	}
	
	// Update is called once per frame
	void Update () {
		reggie.velocity = new Vector2 (0, bulletspeed);
		//if (bulletspeed < 0) {
		//	spriterenderer.flipX = true;
		//}
	}

	private void OnCollisionEnter2D(Collision2D collision){

		if (collision.gameObject.CompareTag("Ground")) {
			Destroy (gameObject);
		}
	}

	private void DestroySelf(){
		Destroy (gameObject);
	}
		
}
