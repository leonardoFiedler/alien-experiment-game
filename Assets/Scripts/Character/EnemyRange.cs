using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour {

	private EnemyCharacterController parent;

	private void Start() {
		parent = GetComponentInParent<EnemyCharacterController>();
	} 

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.tag == "Player")	{
			parent.Target = collision.transform;
		}
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.tag == "Player")	{
			parent.Target = null;
		}
	}
}