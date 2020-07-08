using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour {

	[SerializeField]
	GameObject player;

	[SerializeField]
	float timeoffset;

	[SerializeField]
	Vector2 posoffset;

	[SerializeField]
	float RightLimit;
	[SerializeField]
	float LeftLimit;
	[SerializeField]
	float UpLimit;
	[SerializeField]
	float DownLimit;

	int ajusteX = 3;
	int ajusteY;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//camera position
		Vector3 startpos = transform.position;

		//player position
		Vector3 endpos = player.transform.position;
		endpos.x += posoffset.x; 
		endpos.y += posoffset.y; 
		endpos.z = -10; 

		transform.position = Vector3.Lerp (startpos, endpos, timeoffset * Time.deltaTime);

		transform.position = new Vector3 (
			Mathf.Clamp (transform.position.x, LeftLimit, RightLimit),
			Mathf.Clamp (transform.position.y, DownLimit, UpLimit),
			transform.position.z
		);
	}

	void OnDrawGizmos(){
		//desenha uma caixa para mostrar os limites da camera
		Gizmos.color = Color.red;
		Gizmos.DrawLine(new Vector2 (LeftLimit-ajusteX,UpLimit), new Vector2(RightLimit+ajusteX,UpLimit));
		Gizmos.DrawLine(new Vector2 (RightLimit+ajusteX,UpLimit), new Vector2(RightLimit+ajusteX,DownLimit));
		Gizmos.DrawLine(new Vector2 (LeftLimit-ajusteX,DownLimit), new Vector2(LeftLimit-ajusteX,UpLimit));
		Gizmos.DrawLine(new Vector2 (RightLimit+ajusteX,DownLimit), new Vector2(LeftLimit-ajusteX,DownLimit));
	}
}
