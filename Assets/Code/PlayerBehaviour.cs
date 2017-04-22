using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

	[SerializeField]
	private float mvspd;
	[SerializeField]
	private float jmpheight;
	[SerializeField]
	private float bltSpd;

	private bool doublejump;
	private bool direction; // true = left, false = right

	public Rigidbody2D Kirves;
	private Rigidbody2D Player;
	public LayerMask Ground;
	private Animator animator;

	private Vector2 s;
	private Vector2 start;
	private Vector3 MousePos;
	private Vector3 Pos;

	void Start () {
		Player = GetComponentInChildren<Rigidbody2D> ();
		Ground = LayerMask.GetMask ("Ground");
		animator = GetComponentInChildren<Animator> ();
		direction = true;
		doublejump = true;
		start = transform.position;
	}
	void FixedUpdate (){
		float horizontal = Input.GetAxis("Horizontal");
		HandleMovement (horizontal);
		Flip (horizontal);
	}

	void Update ()
	{


		if (Input.GetKey (KeyCode.R)) {
			Player.velocity = Vector2.zero;
			transform.position = start;
		}
		if (Input.GetKey(KeyCode.Space) && IsGrounded()) {
			doublejump = true;
			Player.AddForce (Vector2.up, ForceMode2D.Impulse);
			animator.SetTrigger ("jump");
		}
		if (Input.GetKey(KeyCode.Space) && doublejump) {
				doublejump = false;
			Player.AddForce (Vector2.up *jmpheight, ForceMode2D.Impulse);

		}
		if (Input.GetMouseButtonDown (0)) {
			ThrowAxe ();
		}
	}

	private void ThrowAxe() {
		Vector3 MousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Debug.DrawLine (transform.position, MousePos);
		Vector3 Dir = MousePos - transform.position;
		Rigidbody2D KirvesClone = (Rigidbody2D) Instantiate (Kirves, transform.position, transform.rotation);
		KirvesClone.transform.LookAt (Camera.main.ScreenToWorldPoint(MousePos));
		KirvesClone.AddForce (Dir * bltSpd, ForceMode2D.Impulse);
		KirvesClone.AddTorque(-50);
	}
	private void HandleMovement(float horizontal){
		Player.AddForce(Vector2.right * horizontal, ForceMode2D.Impulse);

		animator.SetFloat ("Direction", Mathf.Abs(horizontal));
	}
	private void Flip(float horizontal){
		if ((horizontal > 0 && direction) || (horizontal < 0 && !direction)) {
			direction = !direction;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
	}
	private bool IsGrounded(){
		s = transform.position;
		s.y = GetComponent<Collider2D> ().bounds.min.y + 0.1f;
		Debug.DrawRay(s, Vector2.down, Color.red);
		return Physics2D.Raycast (s, Vector2.down, 0.2f, Ground);
	}

}
