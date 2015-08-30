using UnityEngine;
using System.Collections;
public class ControllerScript : MonoBehaviour {
	public GameObject rope;
	public Transform hand;
	private float speed;
	private Vector2 velocity;
	private LineRenderer LR;
	private Animator anim;
	private Vector2 position = Vector2.zero;
	private Vector2 vectorForward = Vector2.right;
	public int min_length=2;
	private SpringJoint2D springJoint;
	public float ShrinkSpeed=3.5f;
	public static bool isDead=false;
	private int bloodCount;
	private Rigidbody2D rigid;
	private RaycastHit2D hit;
	private float ropelength=0.1f;
	private float ShootRopeSpeed=1.8f;
	private float maxSpeed=30f;
	//

	void Start () {
		Application.targetFrameRate = 60;
		rigid = GetComponent<Rigidbody2D>();
		springJoint = GetComponent<SpringJoint2D> ();
		springJoint.enabled = false;
		anim = GetComponent<Animator> ();
		bloodCount=4;
		vectorForward.y=1f;
		LR = rope.GetComponent<LineRenderer>();
		isDead=false;
		rigid.fixedAngle=true;
	}

	void OnCollisionEnter2D(Collision2D obj){
		if (obj.gameObject.transform.tag == "Thorn"||obj.gameObject.transform.tag == "ground") {
			rigid.fixedAngle = false;
			springJoint.enabled = false;
			if(bloodCount>0) {
				Instantiate (Resources.Load ("BloodObj"), obj.contacts [0].point, Quaternion.identity);
				bloodCount--;
			}
					anim.ResetTrigger("End");
					anim.ResetTrigger("Start");
			anim.enabled = false;
			isDead = true;
			UIScript.isShow = true;
			StopAllCoroutines();
			LR.enabled=false;
		}
	}

	// Update is called once per frame
	void Update () {
		if (!isDead) {

			LR.SetPosition (0, hand.transform.position);
			if (Input.GetButtonDown ("Fire1")) {
				rigid.fixedAngle = true;
//				SetPoint ();

				Shoot();
				speed = rigid.velocity.x;
				if(speed>maxSpeed) {velocity=rigid.velocity;
					velocity.x= maxSpeed;
					rigid.velocity=velocity;
				}
			} else if (Input.GetButtonUp ("Fire1")) {
				StopAllCoroutines();
				point=Vector2.zero;
				ropelength=0.1f;
				rigid.fixedAngle = false;
				springJoint.enabled = false;
				anim.SetTrigger ("End");
				LR.enabled = false;
//				if (speed > 25){
//					StartCoroutine(Slower());
//				} 
			} 
		}
	}

	IEnumerator Shrink(){
		while (springJoint.distance > min_length){
			springJoint.distance -= ShrinkSpeed * Time.deltaTime;
			yield return null;
		}
	}

	IEnumerator Slower(){
		while(rigid.velocity.x>25){
		velocity = rigid.velocity;
		velocity.x -=2*Time.deltaTime;
		rigid.velocity = velocity;
			yield return null;
		}
	}

	void Shoot(){
		position = hand.position;
		hit = Physics2D.Raycast (hand.position, vectorForward, 40f);
		if(hit.collider!=null){
			LR.enabled = true;
			Debug.DrawLine(position,hit.point,Color.red);

			StartCoroutine(ShootRope());

		}
	}
	Vector2 point=Vector2.zero;

	IEnumerator ShootRope(){
		Debug.Log("Shoot");
		while(point.y<hit.point.y){
			position = hand.position;
			Vector2 line = hit.point-position;
			point=hand.position + ropelength*Vector3.Normalize(line);
			ropelength+=ShootRopeSpeed;
			LR.SetPosition (1, point);
			if(point.y>=hit.point.y){
				anim.ResetTrigger("End");
				anim.SetTrigger ("Start");
				springJoint.enabled=true;
				springJoint.distance = Vector2.Distance(hit.point,position)-3f;
				springJoint.connectedAnchor = hit.point;
				StartCoroutine(Shrink());
				LR.SetPosition (1, hit.point);
			}
			yield return null;
		}
	}


}
