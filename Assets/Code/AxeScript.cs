using UnityEngine;
using System.Collections;

public class AxeScript : MonoBehaviour {
	private GameObject tObject;
	private Animator animator;
	// Use this for initialization
	void Start () {
		
		tObject = this.gameObject;
		animator = this.GetComponent<Animator> ();
		Debug.Log ("Object created");
	}

	// Update is called once per frame
	void Update () {
	}
	void OnCollisionEnter2D (Collision2D collision)
	{
		Debug.Log ("Collision");
		if(gameObject.tag == "Magic"){
			tObject.GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
			animator.SetTrigger ("destroy");
		}
	}
	void Destroy(){

		Destroy (tObject);
	}

}
