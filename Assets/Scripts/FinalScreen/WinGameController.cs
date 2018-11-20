using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGameController : MonoBehaviour {

	void Start () {
		Destroy(GameObject.FindGameObjectWithTag("Player"));
	}
	
	void Update () {
		
	}
}
