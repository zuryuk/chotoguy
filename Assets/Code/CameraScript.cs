using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	[SerializeField]
	private GameObject Player = null;
	private Vector3 offset;
	// Use this for initialization
	void Start () {
		offset = transform.position - Player.transform.position;
	}

	// Update is called once per frame
	void LateUpdate () {
		transform.position = Player.transform.position + offset;
	}
}
