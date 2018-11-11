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
			attackRoutine = StartCoroutine(Attack());
		}
	}

	private IEnumerator Attack() 
	{
		if (!isAttacking && !isMoving)
		{
			isAttacking = true;
			Animator.SetBool("attack", isAttacking);

			yield return new WaitForSeconds(1);

			Collider2D[] hitObjects = Physics2D.OverlapCircleAll(meleeAttackPosition.position, MeleeRange);
            if (hitObjects.Length > 0)
            {
                foreach(Collider2D collider2D in hitObjects) {
                    if (collider2D.tag == "Enemy")
                    {
                        collider2D.SendMessage("TakeDamage", MeleeDamage, SendMessageOptions.DontRequireReceiver);
                        Debug.Log("Hit " + collider2D.name);
                    }
                }
            }

			StopAttack();
		}
	}
}