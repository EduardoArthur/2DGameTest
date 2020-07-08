using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	Animator animator;
	Rigidbody2D rb2d;
	SpriteRenderer spriterenderer;
	bool isGrounded;
	public bool turnleft = false;
	[SerializeField]
	Transform groundCheck;
	[SerializeField]
	Transform groundCheckL;
	[SerializeField]
	Transform groundCheckR;

	[SerializeField]
	GameObject attackHitbox;

	[SerializeField]
	private float runspeed = 2f;
	[SerializeField]
	private float jumpspeed = 5f;

	Object bulletref;
	private int health = 100;

	bool attack = false;
	bool isalive = true;

	IEnumerator DoAttack(float delay){
		attackHitbox.SetActive (true);
		yield return new WaitForSeconds (delay);
		attackHitbox.SetActive (false);
		attack = false;

	}
	//invenção maluca
	private Material matwhite;
	private Material matdefault;
	SpriteRenderer sr;

	// Use this for initialization
	void Start () {

		animator = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
		spriterenderer = GetComponent<SpriteRenderer> ();
		attackHitbox.SetActive (false);

		// invenção maluca
		sr = GetComponent<SpriteRenderer> ();
		matwhite = Resources.Load ("WhiteFlash", typeof(Material)) as Material;
		matdefault = sr.material;

	}

	private void FixedUpdate(){

		if (health <= 0) {
			animator.Play ("Player2_death");
		} else {
			if ((Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"))) ||
			   (Physics2D.Linecast (transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer ("Ground"))) ||
			   (Physics2D.Linecast (transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer ("Ground"))) ||
			   (Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Inimigo"))) ||
			   (Physics2D.Linecast (transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer ("Inimigo"))) ||
			   (Physics2D.Linecast (transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer ("Inimigo")))) {
				isGrounded = true;
			} else {
				isGrounded = false;
				if (!attack) {
					animator.Play ("Player2_jump");
				}
			}
		}

	}

	private void Update(){

		if (isalive) {

			if (Input.GetKey ("d") || Input.GetKey ("right")) {
				turnleft = false;
				rb2d.velocity = new Vector2 (runspeed, rb2d.velocity.y);
				if (isGrounded && !attack) {
					animator.Play ("Player2_run");
				}
				transform.localScale = new Vector3 (.25f, .25f, 1);
				//spriterenderer.flipX = false;
			} else if (Input.GetKey ("a") || Input.GetKey ("left")) {
				turnleft = true;
				rb2d.velocity = new Vector2 (-runspeed, rb2d.velocity.y);
				if (isGrounded && !attack) {
					animator.Play ("Player2_run");
				}
				//spriterenderer.flipX = true;
				transform.localScale = new Vector3 (-.25f, .25f, 1);
			} else if (Input.GetKeyDown ("space") && !attack) {
				//GameObject bullet = (GameObject)Instantiate (bulletref);
				attack = true;
				float delay = .4f;
				if (isGrounded) {
					int index = UnityEngine.Random.Range (1, 5);

					animator.Play ("Player2_attack" + index);
				} else {
					animator.Play ("Player2_voadora");
					delay = .2f;
				}
				StartCoroutine (DoAttack (delay));

				//if (turnleft == false) {
				//BulletScript bulletS = bullet.GetComponent<BulletScript> ();
				//bulletS.bulletspeed = 3;
				//bullet.transform.position = new Vector3 (transform.position.x + .4f, transform.position.y + .2f, -1);
				//} else {
				//BulletScript bulletS = bullet.GetComponent<BulletScript> ();
				//bulletS.bulletspeed = -3;
				//bullet.transform.position = new Vector3 (transform.position.x - .4f, transform.position.y + .2f, -1);
				//}
			} else {
				if (isGrounded && !attack) {
					animator.Play ("Player2_idle");
				}
				rb2d.velocity = new Vector2 (0, rb2d.velocity.y);
			}

			if ((Input.GetKey ("w") || Input.GetKey ("up")) && isGrounded && rb2d.velocity.y == 0) {
				rb2d.velocity = new Vector2 (rb2d.velocity.x, jumpspeed);
				animator.Play ("Player2_jump");
			}

		}

	}



	private void OnTriggerEnter2D(Collider2D collision){
		if (collision.CompareTag ("Bullet")){
			Destroy (collision.gameObject);
			health--;
			if (health <= 0 && isalive) {
				transform.Rotate (Vector3.forward * 90);
				isalive = false;
			}

		} 
	}


}
