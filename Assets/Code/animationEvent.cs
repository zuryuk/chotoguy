using UnityEngine;
using System.Collections;

public class animationEvent : MonoBehaviour {
	[SerializeField]
	private GameObject Scythe = null;
	[SerializeField]
	private GameObject MagicBody = null;
	protected bool direction; 
	private Vector3 theScale;
	// Use this for initialization
	void Start () {
		theScale = MagicBody.transform.localScale;
		theScale.x *= -1;
	}
	
	// Update is called once per frame
	void Update () {
		direction = GetComponentInParent<PlayerBehaviour> ().direction;
	
	}
	private void ThrowAxe() {
		Vector3 MousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Debug.DrawLine (transform.position, MousePos);
		Vector3 Dir = MousePos - transform.position;
		GameObject ScytheClone = (GameObject) Instantiate (Scythe, transform.position, transform.rotation);
		ScytheClone.transform.LookAt (Camera.main.ScreenToWorldPoint(MousePos));
		ScytheClone.GetComponent<Rigidbody2D>().AddForce (Dir * 3, ForceMode2D.Impulse);
		ScytheClone.GetComponent<Rigidbody2D>().AddTorque(-50);
		Destroy (ScytheClone, 2f);
	}
	private void Magic(){

		GameObject MagicClone = (GameObject) Instantiate (MagicBody, transform.position, transform.rotation);
		if (!direction) {
			MagicClone.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * 20, ForceMode2D.Impulse);
		} else {
			MagicClone.transform.localScale = theScale;
			MagicClone.GetComponent<Rigidbody2D> ().AddForce (Vector2.left * 20, ForceMode2D.Impulse);
		}
		Destroy (MagicClone, 2f);
	}
}
