﻿using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

	[SerializeField]
	private float mvspd = 0;
	[SerializeField]
	private float jmpheight = 0;

	[SerializeField]
	private Stat health;
	[SerializeField]
	private Stat mana = null;

	private int attacktype;
	private bool doublejump;
	internal bool direction; // true = left, false = right


	private Rigidbody2D Player;
	[SerializeField]
	private LayerMask Ground;
	[SerializeField]
	private LayerMask Objects;
	private Animator animator;
	[SerializeField]
	private Animator animator2 = null;

	public Vector2 Pos;
	private Vector2 s;
	private Vector2 start;
	private Vector3 easeVelocity;

	void Start () {
		Player = GetComponentInChildren<Rigidbody2D> ();
		Ground = LayerMask.GetMask ("Ground");
		Objects = LayerMask.GetMask ("Objects");
		animator = GetComponentInChildren<Animator> ();

		mana.MaxVal = 100;
		mana.Currval = 100;

		direction = true;
		doublejump = true;
		start = transform.position;
	}
	void FixedUpdate (){
		float horizontal = Input.GetAxis("Horizontal");
		HandleMovement (horizontal);
		Flip (horizontal);
		HandleAttacks ();
		Reset ();
	}

	void Update ()
	{
		Pos = transform.position;
		HandleInput ();
	}
	private void HandleAttacks(){
		if (attacktype == 1) {
			animator.SetTrigger ("Attack");
		}
		if (attacktype == 2) {
			if (!(animator.GetCurrentAnimatorStateInfo (0).IsName ("Magic"))) {
				mana.Currval -= 10;
				animator.SetTrigger ("Magic");
			}
		}
		if (attacktype == 3) {
			animator.SetTrigger ("Throw");
		}
	}
	private void HandleInput(){
		if (Input.GetKeyDown (KeyCode.G)) {
			animator2.SetTrigger ("Attack");
		}
		if (Input.GetKeyDown (KeyCode.H)) {			
				animator2.SetTrigger ("Stomp");
		}
		if (Input.GetKey (KeyCode.J)) {
			animator2.SetTrigger ("Laser");
		}
		if (Input.GetKey (KeyCode.R)) {
			Player.velocity = Vector2.zero;
			transform.position = start;
		}
		if (Input.GetKeyDown(KeyCode.Space)) {
			if(IsGrounded()){
				Player.AddForce (Vector2.up * jmpheight, ForceMode2D.Impulse);
				doublejump = true;
				animator.SetTrigger ("jump");
			}
			else if(doublejump){
				doublejump = false;
				Player.AddForce (Vector2.up *jmpheight, ForceMode2D.Impulse);
				animator.SetTrigger ("jump");
			}
		}

		if (Input.GetKeyDown(KeyCode.LeftShift)) {
			attacktype = 1;
		}
		if (Input.GetKeyDown (KeyCode.LeftControl)) {
			attacktype = 2;
		}
		if (Input.GetMouseButtonDown (0)) {
			attacktype = 3;
		}

	}

	private void HandleMovement(float horizontal){
		easeVelocity = Player.velocity;
		if (easeVelocity.x <= 12f && easeVelocity.x > -12f)
		{
			easeVelocity.x = 0;
		}
		Player.velocity = easeVelocity;

		Player.AddForce(Vector2.right * horizontal * mvspd, ForceMode2D.Impulse);
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
		return Physics2D.Raycast (s, Vector2.down, 0.2f, Ground) || Physics2D.Raycast (s, Vector2.down, 0.2f, Objects);
	}
	private void Reset(){
		attacktype = 0;
	}
}
