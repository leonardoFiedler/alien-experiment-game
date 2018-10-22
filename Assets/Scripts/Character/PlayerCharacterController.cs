using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : Character {
	
	protected override void Update () 
	{
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
			attackRoutine = StartCoroutine(Attack());
		}
	}

	private IEnumerator Attack() 
	{
		if (!isAttacking && !isMoving)
		{
			isAttacking = true;
			animator.SetBool("attack", isAttacking);

			yield return new WaitForSeconds(1);

			Collider2D[] hitObjects = Physics2D.OverlapCircleAll(meleeAttackPosition.position, meleeRange);
			if (hitObjects.Length > 1) {
				hitObjects[1].SendMessage("TakeDamage", meleeDamage, SendMessageOptions.DontRequireReceiver);
				Debug.Log("Hit " + hitObjects[1].name);
			}

			StopAttack();
		}
	}
}