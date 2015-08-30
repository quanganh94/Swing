using UnityEngine;
using System.Collections;

public class FollowScript : MonoBehaviour {


	public Transform player;
	// Use this for initialization
	void Start () {
		Camera.main.orthographicSize = 11;

	}
	
	// Update is called once per frame
	void Update () {
			Vector3 position = this.gameObject.transform.position;
			position.x = player.position.x+8.5f;
			this.gameObject.transform.position = position;
		
	}
}
