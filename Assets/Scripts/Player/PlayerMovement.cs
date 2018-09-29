using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	private float speed;
	
	
	private void Awake() {
		speed = 3;		
	}
	
	void Update () {
		float axisX = Input.GetAxis("Horizontal");
		float axisY = Input.GetAxis("Vertical");
		transform.Translate(new Vector3(axisX, axisY) * Time.deltaTime * speed);
	}
}
