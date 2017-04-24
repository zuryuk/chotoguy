using UnityEngine;
using System.Collections;

public class BossBehaviour : MonoBehaviour {
	private Vector2 start;
	private Quaternion origin;
	private Rigidbody2D Object;
	[SerializeField]
	private GameObject icicle = null;
	private LineRenderer laser;
	[SerializeField]
	private PlayerBehaviour target;

	private Vector2 locked;
	private Vector2 offset;
	private Vector2 offset2;

	void Start () {
		Object = GetComponentInParent<Rigidbody2D> ();
		laser = GetComponent<LineRenderer> ();
		laser.enabled = false;
		start = transform.position;
		origin = transform.rotation;
		offset.x = start.x - 4;
		offset.y = start.y + 10;
		//offset2.x = 613.57f - 612.71f;
		//offset.y = 177.79f - 179.79f;
		offset2.x = start.x - 0.66f;
		offset2.y = start.y + 2;
	}

	void Update () {
		
		if (Input.GetKey (KeyCode.L)) {
			Laser ();
		}

		if (Input.GetKey (KeyCode.R)) {
			Object.angularVelocity = 0;
			Object.velocity = Vector2.zero;
			Object.transform.rotation = origin;
			Object.transform.position = start;
		}
	}
	void Icicles(){
		Debug.Log ("Icicles");
		for (int i = 0; i < 5; i++){
			Debug.Log (offset);
			GameObject IcicleClone = (GameObject) Instantiate (icicle, offset, transform.rotation);
			Destroy (IcicleClone, 2.0f);
			offset.x -= 3;
			}
		offset.x += 15;
	}
	void Laser(){
		laser.SetPosition (0, offset2);
		laser.SetPosition (1, locked);
		laser.enabled = true;
	}
	void LaserOff(){
		laser.enabled = false;
	}
	void LockDown(){
		locked = target.Pos;
	}
}