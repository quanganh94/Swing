using UnityEngine;
using System.Collections;

public class BGLooper : MonoBehaviour {
	Vector2 pos;
	public Object[] cloudMat;
	public GameObject[] Cloud;
	private Vector3 position;
	private int score, lastadded;
	void OnTriggerEnter2D(Collider2D obj){
		if (obj.gameObject.transform.tag == "Cloud") {
				pos = this.gameObject.transform.position;
				pos.y=Random.Range(0f,7.2f);
				obj.gameObject.transform.position=pos;
		}

	}
	void Start () {
	}

	public void AddNumber(int i){
		lastadded =i;
		Debug.Log("Add");
		int temp=i;
		position=transform.position;
		while(temp!=0){
			setCloud(temp%10);
			temp/=10;
		}
	}

	private void setCloud(int index){
		if(Cloud[index]==null) {
			Cloud[index] = Instantiate (cloudMat[index], position, Quaternion.identity) as GameObject;
			Cloud[index].transform.position = position;
		}
		else Cloud[index].transform.position = position;
		position.x-=4.5f;
	}
	// Update is called once per frame

	void Update () {
		score = UIScript.initialScore;
		if(score%10==0&&score!=lastadded){
			AddNumber(score);
		}
	}
}
