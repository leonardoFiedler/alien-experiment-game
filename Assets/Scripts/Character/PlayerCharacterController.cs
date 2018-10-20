using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCharacterController : Character {
	
	[SerializeField]
	private Stat health;	
	
	protected override void Update () 
	{
		health.MyCurrentValue = this.life;
		GetInput();
		base.Update();
	}


	private void GetInput() 
	{
		direction = Vector2.zero;
		if (Input.GetKey(KeyCode.W)) {
			direction += Vector2.up;
		}
		if (Input.GetKey(KeyCode.S)) {
			direction += Vector2.down;
		} 
		if (Input.GetKey(KeyCode.D)) {
			direction += Vector2.right;
		} 
		if (Input.GetKey(KeyCode.A)) {
			direction += Vector2.left;
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			StartCoroutine(Attack());
		}
	}

	private IEnumerator Attack() 
	{
		animator.SetLayerWeight(1, 1);
		animator.SetBool("attack", true);
		yield return new WaitForSeconds(3);
		animator.SetBool("attack", false);
		Debug.Log("Attacking!!!");
	}
}