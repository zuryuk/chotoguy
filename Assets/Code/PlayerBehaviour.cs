using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

	public float mvspd;
	public float jmpheight;
	public float bltSpd;

	private bool doublejump;
	private float maxspd = 5;

	public Rigidbody2D Kirves;
	private Rigidbody2D Player;
	public LayerMask Ground;

	private Vector2 s;
	private Vector2 start;
	private Vector3 MousePos;
	private Vector3 Pos;

	void Start () {
		Player = GetComponent<Rigidbody2D> ();
		Ground = LayerMask.GetMask ("Ground");
		doublejump = true;
		start = transform.position;
	}
		
	void Update ()
	{
		if (Player.velocity.magnitude > maxspd) {
			
		} 
		else if (Input.GetKey (KeyCode.Space) && IsGrounded ()) {
			Player.AddForce (Vector2.up * jmpheight, ForceMode2D.Impulse);
			doublejump = true;
		} 
		else if (Input.GetKeyDown (KeyCode.Space) && doublejump) {
			Player.AddForce (Vector2.up * jmpheight*2, ForceMode2D.Impulse);
			doublejump = false;
		} 
		else if (Input.GetKey (KeyCode.A)) {
			Player.AddForce ((Vector2.left) * mvspd, ForceMode2D.Impulse);
		}
		else if (Input.GetKey (KeyCode.D)) {
			Player.AddForce (Vector2.right * mvspd, ForceMode2D.Impulse);
		}
		if (Input.GetKey (KeyCode.R)) {
			Player.velocity = Vector2.zero;
			transform.position = start;
		}
		else if (Input.GetKeyDown (KeyCode.F)) {
			ThrowAxe ();
		}

		if (Input.GetMouseButtonDown (0)) {
			Vector3 MousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Debug.DrawLine (transform.position, MousePos);
			Vector3 Dir = MousePos - transform.position;
			Rigidbody2D KirvesClone = (Rigidbody2D) Instantiate (Kirves, transform.position, transform.rotation);
			KirvesClone.transform.LookAt (Camera.main.ScreenToWorldPoint(MousePos));
			KirvesClone.AddForce (Dir * bltSpd, ForceMode2D.Impulse);
			KirvesClone.AddTorque(-50);
		}


	}

	private void ThrowAxe(){

		Rigidbody2D KirvesClone = (Rigidbody2D) Instantiate (Kirves, s, transform.rotation);
		KirvesClone.AddForce (Vector2.right * bltSpd, ForceMode2D.Impulse);
		KirvesClone.AddTorque(-200);
	}

	private bool IsGrounded(){
		s = transform.position;
		s.y = GetComponent<Collider2D> ().bounds.min.y + 0.1f;
		Debug.DrawRay(s, Vector2.down, Color.red);
		return Physics2D.Raycast (s, Vector2.down, 0.2f, Ground);
	}

}
