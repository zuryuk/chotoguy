using UnityEngine;
using System.Collections;

public class AssetBehaviour : MonoBehaviour {
	private Vector2 start;
	private Quaternion origin;
	private Rigidbody2D Object;

	void Start () {
		Object = GetComponent<Rigidbody2D> ();
		start = transform.position;
		origin = transform.rotation;
	}

	void Update () {
		if (Input.GetKey (KeyCode.R)) {
			Object.angularVelocity = 0;
			Object.velocity = Vector2.zero;
			Object.transform.rotation = origin;
			Object.transform.position = start;
		}
	}
}
