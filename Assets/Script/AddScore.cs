using UnityEngine;
using System.Collections;

public class AddScore : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	}

	void OnTriggerExit2D(Collider2D obj){
		if(obj.gameObject.tag=="Head") UIScript.upScore();
	}

	// Update is called once per frame
	void Update () {
	}
}
