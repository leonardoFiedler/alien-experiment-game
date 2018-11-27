using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacterController : Character {

	private Collider2D[] hitObjects;

	protected override void Update () 
	{
		GetInput();
		base.Update();
	}

	protected  override void FixedUpdate()
	{
		base.FixedUpdate();
		VerificaEnemyAtingido();
	}

	private void VerificaEnemyAtingido() 
	{
		if (hitObjects != null && hitObjects.Length > 0)
		{
			bool isHitPlayer = false;
			Collider2D collider = null;
			foreach(Collider2D collider2D in hitObjects) {
				if (collider2D.tag == "Enemy")
				{
					isHitPlayer = true;
					collider = collider2D;
				}

				if (collider2D.tag == "Wall") {
					isHitPlayer = false;
					collider = null;
					break;
				}
			}

			if (isHitPlayer) {
				collider.SendMessage("TakeDamage", MeleeDamage, SendMessageOptions.DontRequireReceiver);
				Debug.Log("Hit " + collider.name);
			}

			hitObjects = null;
		}
	}

	private void GetInput() 
	{
		Direction = Vector2.zero;
		if (Input.GetKey(KeyCode.W)) {
			Direction += Vector2.up;
		}
		if (Input.GetKey(KeyCode.S)) {
			Direction += Vector2.down;
		} 
		if (Input.GetKey(KeyCode.D)) {
			Direction += Vector2.right;
		} 
		if (Input.GetKey(KeyCode.A)) {
			Direction += Vector2.left;
		}
		if (Input.GetKeyDown(KeyCode.Space)) {
			if (attackRoutine == null)
				attackRoutine = StartCoroutine(Attack());
		}
	}

	private IEnumerator Attack() 
	{
		if (!isAttacking && !isMoving)
		{
			isAttacking = true;
			Animator.SetBool("attack", isAttacking);
			hitObjects = Physics2D.OverlapCircleAll(meleeAttackPosition.position, MeleeRange);
			
			yield return new WaitForSeconds(1);
			StopAttack();
		} else {
			yield break;
		}
	}
}