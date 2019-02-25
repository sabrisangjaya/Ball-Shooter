using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {
	public GameObject target,rusak;
	private float timer = 1f;
	bool AttackStatus=true;
	// Use this for initialization
	void Start () {
		rusak.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		target=FindObjectOfType<Bolagerak> ().gameObject;
		transform.LookAt (target.transform.position);
		Attack ();
	}


	private void Attack()
	{
		if (AttackStatus) {
			timer -= Time.deltaTime;
			if (timer <= 0) {
				GameObject bullets = (GameObject)Instantiate (Resources.Load ("pelurumusuh"));
				bullets.transform.position = transform.position;
				Rigidbody rb = bullets.GetComponent<Rigidbody> ();
				rb.velocity = transform.forward * 20;
				Vector3 movement2 = (transform.forward * 10 * Time.deltaTime).normalized;
				rb.velocity = movement2 * 40;
				Destroy (bullets, 3f);
				timer = 1f;
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "peluru(Clone)") {
			Debug.Log ("Hancur"+System.DateTime.Now);
			rusak.SetActive (true);
			AttackStatus = false;
			Destroy (this.gameObject, 5);
		}
	}
}
