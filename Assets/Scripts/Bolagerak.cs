using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolagerak : MonoBehaviour {
	[SerializeField]
	float speed,jump;
	public Camera mainCamera;
	Vector3 movement,movement2,pos;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float mVertical=Input.GetAxis("Vertical");
		float mHorizontal=Input.GetAxis("Horizontal");
		movement = (
			mainCamera.transform.forward * mVertical + 
			mainCamera.transform.right * mHorizontal
		).normalized;
		movement.y = 0f;
		if (Input.GetKeyDown(KeyCode.Space)) {
			movement += Vector3.up * jump; 
			Debug.Log ("ditekan "+movement);
		}
		this.GetComponent<Rigidbody> ().AddForce (movement*speed);

		if (Input.GetKeyDown(KeyCode.X)||Input.GetMouseButtonDown(0)) {
			GameObject projectile = (GameObject)Instantiate(Resources.Load("peluru"));
			projectile.transform.position = this.transform.position;
			//pos = projectile.transform.position;
			//pos += new Vector3 (3, 3, 3);
			Rigidbody rb = projectile.GetComponent<Rigidbody>();
			//movement2 = (mainCamera.transform.forward *speed*Time.deltaTime).normalized;
			movement2 = (Vector3.forward *speed*Time.deltaTime).normalized;
			rb.velocity = movement2*40;
			Destroy (projectile, 3f);

		}
	}
		
}
