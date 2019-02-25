using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public GameObject player;

	private Vector3 offset;

	void Start ()
	{
		offset = new Vector3(player.transform.position.x, player.transform.position.y + 3.0f, player.transform.position.z - 5.0f);
		this.transform.Rotate (20, 0, 0);
		this.transform.position = player.transform.position + offset; 
	}
		
	void FixedUpdate ()
	{
		//offset = Quaternion.AngleAxis (Input.GetAxis ("Mouse X") * 4.0f, Vector3.up) * offset;
		transform.LookAt (player.transform.position);
		transform.position = player.transform.position + offset; 
	}




}
