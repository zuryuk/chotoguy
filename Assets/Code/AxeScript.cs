using UnityEngine;
using System.Collections;

public class AxeScript : MonoBehaviour {
	GameObject KirvesObj;
	Collider2D Kirves;
	// Use this for initialization
	void Start () {
		KirvesObj = this.gameObject;
		Kirves = this.GetComponent<Collider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		Destroy (KirvesObj, 2f);
	}
	void OnCollisionEnter (Collision collision)
	{
		if (collision.gameObject.tag == "Player") {
			Physics2D.IgnoreCollision (Kirves, GameObject.FindGameObjectWithTag ("Player").GetComponent<Collider2D>());
		}

	}

}
