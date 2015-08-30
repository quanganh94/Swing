using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {


	Object Thorn;
	int thornDownIndex = 0,thornUpIndex=0, ThornAmount=6;
	GameObject[] groupThornDown;
	GameObject[] groupThornUp;
//	public Transform player;
	public Transform upGround;
	public Transform downGround;
	private float minPositiony=-14f,maxPositiony=-10f;
	private float minPositionx=6f,maxPositionx=8f;
	private int maxVary=5;
	private int sign=1;
	private float positionRespawn;
	private float maxDistance=31f, minDistance=27f;
	private float lastposition_y;
	//max distance between 2 thorn
	
	int thorncount=0;
	Vector2 pos_up;
	Vector2 pos_down;


	
	// Use this for initialization
	void Start () {
		groupThornDown = new GameObject[ThornAmount];
		groupThornUp = new GameObject[ThornAmount];
		Thorn = Resources.Load ("Thorn");
	
		maxPositiony=downGround.position.y;;
		minPositiony=maxPositiony-5f;
		pos_down.y=-13f;
//		StartCoroutine(CreateThorn(Random.Range(minGroup,maxGroup)));
		positionRespawn = this.gameObject.transform.position.x;
		StartCoroutine(AddThorn());
	}

	IEnumerator AddThorn(){
		while(thorncount<ThornAmount){
			groupThornDown[thorncount] = Instantiate (Thorn, new Vector2 (400, -100), Quaternion.identity) as GameObject;
			groupThornUp[thorncount] = Instantiate (Thorn, new Vector2 (400, -100), Quaternion.Euler (0, 0, 180f )) as GameObject;
			thorncount++;
			StartCoroutine(Generate());
			yield return null;
		}
	}

	void Update () {

	}

	void OnTriggerEnter2D(Collider2D obj){
		if (obj.gameObject.transform.tag == "Thorn") {
			Debug.Log("Collision Thorn");
			StartCoroutine(Generate());
		}
		
	}

	IEnumerator Generate(){
//		if(thorncount<ThornAmount) Add(thornDownIndex);
		positionRespawn+=Random.Range(minPositionx+2f,maxPositionx);

		pos_down.x=positionRespawn;
		pos_down.y +=sign*Random.Range(2,maxVary);
		if(pos_up.y-pos_down.y<minDistance) pos_down.y = pos_up.y-minDistance;
		sign*=-1;
		if(pos_down.y<minPositiony) pos_down.y=minPositiony;
		else if(pos_down.y>maxPositiony) pos_down.y-=1f;
		groupThornDown[thornDownIndex].transform.position=pos_down;
		positionRespawn+=Random.Range(minPositionx,maxPositionx);
		pos_up.x=positionRespawn;
		pos_up.y=pos_down.y+Random.Range(minDistance,maxDistance);

		groupThornUp[thornUpIndex].transform.position=pos_up;
		thornDownIndex++;
		thornUpIndex++;
		if(thornUpIndex==ThornAmount) thornUpIndex=0;
		if(thornDownIndex==ThornAmount) thornDownIndex=0;
		yield return null;
	}
}
